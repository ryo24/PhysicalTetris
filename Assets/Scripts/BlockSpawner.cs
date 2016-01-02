using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour {
	public float spwanInterval;
	float timer = 0;
	public GameObject[] tetrimino;
	public GameObject spawner;


	// Use this for initialization
	void Start () {
		spawner = GameObject.Find("TetriminoSpawner"); 
		Spawn();
	
	}
	
	// Update is called once per frame
	void Update () {
//		if (timer >= spwanInterval){
//			int random = Random.Range(0,tetrimino.Length);
//			Debug.Log(random);
////			Instantiate(tetrimino[random],new Vector3(Random.Range( -6.0f,6.0f), 16.0f,0.0f),Quaternion.identity);
//			Instantiate(tetrimino[random],
//			            new Vector3(Random.Range( -6.0f,6.0f), spawner.transform.position.y, spawner.transform.position.z),
//			            Quaternion.identity);
//
//			timer = 0.0f;
//		}
//		timer += 0.1f;
		timer += 0.04f;

	
	}

	void Spawn(){

		if(timer <= 1.0f){
			return;
		}
		timer = 0.0f;
		int random = Random.Range(0,tetrimino.Length);
		Debug.Log("Spawn Random Seed = " + random);
		//			Instantiate(tetrimino[random],new Vector3(Random.Range( -6.0f,6.0f), 16.0f,0.0f),Quaternion.identity);
		Instantiate(tetrimino[random],
		            new Vector3(Random.Range( -6.0f,6.0f), spawner.transform.position.y, spawner.transform.position.z),
		            Quaternion.identity);


	}
}
