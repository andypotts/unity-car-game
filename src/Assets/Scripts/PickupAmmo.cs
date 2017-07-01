using UnityEngine;
using System.Collections;

public class PickupAmmo : MonoBehaviour {


  //ammo
  public static bool ammoStatus;
  public GameObject ammo;
  Object thisAmmo;


	// Use this for initialization
	void Start () {
    ammoStatus = false;
	}


  void OnTriggerEnter(Collider other) {
    if (other.gameObject.tag == "Player")
    {
      ammoStatus = true;
      Destroy(gameObject);
    }
  }





}
