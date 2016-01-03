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



	// Use this for initialization
	void Start () {

		 //_rigidbody = this.GetComponent<Rigidbody>();
		isFallding = true;
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

		//StartCoroutine("AutoMoveGravityCenter");
	
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
					StartCoroutine("SpawnCoroutine");
					return;
				}
			}
		}
		childrenVelocityMagnitude  = childrenVelocityMagnitude / children.Length;
		//Debug.Log( " childrenVelocityMagnitude = " + childrenVelocityMagnitude );


		if( isFallding && childrenVelocityMagnitude <  2.0f){
			Debug.Log("spawnPhase");
			isFallding = false;
			StartCoroutine("SpawnCoroutine");
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
