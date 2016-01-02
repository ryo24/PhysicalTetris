using UnityEngine;
using System.Collections;

public class TetriminoChild : MonoBehaviour {
	public Vector3 InitialLocalPosition;

	void Awake(){
		InitialLocalPosition = this.transform.localPosition;

	}

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
