using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	GameObject manager;
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		if (isOutOfRange ()) {
			Destroy (gameObject);
		}
	}

	bool isOutOfRange(){
		Vector2 p1 = transform.parent.position;
		Vector2 p2 = transform.position;
		float distance = Mathf.Sqrt (Mathf.Pow(p2.x-p1.x, 2)+Mathf.Pow(p2.y-p1.y,2));
		return distance > 12;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Enemy") {
			manager.GetComponent<GameManagerScript> ().changeKillsCount (1);
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Health") {
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
		if (col.gameObject.tag == "Bullets") {
			Destroy (col.gameObject);
			Destroy (gameObject);
		}
	}
}
