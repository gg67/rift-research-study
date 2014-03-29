using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	private int inputWidth = 200;
	public static string subjectNumber = "0";

	void OnGUI() {
		subjectNumber = GUI.TextField(new Rect(Screen.width/2 - inputWidth/2, Screen.height/2, 200, 20), subjectNumber, 25);
	}
}
