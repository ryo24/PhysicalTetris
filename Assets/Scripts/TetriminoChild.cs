using UnityEngine;
using System.Collections;

public class TetriminoChild : MonoBehaviour {
	public Vector3 InitialLocalPosition;
	GameObject tetriminoSpawner;

	void Awake(){
		InitialLocalPosition = this.transform.localPosition;

	}

	// Use this for initialization
	void Start () {
		tetriminoSpawner = GameObject.Find("TetriminoSpawner");

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
//		if(other.gameObject.tag == "Respawn"){
//			tetriminoSpawner.SendMessage("Spawn");
//			Destroy(this.transform.parent.gameObject);
//		}
	}
}
