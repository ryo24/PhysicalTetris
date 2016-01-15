using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndlessStageManager : MonoBehaviour {
	public int tetriminoMaxNumber;
	public int RowBreakThreshold;
	public int LimitBreakTimes;
	public Text leftNumberText;
	public Text stepNumberText;
	public Text limitTimesText;
	int stepNumber;

	GameManager gameManager;
	public GameObject endlessColliderParent;
	
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		endlessColliderParent = GameObject.Find("EndlessColliderParent");

		tetriminoMaxNumber =   gameManager.TetriminoMaxNumber;
		RowBreakThreshold = gameManager.RowBreakThreshold;
		LimitBreakTimes = gameManager.LimitBreakTimes;

		stepNumber = 0;
		leftNumberText.text = tetriminoMaxNumber.ToString();
		stepNumberText.text = stepNumber.ToString();
		limitTimesText.text = LimitBreakTimes.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		if(tetriminoMaxNumber <= 0 || LimitBreakTimes <= 0 ){
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

		int childCount = endlessColliderParent.GetComponent<EndlessColliderParent>().childCount;
		bool isMaking = endlessColliderParent.GetComponent<EndlessColliderParent>().isMaking;
		if(childCount - stepNumber  <= 2 && isMaking == false){
			endlessColliderParent.SendMessage("SpawnCollider");
		}

	}

	public void DecreaseRowLimitNumber(){
		LimitBreakTimes--;
		limitTimesText.text = LimitBreakTimes.ToString();
	}
	
	void GetGameSettings(){
		
	}
	
	void GameOver(){
		Debug.Log("GameOver");
		gameManager.stepNumber = stepNumber;
		Application.LoadLevel("GameOver");
		
	}



}