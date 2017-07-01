

using UnityEngine;
using System.Collections;

public class Splashcreen : MonoBehaviour {
	
	public Texture btnTexture;
	public bool instructions;

	public GUIText instructionText;
	public GUIText controlsText;
	public float instructionCount;
	
	// Use this for initialization
	void Start () {
		instructions = true;
		instructionCount = 1;
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public void OnGUI()
	{
			
		GUIStyle myButtonStyleBig = new GUIStyle (GUI.skin.button);
		myButtonStyleBig.fontSize = 40;

		GUIStyle textTexture = new GUIStyle (GUI.skin.label);
		textTexture.fontSize = 30;
		textTexture.fixedWidth = Screen.width - 300;
		textTexture.alignment = TextAnchor.MiddleCenter;


		if (instructions) {

			GUI.Label(new Rect(150 ,Screen.height / 2 - 50,100,100),"Instructions: You need to access the main island to rescue the sceintist, but the bridge is blocked by rocks... Find rocket launcher ammo to clear the bridge!", textTexture);

			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 100), "Next", myButtonStyleBig)){
				instructionCount ++;
				instructions = false;
			}

		


		}
		if (!instructions) {
			GUI.Label(new Rect(150, Screen.height / 2 - 50,100,100),"Controls: Tilt the phone to steer, collect ammo to shoot, press buttons to accelarate fire and hand brake", textTexture);

			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 50, 200, 100), "Start", myButtonStyleBig))
					Application.LoadLevel ("Game"); 

			if (GUI.Button (new Rect (Screen.width / 2 - 100, Screen.height / 2 + 150, 200, 100), "Quit", myButtonStyleBig))
					Application.Quit ();
		}

	}
}
