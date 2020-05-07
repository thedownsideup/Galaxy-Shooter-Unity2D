using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour {
	
	[SerializeField]
	private Text _scoreText;
	[SerializeField]
	private Text _gameoverText;
	[SerializeField]
	private Text _restartText;
	[SerializeField]
	private Sprite[] _lives_Sprites;
	[SerializeField]
	private Image _livesImg;
	private Game_Manager _gameManager;
	// Use this for initialization
	void Start () {

		_gameManager = GameObject.Find("Game_Manager").GetComponent<Game_Manager>();
		if(_gameManager == null) {
			Debug.LogError("Game Manager Is Empty");
		}
		_scoreText.text = "Score: " + 0;
		_gameoverText.gameObject.SetActive(false);
		_restartText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void UpdateScore (int score) {
		_scoreText.text = "Score: " + score;
	}

	public void UpdateLives (int lives) {
		_livesImg.GetComponent <Image> ().sprite = _lives_Sprites[lives];
		if (lives == 0){
			GameOverSequence();
		}

	}

	void GameOverSequence () {
		_gameManager.GameOver();
		StartCoroutine(GameOverFlicker());
		_restartText.gameObject.SetActive(true);
	}

	IEnumerator GameOverFlicker () {
		while (true) {
			_gameoverText.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			_gameoverText.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
