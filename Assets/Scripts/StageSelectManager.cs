﻿using UnityEngine;
using System.Collections;

public class StageSelectManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartStage(){
		Application.LoadLevel("TestMainScene");
	}
}
