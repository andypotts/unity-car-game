using UnityEngine;
using System.Collections;

public class CarCameraScript : MonoBehaviour {

	public Transform car;
	public float distance = 6.4f;
	public float height = 1.4f;
	public float rotationDamping = 3.0f;
	public float heightDamping = 2.0f;
	public float zoomRatio = 0.5f;
	private Vector3 rotationVector;

	public bool menu = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		if (car) {
			float wantedAngle = car.eulerAngles.y;
			float wantedHeight = car.position.y + height;
			float myAngle = transform.eulerAngles.y;
			float myHeight = transform.position.y;
			myAngle = Mathf.LerpAngle (myAngle, wantedAngle, rotationDamping * Time.deltaTime);
			myHeight = Mathf.Lerp (myHeight, wantedHeight, heightDamping * Time.deltaTime);
			Quaternion currentRotation = Quaternion.Euler (0, myAngle, 0);
			transform.position = car.position;
			transform.position -= currentRotation * Vector3.forward * distance;
			Vector3 newPosition = transform.position;
			newPosition.y = myHeight;
			transform.position = newPosition;
			transform.LookAt (car);
		} else {
			menu = true;
		}

	}

	public void OnGUI(){
		GUIStyle myButtonStyleBig = new GUIStyle (GUI.skin.button);
		myButtonStyleBig.fontSize = 40;

		if (menu) {
				//menu buttons
			if (GUI.RepeatButton (new Rect (Screen.width/2 - 300, Screen.height/2 + 50, 250, 100), "Main menu", myButtonStyleBig)) {
					Application.LoadLevel ("Splashscreen"); 
			}

			if (GUI.RepeatButton (new Rect (Screen.width/2 + 50, Screen.height/2 + 50, 250, 100), "Restart", myButtonStyleBig)) {
					Application.LoadLevel ("Game"); 
			}
		}
	
	}

}
