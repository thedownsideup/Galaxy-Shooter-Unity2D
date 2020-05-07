using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	[SerializeField]
	private float _speed = 7f;


	void Start () {
	}
	

	void Update () {
		Move();
	}

	void Move () {
		transform.Translate(Vector3.up * _speed * Time.deltaTime);
		
		if(transform.position.y >= 10f || transform.position.y <= -2f || transform.position.x >= 11f || transform.position.x <= -11f){
			if (transform.parent != null) {
				Destroy(transform.parent.gameObject);
			}
			Destroy(this.gameObject);
		}
	}
}
