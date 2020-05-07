using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour {

	private Player _player;
	private Spawn_Manager _spawnManager;
	private Animator _animator;

	[SerializeField]
	private int _points = 5;

	[SerializeField]
	private float _rotationSpeed = 20f;

	[SerializeField]
	private GameObject _explosionPrefab;

	private float _destroyDuration = 1.0f;
	private float _explosionDuration = 2.7f;
	
	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player").GetComponent<Player>();
		_spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();

		if (_player == null)
			Debug.LogError("Player Is Empty");
		if (_spawnManager == null)
			Debug.LogError("Spawn Manager Is Empty");
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}

	private void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			Player player = other.transform.GetComponent<Player>();

			if (player != null)
				player.Damage();
			
			DestroyAsteroid();
		}

		else if (other.gameObject.tag == "Laser") {
			
			Destroy(other.gameObject);
		
			if (_player != null)
				_player.AddScore(_points);
			
			DestroyAsteroid();
		}
	}

	void Move () {
		transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
	}

	void DestroyAsteroid () {

		_spawnManager.DestroyAsteroid();

		GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
		_animator = explosion.gameObject.GetComponent<Animator>();
		if (_animator == null)
			Debug.LogError("Animator Is Empty");

		_animator.SetTrigger("OnAsteroidDestroy");
		Destroy(this.gameObject, _destroyDuration);
		Destroy(explosion, _explosionDuration);
	}
}
