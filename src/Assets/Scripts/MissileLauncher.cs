using UnityEngine;
using System.Collections;

public class MissileLauncher : MonoBehaviour {


	public GameObject missile;

	public float timeBetweenShots = 0.5f;  // Allow 2 shots per second
	private float timestamp;
	public bool ammoStatus;


	private PickupAmmo pickupAmmo;




	// Update is called once per frame
	void Update () {
		pickupAmmo = GetComponent<PickupAmmo>();
	}

	public void OnGUI()
	{
		int butWidth, butHeight;
		GUIStyle myButtonStyleBig = new GUIStyle (GUI.skin.button);
		myButtonStyleBig.fontSize = 45;

		butWidth = butHeight = 200;


		if(GUI.Button (new Rect (200,Screen.height-butHeight,butWidth,butHeight), "Fire",myButtonStyleBig))
		{
					if (Time.time >= timestamp && PickupAmmo.ammoStatus == true){
			 	  	Vector3 position = new Vector3(0f, 65.0f, 5f) * 10.0f;
						position = transform.TransformPoint (position);
						GameObject thisMissile = Instantiate (missile, position, transform.rotation) as GameObject;
						Physics.IgnoreCollision(thisMissile.GetComponent<Collider>(), GetComponent<Collider>());

						timestamp = Time.time + timeBetweenShots;
	        }
		}

	}

}
