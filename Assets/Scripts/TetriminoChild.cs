using UnityEngine;
using System.Collections;

public class TetriminoChild : MonoBehaviour {
	public Vector3 InitialLocalPosition;
	GameObject tetriminoSpawner;
	GameObject stageManager;



	void Awake(){
		InitialLocalPosition = this.transform.localPosition;

	}

	// Use this for initialization
	void Start () {
		tetriminoSpawner = GameObject.Find("TetriminoSpawner");
		stageManager = GameObject.Find("StageManager");
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y < -20f){
			this.transform.parent.gameObject.SendMessage("SpawnNextTetrimino");
			//tetriminoSpawner.SendMessage("Spawn");

			stageManager.SendMessage("DecreaseLeftNumber");
			Destroy(this.transform.parent.gameObject); 
		}
	}

	void OnTriggerEnter(Collider other){
//		if(other.gameObject.tag == "Respawn"){
//			tetriminoSpawner.SendMessage("Spawn");
//			Destroy(this.transform.parent.gameObject);
//		}
	}
}
