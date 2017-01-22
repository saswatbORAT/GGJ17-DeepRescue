using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
	public int health, bullets, kills;
	public Text bulletText, healthText, killsText;

	void Start(){
		updateBulletText ();
		updateHealthText ();
		updateKillsText ();
	}

	public void changeBulletCount(int value){
		bullets += value;
		if (bullets > 20) {
			bullets = 20;
		}
		if (bullets < 0) {
			bullets = 0;
		}
		updateBulletText ();
	}

	public void changeHealthCount(int value){
		health += value;
		if (health > 100) {
			health = 100;
		}
		if (health <= 0) {
			health = 0;
			PlayerPrefs.SetInt ("score", kills);
			Application.LoadLevel ("Result");
		}
		updateHealthText ();
	}

	public void changeKillsCount(int value){
		kills += value;
		if (bullets < 0) {
			bullets = 0;
		}
		updateKillsText ();
	}

	public void updateHealthText(){
		healthText.text = "Health : " + health.ToString ();
	}

	public void updateBulletText(){
		bulletText.text = "Bullets : " + bullets.ToString ();
	}

	public void updateKillsText(){
		killsText.text = "Kills : " + kills.ToString ();
	}
}
