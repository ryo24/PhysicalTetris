using UnityEngine;
using System.Collections;

public class DestroyWall : MonoBehaviour {
	public GameObject stageManager;

	// Use this for initialization
	void Start () {
		stageManager = GameObject.Find("StageManager");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "tetriminoParent"){
			stageManager.SendMessage("DecreaseLeftNumber");
			Destroy(collider);
		}


	}
}
