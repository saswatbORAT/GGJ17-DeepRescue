using UnityEngine;
using System.Collections;

public class waveScript : MonoBehaviour {
	AudioClip clip;
	AudioSource audioSource;
	float[] data;
	Vector2[] points;
	public GameObject bullet;
	int x = 0;
	bool move = true;
	bool input = false;
	bool keyInput = true;
	void Start () {
		points = new Vector2[40];
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = Microphone.Start(null, true, 10, 44100);
		audioSource.loop = true;
		while (!(Microphone.GetPosition("Built-in Microphone") > 0)){}
		audioSource.Play();
		data = new float[2048];
	}
		
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && keyInput) {
			input = true;
			keyInput = false;
		}
		if (input) {
			if (transform.position.x < 10) {
				transform.position += Vector3.right * 0.5f;
				if (Microphone.GetPosition("Built-in Microphone") > 0 && move) {
					audioSource.GetOutputData (data, 0);
					moveLine();
				}
			} else {
				input = false;
				move = false;
				checkForEnemies ();
				StartCoroutine ("fadeBar");
			}

		}
	}	

	IEnumerator fadeBar(){
		yield return new WaitForSeconds (0.1f);
		Color col = GetComponent<TrailRenderer> ().material.color;
		col.a -= 0.02f;
		GetComponent<TrailRenderer> ().material.color = col;
		if (col.a < 0.5f) {
			Vector2[] vec = new Vector2[2];
			vec [0] = Vector2.zero;
			vec [1] = Vector2.zero;
			GetComponent<EdgeCollider2D> ().points = vec;
		}
		if (col.a <= 0) {
			transform.position = new Vector3 (-10, 0, 0);
			col.a = 1.0f;
			GetComponent<TrailRenderer> ().material.color = col;
			move = true;
			keyInput = true;
			x = 0;
		} else {
			StartCoroutine ("fadeBar");
		}

	}

	void moveLine()
	{
		float value = 0;
		for (int i = 0; i < data.Length; i++) {
			value += data [i];
		}
		value = Mathf.Abs (value);
		value *= 300;
		if (value > 80) {
			value = 80;
		}

		transform.localPosition = new Vector3 (transform.position.x, value/10, transform.position.z);
		Vector3 targetPosition = new Vector3 (transform.position.x, value, transform.position.z);
		Vector3 velocity = Vector3.zero;

		if (!(transform.position.y < 0.5f && transform.position.y > -0.5f)) {
			transform.localPosition = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, 2.0f);
		} else {
			Vector3 pos = new Vector3 (transform.position.x, 0, transform.position.z);
			transform.localPosition = pos;
		}
		points [x] = transform.localPosition;
		x++;
	}

	void checkForEnemies(){
		gameObject.GetComponent<EdgeCollider2D> ().points = points;
		gameObject.GetComponent<EdgeCollider2D> ().offset = -transform.position;

	}
}