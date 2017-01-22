using UnityEngine;
using System.Collections;

public class submarineScript : MonoBehaviour {
	GameObject manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("Manager");
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Enemy") {
			manager.GetComponent<GameManagerScript> ().changeHealthCount (-20);
			if (manager.GetComponent<GameManagerScript> ().health <= 0) {
				print ("game over");
			}
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "Health") {
			manager.GetComponent<GameManagerScript> ().changeHealthCount (5);
			Destroy (col.gameObject);
		}
		if (col.gameObject.tag == "Bullets") {
			manager.GetComponent<GameManagerScript> ().changeBulletCount (10);
			Destroy (col.gameObject);
		}
	}
}
