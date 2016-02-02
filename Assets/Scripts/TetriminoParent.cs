using UnityEngine;
using System.Collections;

public class TetriminoParent : MonoBehaviour {

// 	 List<GameObject> children  = new List<GameObject>();
	Transform[] children;
	Vector3 sum;
	Vector3 gravityCenter; 
	float gravityCenterX;
	float gravityCenterY;
	float gravityCenterZ;
	Rigidbody _rigidbody;
	public GameObject tetriminoSpawner;
	float childrenVelocityMagnitude;
//	float coroutineCount;

	public bool isFallding;
	bool isSpawn;

	Vector3 startPosition;
	Vector3 endPosition;

	public bool isHolding;
	public Vector3 offset;
	RaycastHit hit;
	Vector3 world;

	float CAMERA_DEPTH = 15.0f;


	// Use this for initialization
	void Start () {

		 //_rigidbody = this.GetComponent<Rigidbody>();
		isFallding = true;
		isSpawn = false;
		tetriminoSpawner = GameObject.Find("TetriminoSpawner");
//		coroutineCount = 0.0f;


		children = new Transform[transform.childCount];
		//Debug.Log("childrenLength = "+ children.Length);
		int count = 0;
		sum = new Vector3(0,0,0);

		foreach (Transform child in this.transform ){
			 Debug.Log (child.gameObject.name);
			 if(child.tag == "tetriminochild"){
				children[count] = child;
				Debug.Log( "in tag check," + children[count].name );
				//Debug.Log( "chidlren["+ count  + "] position = "  + children[count].position);
				count++;
				child.gameObject.GetComponent<Rigidbody>().velocity = new Vector3 (0,-2.0f,0);


			}
		}

		startPosition = new Vector2 ( 0,0);
		endPosition = new Vector2 (0,0);

			
	}
	
	// Update is called once per frame
	void Update () { 

		foreach(Transform child in children){
			if(child != null){
				childrenVelocityMagnitude += child.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
			}else{
				if(isFallding){
					Debug.Log("TetriminoParent: Child Destroyed. Next Spawn");
					isFallding = false;
					SpawnNextTetrimino();
					return;
				}
			}
		}
		childrenVelocityMagnitude  = childrenVelocityMagnitude / children.Length;
		//Debug.Log( " childrenVelocityMagnitude = " + childrenVelocityMagnitude );


		if( isFallding && childrenVelocityMagnitude <  2.0f){
			Debug.Log("spawnPhase");
			isFallding = false;
			SpawnNextTetrimino();
			 //tetriminoSpawner.SendMessage("Spawn");
		}


		//InputFunction
		//rotate
		if ( isFallding && Input.GetKeyDown("a")){
			leftRotate();
 		}
		if (isFallding && Input.GetKeyDown("d")){
			rightRotate();
		}

		//move
		if (isFallding && Input.GetKeyDown( KeyCode.LeftArrow) && this.transform.position.x > -7f ){

				this.transform.position += new Vector3(-0.5f,0,0);

			foreach( Transform child in children){
				child.position += new Vector3(-0.5f,0,0);
						
			}

		}

		if (isFallding && Input.GetKeyDown(KeyCode.RightArrow) && this.transform.position.x  < 7f ){
			this.transform.position += new Vector3(0.5f,0,0);
			foreach( Transform child in children){
				child.position += new Vector3(0.5f,0,0);
			}
		}


		 //=====Debugging mouse input=====

		if(Input.GetMouseButtonDown(0) && isFallding){
			isHolding = false;
			
			Vector3 mouse = Input.mousePosition;
			mouse.z = CAMERA_DEPTH;
			//			Debug.Log("InputGetMouseButton");
			//			Debug.Log("mousepositon = " + mouse);
			Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
			Ray ray = Camera.main.ScreenPointToRay(mouse);
			
			
			if(Physics.Raycast(ray, out hit,100)){
				Debug.Log("hit");
				//int instanceid = this.transform.GetInstanceID();
				int hitid = hit.transform.GetInstanceID();
				Debug.Log("hitid = " + hitid);
				
				int count = 0;
				foreach(Transform child in children){
					Debug.Log("count = " +  count);
					count++;
					
					if( hitid == child.GetInstanceID()){
						offset =   transform.position - child.position ;
						isHolding  = true;
					}
				}
			}
			
			if(!isHolding){
				if(world.x - this.transform.position.x > 0){
					rightRotate();
				}else{
					leftRotate();
				}
				offset = Vector3.zero;
			}

		}

		if(Input.GetMouseButton(0) && isHolding && isFallding){
			Vector3 mouse = Input.mousePosition;
			mouse.z = CAMERA_DEPTH;
			Vector3 world = Camera.main.ScreenToWorldPoint(mouse);
			transform.position = new Vector3(world.x + offset.x ,transform.position.y,transform.position.z);
			
		}

		 //Touch Input
		if (Input.touchCount > 0 && isFallding){
			Touch touch = Input.GetTouch(0);

			switch(touch.phase){
			case TouchPhase.Began:
				isHolding = false;
				
				startPosition = touch.position;
				startPosition.z = CAMERA_DEPTH;
				//			Debug.Log("InputGetMouseButton");
				//			Debug.Log("mousepositon = " + mouse);
				world = Camera.main.ScreenToWorldPoint(startPosition);
				Ray ray = Camera.main.ScreenPointToRay(startPosition);
				
				
				if(Physics.Raycast(ray, out hit,100)){
					Debug.Log("hit");
					//int instanceid = this.transform.GetInstanceID();
					int hitid = hit.transform.GetInstanceID();
					Debug.Log("hitid = " + hitid);
					
					int count = 0;
					foreach(Transform child in children){
						if( hitid == child.GetInstanceID()){
							offset =   transform.position - child.position ;
							isHolding  = true;
						}
					}
				}
				
				if(!isHolding){
					if(world.x - this.transform.position.x > 0){
						rightRotate();
					}else{
						leftRotate();	
					}
					offset = Vector3.zero;
				}
			break;

			case TouchPhase.Moved:
			case TouchPhase.Stationary:
				Vector3 mouse = touch.position;
				mouse.z = CAMERA_DEPTH;
				world = Camera.main.ScreenToWorldPoint(mouse);
				transform.position = new Vector3(world.x + offset.x ,transform.position.y,transform.position.z);

			break;

//			case TouchPhase.Stationary:
//				float distance = Vector3.Distance(Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,0)), transform.position);
//				Vector3 forward = new Vector3(distance,0,0);
//				//Vector3 forward = new Vector3(touch.position.x - transform.position.x,0,0);
//				forward.Normalize();
//				this.transform.position += forward * Time.deltaTime;
//				break;

			case TouchPhase.Ended:
				endPosition =touch.position;
				endPosition.z = CAMERA_DEPTH;
				float swipeDistance = (endPosition - startPosition).magnitude;

				if(swipeDistance > 20f){

				 	//float signY = Mathf.Sign( (endPosition.y - startPosition.y));
					float signY = Mathf.Sign( endPosition.y - startPosition.y);
					if(signY < 0){
						foreach(Transform child in children){
							child.GetComponent<Rigidbody>().velocity = new Vector3(0, -5.0f,0);
						}
					}
				}
				break;
			}

		}




	}

