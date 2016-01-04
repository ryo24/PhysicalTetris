using UnityEngine;
using System.Collections;

public class GoalLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		bool isfalling  = collider.transform.parent.gameObject.GetComponent<TetriminoParent>().isFallding;

		if(collider.gameObject.tag == "tetriminochild" && !isfalling){
			Debug.Log("Clear");
			Application.LoadLevel("Clear");

		}

	}
}
