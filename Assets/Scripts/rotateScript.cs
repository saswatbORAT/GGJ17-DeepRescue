using UnityEngine;
using System.Collections;

public class rotateScript : MonoBehaviour {
	Vector2 gunPos, mousePos, difference;
	public GameObject bullet;
	GameObject manager;
	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag ("Manager");
	}
	
	// Update is called once per frame
	void Update () {
		gunPos = transform.position;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		mousePos = ray.origin;
		difference = mousePos - gunPos;	
		float angle = Mathf.Atan2 (difference.y, difference.x) * Mathf.Rad2Deg;
		angle += 90;
		transform.eulerAngles = new Vector3 (0, 0, angle);
		if (Input.GetMouseButtonDown (0)) {
			if (manager.GetComponent<GameManagerScript> ().bullets > 0) {
				manager.GetComponent<GameManagerScript> ().changeBulletCount (-1);
				GameObject gj = Instantiate (bullet, transform.position, Quaternion.identity) as GameObject;
				gj.GetComponent<Rigidbody2D> ().velocity = transform.TransformDirection (-Vector2.up * 10);
				gj.transform.parent = transform.parent;
			}
		}
	}

}