	public void SpawnNextTetrimino(){
		if(!isSpawn){
			StartCoroutine("SpawnCoroutine");
			isSpawn = true;
		}
	}

	void leftRotate(){
		Debug.Log("leftRotate" ) ;


		moveGravityCenter();
		//moveInitLocalPosition();
		transform.Rotate(0.0f,0.0f,90.0f);


	}

	void rightRotate(){
		
		moveGravityCenter();
		//moveInitLocalPosition();
		transform.Rotate(0.0f,0.0f, -90.0f);
		
		
	}

	void moveGravityCenter(){
		sum = Vector3.zero;

		for (int i  = 0;i < children.Length; i++){
			Debug.Log("childrenpos = " + children[i].position);
			sum += children[i].position;
			
		}
		Debug.Log("sum = "+ sum );
		Debug.Log("sum Y = "+ sum.y );

		gravityCenter = sum / 4.0f;
		Debug.Log("gravitycenter Y = "+ gravityCenter.y);
		Debug.Log("grai¥vitycenter = "+ gravityCenter);

		this.transform.position = gravityCenter;

		moveInitLocalPosition();
		 
	}

	 void moveInitLocalPosition(){
		foreach( Transform child in children){
			Debug.Log("In moveInitLocalPosition, childname = " + child.name);

			Debug.Log("BEFORE child position = " + child.position);
			  child.localPosition = child.gameObject.GetComponent<TetriminoChild>().InitialLocalPosition;
			  Debug.Log("AFTER child position = " + child.position);

		}

	}


	 IEnumerator SpawnCoroutine(){

		for(float coroutineCount  = 0.3f;  coroutineCount > 10f ; coroutineCount -= 0.1f ) {
			Debug.Log("in Coroutine, coroutineCount = " + coroutineCount);

				yield return new  WaitForSeconds(0.1f);
		}
		Debug.Log("Spawn");
 		tetriminoSpawner.SendMessage("Spawn" );

	}

	IEnumerator AutoMoveGravityCenter(){
		while(true){
			moveGravityCenter();
			yield return new WaitForSeconds(0.5f);
		}
	}
}
