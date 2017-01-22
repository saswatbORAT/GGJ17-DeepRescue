using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour {

	float speed, altitude;
	bool move = false;
	bool attack = false;
	bool isHit = false;
	float count = 0;
	float yPos;
	public Vector3 sub;
	public Vector2 direction;
	// Use this for initialization
	void Start () {
		sub = GameObject.Find ("Submarine").transform.position;
		int hit = Random.Range (0,5);
		if (hit == 0 && tag == "Enemy") {
			print ("attack");
			attack = true;
			move = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			float height = yPos + (Mathf.Sin (count) * altitude);
			float xPos = transform.localPosition.x + speed;
			transform.position = new Vector3 (xPos, height, 0);
			count += speed;
		}
		if (attack) {
			if (isVisible (transform.position, sub)) {
				GetComponent<SpriteRenderer> ().enabled = true;
			}
			transform.localPosition = Vector3.Lerp (transform.position, sub, Time.deltaTime);
		//	transform.localPosition += moveObj(transform.localPosition, sub, speed);//Vector3.MoveTowards(transform.localPosition, sub, speed);
		}
		if (transform.position.y > 15) {
			Destroy (gameObject);
		}
		if (speed < 0) {
			if (transform.localPosition.x < -9) {
				Destroy (gameObject);
			}
		} else {
			if (transform.localPosition.x > 9) {
				Destroy (gameObject);
			}
		}
	}

	bool isVisible(Vector2 p1, Vector2 p2){
		float distance = Mathf.Sqrt (Mathf.Pow(p2.x-p1.x, 2)+Mathf.Pow(p2.y-p1.y,2));
		return distance < 4;
	}

	Vector3 moveObj(Vector2 f, Vector2 t, float speed){
		Vector2 difference = f - t;
		float angle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		Vector2 d = new Vector2 (-Mathf.Sin (angle * Mathf.Deg2Rad), Mathf.Cos (angle * Mathf.Deg2Rad));
		print (angle);
		direction = d*speed;
		return direction;
	}

	public void start(float pSpeed, float pAltitude){
		speed = pSpeed;
		altitude = pAltitude;
		yPos = transform.position.y;
		move = true;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Trail" && !isHit) {
			isHit = true;
			GetComponent<SpriteRenderer> ().enabled = true;
			if (tag == "Enemy") {
				int attackShip = Random.Range (0, 2);
				attack = attackShip == 0;
				move = !attack;
			} else {
				move = false;
				attack = true;
			}
		}
	}
}
