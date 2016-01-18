using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndlessCollider : MonoBehaviour {

	GameObject[]   collidedTetriminoChildGameObjects;
	int[] collidedTetriminoChildIds;
	int invalidInstanceId;
	int collidedCount;
	public int stepNumber;
	GameObject stageManager;
	

	int ARRAY_LENGTH =  30;
	int DESTROY_THRESHOLD = 10;

	// Use this for initialization
	void Start () {	
		collidedTetriminoChildIds = new int[ARRAY_LENGTH];
		collidedTetriminoChildGameObjects = new GameObject[ARRAY_LENGTH];

		invalidInstanceId = -1;
		collidedCount = 0;

		stageManager = GameObject.Find("StageManager");

		for(int i = 0; i < collidedTetriminoChildIds.Length; i++){
			collidedTetriminoChildIds[i] = invalidInstanceId;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(collidedCount > DESTROY_THRESHOLD){
			DestroyCollidedTetriminoChild();
		}
	
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "tetriminochild"){
			Debug.Log("OnTriggerEnter:TestCollider");


			bool isfalling  = collider.transform.parent.gameObject.GetComponent<TetriminoParent>().isFallding;

			if(!isfalling){
				Debug.Log("CountStep: " + stepNumber);
				stageManager.SendMessage("IncreaseStepNumber", stepNumber);

			}

			int childInstanceId = collider.gameObject.GetInstanceID();
			Debug.Log("OnTriggerEnter: collided objecct id = " + childInstanceId);

			bool canCount = checkTetriminoChildCount(childInstanceId);
			Debug.Log("canCount = " + canCount);

			if(canCount){
				int arrayNumber = getInsertableNumberInArray();
				Debug.Log("arrayNumber = " + arrayNumber);

				if(arrayNumber >= 0){
					collidedTetriminoChildIds[arrayNumber] = childInstanceId;
					collidedTetriminoChildGameObjects[arrayNumber] = collider.gameObject;
					collidedCount++;
					Debug.Log("Counted Collided Tetrimino Child in TestCollider");
					Debug.Log("collidedCount = " + collidedCount);
				}
			}

		}
	}

	void OnTriggerStay(Collider collider){
		bool isfalling  = collider.transform.parent.gameObject.GetComponent<TetriminoParent>().isFallding;
		
		if(!isfalling){
			Debug.Log("CountStep: " + stepNumber);
			stageManager.SendMessage("IncreaseStepNumber", stepNumber);
			
		}
	}

	void OnTriggerExit(Collider collider){
		Debug.Log("OnTriggerExit start ");

		if(collider.gameObject.tag == "tetriminochild"){
			int exitedChildInstanceId = collider.gameObject.GetInstanceID();

			if(collidedCount > 0){
				for(int id  = 0; id < collidedTetriminoChildIds.Length; id++){
					if(collidedTetriminoChildIds[id] == exitedChildInstanceId){
						Debug.Log("OnTriggerExit: decrease Number ");
						Debug.Log("OnTriggerExit: Exit object id =  " + collidedTetriminoChildIds[id]);
						collidedCount--;
						collidedTetriminoChildIds[id] = invalidInstanceId;
						collidedTetriminoChildGameObjects[id] = null;
						return;
					}
				}
			}
		}
	}

	bool checkTetriminoChildCount(int childInstanceId){

		int existIndex = System.Array.IndexOf( collidedTetriminoChildIds,childInstanceId );
		Debug.Log("existIndex = " + existIndex);
			if (existIndex ==  -1){
				return  true;
			}

		return false;

	}

	int getInsertableNumberInArray(){
		for(int i = 0 ; i < collidedTetriminoChildIds.Length; i++){
			if(collidedTetriminoChildIds[i] == invalidInstanceId){
				Debug.Log("getInsetableNumberInArray: returnNumber = " + i);

				return i;
			}
		}

		return -1;
	}


	void DestroyCollidedTetriminoChild(){
		Debug.Log("Destroy!!!");
		for(int i = 0; i < ARRAY_LENGTH; i++){
			Destroy(collidedTetriminoChildGameObjects[i]);
			collidedTetriminoChildIds[i] = invalidInstanceId;
		}
		stageManager.SendMessage("DecreaseRowLimitNumber");
		collidedCount = 0;

	}

}
