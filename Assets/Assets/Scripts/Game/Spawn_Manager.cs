using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour {

	[SerializeField]
	private float _enemyWaitTime = 3f;

	[SerializeField]
	private GameObject _enemyPrefab;

	[SerializeField]
	private GameObject[] _powerUps;

	[SerializeField]
	private GameObject _container;
	
	private bool _playerIsDead = false;

	void StartSpawning () {
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnPowerUpRoutine());
	}

	IEnumerator SpawnEnemyRoutine () {
		while (_playerIsDead == false) {

			Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 11, 0);
			GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
			newEnemy.transform.parent = _container.transform;
			yield return new WaitForSeconds(_enemyWaitTime);
	
		}
	}

	IEnumerator SpawnPowerUpRoutine () {
		while (_playerIsDead == false) {

			Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 11, 0);
			int powerUpId = Random.Range(0,3);
			GameObject newPowerUp = Instantiate(_powerUps[powerUpId], posToSpawn, Quaternion.identity);
			PowerUp powerUp = newPowerUp.transform.GetComponent<PowerUp>();
			powerUp.AssignID(powerUpId);
			yield return new WaitForSeconds(Random.Range(7f, 10f));
	
		}
	}

	public void OnPlayerDeath () {
		_playerIsDead = true;
	}

	public void DestroyAsteroid () {
		StartSpawning();
	}
	
}
