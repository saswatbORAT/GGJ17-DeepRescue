using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class resultScript : MonoBehaviour {
	public Text killText;
	public Text feedbackText;


	void Start(){
		int kills = PlayerPrefs.GetInt ("score", 0);
		killText.text = kills.ToString();

		if (kills < 10) {
			feedbackText.text = "You Noob !";
		} else if (kills < 20) {
			feedbackText.text = "You can do better !";
		} else if (kills < 30) {
			feedbackText.text = "Getting there !";
		} else if (kills < 40) {
			feedbackText.text = "Not Bad !";
		} else {
			feedbackText.text = "Damn, Son !";
		}
	}

	public void loadMenu(){
		Application.LoadLevel ("Menu");
	}

	public void loadGame(){
		Application.LoadLevel ("Game");
	}
}
