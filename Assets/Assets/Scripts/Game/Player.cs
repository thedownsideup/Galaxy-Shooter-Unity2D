using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	private float _speed = 10f;
	
	[SerializeField]
	private GameObject _laserPrefab;
	
	[SerializeField]
	private GameObject _tripleshotPrefab;
	
	[SerializeField]
	private int _lives = 3;
	
	[SerializeField]
	private float _laseroffset = 1.05f;
	
	[SerializeField]
	private float fireRate = 0.05f;
	
	[SerializeField]
	private float _canFire = -1f;

	[SerializeField]
	private bool _tripleShotCollected = false;
	[SerializeField]
	private bool _speedUpCollected = false;
	[SerializeField]
	private bool _shieldCollected = false;

	[SerializeField]
	private float _coolDownTime = 5f;
	
	[SerializeField]
	private float _speedUp = 1.5f;
	
	[SerializeField]
	private GameObject _shield;
	
	[SerializeField]
	private int _score = 0;
	
	private Spawn_Manager _spawnManager;
	private UI_Manager _uiManager;

	private Coroutine _currentSpeedup = null;
	private Coroutine _currentTripleshot = null;
	private Coroutine _currentShield = null;

	[SerializeField]
	private GameObject _rightEngine;
	[SerializeField]
	private GameObject _leftEngine;

	void Start () {
		transform.position = new Vector3(0, 0, 0);
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
		_uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

		if (_spawnManager == null)
			Debug.LogError("Spawn Manager Is Empty");
		if (_uiManager == null)
			Debug.LogError("UI Manager Is Empty");
	}

	void Update () {

		Move();
		if (Input.GetMouseButtonDown(0) && Time.time > _canFire)
			Fire();
	}

	void Move () {
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
		
		if (_speedUpCollected)
			transform.Translate(direction * _speedUp * _speed * Time.deltaTime);
		else
			transform.Translate(direction * _speed * Time.deltaTime);
			
		if(transform.position.x > 11)
			transform.position = new Vector3(-11, transform.position.y, 0);
		
		if(transform.position.x < -11)
			transform.position = new Vector3(11, transform.position.y, 0);
	}

	void Fire () {
		_canFire = Time.time + fireRate;
		if (_tripleShotCollected)
			Instantiate(_tripleshotPrefab, transform.position, Quaternion.identity);
		else
			Instantiate(_laserPrefab, transform.position + new Vector3(0, _laseroffset, 0) , Quaternion.identity);
		
	}

	public void Damage () {

		if (_shieldCollected) {
			_shieldCollected = false;
			_shield.SetActive(false);
			return;
		}

		_lives--;
		_uiManager.UpdateLives(_lives);

		if (_lives == 2) 
			_rightEngine.SetActive(true);

		if (_lives == 1) 
			_leftEngine.SetActive(true);		
			
		if (_lives == 0) {
			Destroy(this.gameObject);
			_spawnManager.OnPlayerDeath();
		}
	}

	public void CollectTripleShotPowerUp () {
		_tripleShotCollected = true;		
		if (_currentTripleshot != null) {
         	StopCoroutine(_currentTripleshot);
		}
		_currentTripleshot = StartCoroutine(TripleShotPowerDown());
	}

	IEnumerator TripleShotPowerDown () {
		yield return new WaitForSeconds(_coolDownTime);
		_tripleShotCollected = false;
	}

	public void CollectSpeedUpPowerUp () {

		_speedUpCollected = true;
	
		if (_currentSpeedup != null) {
         	StopCoroutine(_currentSpeedup);
		}
		_currentSpeedup = StartCoroutine(SpeedUpPowerDown());

	}

	IEnumerator SpeedUpPowerDown () {
		yield return new WaitForSeconds(_coolDownTime);
		_speedUpCollected = false;
	}

	public void CollectShieldPowerUp () {
		_shieldCollected = true;
		_shield.SetActive(true);
		if (_currentShield != null) {
         	StopCoroutine(_currentShield);
		}
		_currentShield = StartCoroutine(ShieldPowerDown());
	}

	IEnumerator ShieldPowerDown () {

		yield return new WaitForSeconds(_coolDownTime);
		_shieldCollected = false;
		_shield.SetActive(false);
	}

	public void AddScore (int points) {
		_score += points;
		_uiManager.UpdateScore(_score);
	}
}
