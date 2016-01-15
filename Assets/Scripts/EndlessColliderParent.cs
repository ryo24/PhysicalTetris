using UnityEngine;
using System.Collections;

public class EndlessColliderParent : MonoBehaviour {
	public int childCount;
	public int blockNumber;
	public GameObject endlessCollider;
	public bool isMaking = false;

	// Use this for initialization
	void Start () {
		SpawnCollider();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SpawnCollider(){
		isMaking = true;
		StartCoroutine("SpawnColliderCoroutine");
		
	}

	IEnumerator SpawnColliderCoroutine(){
		for(int i = 0; i <= blockNumber; i++){
			IncreaseChildCount();
			yield return new WaitForSeconds(.1f);
		}
		isMaking = false;
	}


	void IncreaseChildCount(){
		Vector3 spawnPosition = new Vector3 (0f,childCount + 0.5f, 0f);
		GameObject child = Instantiate(endlessCollider,spawnPosition,Quaternion.identity) as GameObject;
		child.GetComponent<EndlessCollider>().stepNumber  = childCount;
		child.transform.parent = this.transform;
		childCount++;


	}
}
