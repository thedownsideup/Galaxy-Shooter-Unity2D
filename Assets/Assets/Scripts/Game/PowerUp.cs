using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

	// Use this for initialization
	[SerializeField]
	private float _speed = 3f;
	private int powerupID;
	
	// Update is called once per frame
	public void Update () {
		Move();
		DestroyPowerUp();
	}

	public void Move () {
		transform.Translate(Vector3.down * _speed * Time.deltaTime);
	}

	public void DestroyPowerUp() {
		if (transform.position.y < -2f)
            Destroy(this.gameObject);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject.tag == "Player"){
			Player player = other.transform.GetComponent<Player>();
			if (player != null) {
				if (powerupID == 0)
					player.CollectTripleShotPowerUp();
				else if (powerupID == 1)
					player.CollectSpeedUpPowerUp();

				else 
					player.CollectShieldPowerUp();
				
				Destroy(this.gameObject);
			}
		}
		
	}

	public void AssignID (int id) {
		powerupID = id;
	}
}
