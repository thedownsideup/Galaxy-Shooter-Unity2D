using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

	private bool _gameOver = false;
	
	// Update is called once per frame
	void Update () {
		if (_gameOver) {
			if (Input.GetKey("r") && _gameOver){
				SceneManager.LoadScene(1); //game scene
			}
		}
	} 

	public void GameOver () {
		_gameOver = true;
	}
}
