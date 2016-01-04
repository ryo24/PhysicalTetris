using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
	public int tetriminoMaxNumber;
	public int RowBreakThreshold;
	public Text leftNumber;

	// Use this for initialization
	void Start () {
		tetriminoMaxNumber =   30;
		RowBreakThreshold = 15;
		leftNumber.text = tetriminoMaxNumber.ToString();
	
	}
	
	// Update is called once per frame
	void Update () {
		if(tetriminoMaxNumber <= -1){
			GameOver();
		}
	
	}

	public void DecreaseLeftNumber(){
		tetriminoMaxNumber--;
		leftNumber.text = tetriminoMaxNumber.ToString();

	}

	void GetGameSettings(){

	}

	void GameOver(){
		Debug.Log("GameOver");
		Application.LoadLevel("Title");

	}
}
