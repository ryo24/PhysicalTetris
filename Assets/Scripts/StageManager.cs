using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {
	public int tetriminoMaxNumber;
	public int RowBreakThreshold;
	public Text leftNumberText;
	public Text stepNumberText;
	GameManager gameManager;
	int stepNumber;

	// Use this for initialization
	void Start () {
		tetriminoMaxNumber =   30;
		RowBreakThreshold = 15;
		leftNumberText.text = tetriminoMaxNumber.ToString();
		stepNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(tetriminoMaxNumber <= -1){
			GameOver();
		}
	
	}

	public void DecreaseLeftNumber(){
		tetriminoMaxNumber--;
		leftNumberText.text = tetriminoMaxNumber.ToString();

	}

	public void IncreaseStepNumber(int colliderNumber){
		if (colliderNumber > stepNumber){
			stepNumber = colliderNumber;
			stepNumberText.text = stepNumber.ToString();
		}
	}

	void GetGameSettings(){

	}

	void GameOver(){
		Debug.Log("GameOver");
		Application.LoadLevel("Title");

	}
}
