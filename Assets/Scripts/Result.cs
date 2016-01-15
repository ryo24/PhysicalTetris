using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Result : MonoBehaviour {

	public Text result;
	GameManager gameManager;
	string hashtag = "#MakeBlockTower";

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		result.text = gameManager.stepNumber.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Retry(){
		Application.LoadLevel("EndlessScene");
	}

	public void TweetButton(){
		string message = "Make Block Towerで" + gameManager.stepNumber + "段までタワーを積み上げました。" + hashtag;
		Application.OpenURL("http://twitter.com/intent/tweet?text=" + WWW.EscapeURL(message));
	}
}
