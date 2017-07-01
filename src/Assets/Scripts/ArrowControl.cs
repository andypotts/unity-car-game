using UnityEngine;
using System.Collections;

public class ArrowControl : MonoBehaviour {

	public GameObject Ammo;
	public GameObject Rock;
	public GameObject Finish;
	public Transform target;


	// Use this for initialization
	void Start () {
		Ammo = GameObject.FindWithTag("Ammo"); 
		Rock = GameObject.FindWithTag("Target"); 
		Finish = GameObject.FindWithTag("Finish"); 

		target = Ammo.transform;
	}

	// Update is called once per frame
	void Update () {

		if (Ammo == null && Rock == null){
			target = Finish.transform;
		}
		else if (Ammo == null) {
			target = Rock.transform;
		}

		Vector3 direction = transform.position - target.position;
		Quaternion rotation = Quaternion.LookRotation (direction);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 2f * Time.deltaTime);

	}
}
