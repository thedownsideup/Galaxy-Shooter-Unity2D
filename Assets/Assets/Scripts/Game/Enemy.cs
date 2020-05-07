using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float _speed = 3f;

	private Player _player;
	private Animator _animator;

	[SerializeField]
	private int _points = 10;

	private float _destroyDuration = 2.7f;

	void Start () {
		_player = GameObject.Find("Player").GetComponent<Player>();
		_animator = GetComponent<Animator>();

		if (_player == null)
			Debug.LogError("Player Is Empty");
		if (_animator == null)
			Debug.LogError("Animator Is Empty");
	}
	
	void Update () {
		Move();
		DestroyEnemy();
	}

	void Move () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

	void DestroyEnemy () {
		if (transform.position.y < -2f)
            Destroy(this.gameObject);
	}

	private void OnTriggerEnter2D(Collider2D other){

		if(other.gameObject.tag == "Player"){
			Player player = other.transform.GetComponent<Player>();

			if (player != null){
				player.Damage();
				_speed = 0;
				_animator.SetTrigger("OnEnemyDeath");
				Destroy(this.gameObject, _destroyDuration);
			}
		}

		else if (other.gameObject.tag == "Laser") {
			
			Destroy(other.gameObject);
			_speed = 0;
			_animator.SetTrigger("OnEnemyDeath");
			Destroy(this.gameObject, _destroyDuration);
			
			if (_player != null)
				_player.AddScore(_points);
		}
		else if (other.gameObject.tag == "Shield") {
			Destroy(this.gameObject);
			_player.Damage();
		}
	}

}
