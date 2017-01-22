using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {
	[System.Serializable] public struct Range{
		public float xMin, xMax, yMin, yMax;
		public float minSpeed, maxSpeed, minAltitude, maxAltitude;
	}

	public GameObject enemy, health, bullets;
	enum SpawnType{items, enemies};

	public Range enemyRange, healthRange, bulletsRange;

	void Start () {
		StartCoroutine ("spawnEnemy");
		StartCoroutine ("spawnBullets");
		StartCoroutine ("spawnHealth");
	}

	IEnumerator spawnEnemy(){
		yield return new WaitForSeconds (1.0f);
		float x, y, speed, altitude;
		int side = Random.Range (0, 2);
		x = Random.Range ((float)enemyRange.xMin, (float)enemyRange.xMax) * (side == 0 ? -1 : 1);
		y = Random.Range ((float)enemyRange.yMin, (float)enemyRange.yMax);
		speed = Random.Range ((float)enemyRange.minSpeed, (float)enemyRange.maxSpeed) *( side == 0 ? 1 : -1);
		altitude = Random.Range ((float)enemyRange.minAltitude, (float)enemyRange.maxAltitude);

		GameObject gj = Instantiate (enemy, Vector3.zero, Quaternion.identity) as GameObject;
		gj.transform.parent = transform;
		gj.transform.localPosition = new Vector3 (x, y, 0);
		gj.GetComponent<enemyScript> ().start (speed, altitude);
		StartCoroutine ("spawnEnemy");
	}

	IEnumerator spawnBullets(){
		yield return new WaitForSeconds (4.0f);
		float x, y, speed, altitude;
		int side = Random.Range (0, 2);
		x = Random.Range ((float)bulletsRange.xMin, (float)bulletsRange.xMax) * (side == 0 ? -1 : 1);
		y = Random.Range ((float)bulletsRange.yMin, (float)bulletsRange.yMax);
		speed = Random.Range ((float)bulletsRange.minSpeed, (float)bulletsRange.maxSpeed) *( side == 0 ? 1 : -1);
		altitude = Random.Range ((float)bulletsRange.minAltitude, (float)bulletsRange.maxAltitude);

		GameObject gj = Instantiate (bullets, Vector3.zero, Quaternion.identity) as GameObject;
		gj.transform.parent = transform;
		gj.transform.localPosition = new Vector3 (x, y, 0);
		gj.GetComponent<enemyScript> ().start (speed, altitude);
		StartCoroutine ("spawnBullets");
	}

	IEnumerator spawnHealth(){
		yield return new WaitForSeconds (7.0f);
		float x, y, speed, altitude;
		int side = Random.Range (0, 2);
		x = Random.Range ((float)healthRange.xMin, (float)healthRange.xMax) * (side == 0 ? -1 : 1);
		y = Random.Range ((float)healthRange.yMin, (float)healthRange.yMax);
		speed = Random.Range ((float)healthRange.minSpeed, (float)healthRange.maxSpeed) *( side == 0 ? 1 : -1);
		altitude = Random.Range ((float)healthRange.minAltitude, (float)healthRange.maxAltitude);

		GameObject gj = Instantiate (health, Vector3.zero, Quaternion.identity) as GameObject;
		gj.transform.parent = transform;
		gj.transform.localPosition = new Vector3 (x, y, 0);
		gj.GetComponent<enemyScript> ().start (speed, altitude);
		StartCoroutine ("spawnHealth");
	}


	// Update is called once per frame
	void Update () {
	
	}
}
