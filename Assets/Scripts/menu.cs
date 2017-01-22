using UnityEngine;
using System.Collections;

public class menu : MonoBehaviour {

	public void loadGame(){
		Application.LoadLevel ("Game");
	}

	public void loadTutorial(){
		Application.LoadLevel ("howtoplay");
	}

	public void close(){
		Application.Quit ();
	}
}
