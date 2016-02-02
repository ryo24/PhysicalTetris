using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int TetriminoMaxNumber;
	public int RowBreakThreshold;
	public int LimitBreakTimes;
	public int stepNumber;
	 
	public static bool isCreated = false;
	
	public enum GameMode{
		Endless,
		Mission
	}

	public GameMode state;


	void Awake(){
		if(!isCreated){
			DontDestroyOnLoad(this.gameObject);
			isCreated = true;
		}else{
			Destroy(this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		Screen.orientation = ScreenOrientation.Portrait;
		stepNumber = 0;

		if(TetriminoMaxNumber == 0){
			TetriminoMaxNumber = 10;
		}
		if(RowBreakThreshold == 0){
			RowBreakThreshold = 20;
		}
		if(LimitBreakTimes == 0){
			LimitBreakTimes = 5;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
