using UnityEngine;
using System.Collections;

public class CarControlScript : MonoBehaviour {
	public WheelCollider WheelFL;
	public WheelCollider WheelFR;
	public WheelCollider WheelRL;
	public WheelCollider WheelRR;

	public Transform WheelFRTrans;
	public Transform WheelFLTrans;
	public Transform WheelRLTrans;
	public Transform WheelRRTrans;

	float lowestSteerAtSpeed = 50;
	float lowSpeedSteerAngle = 10;
	float highSpeedSteerAngle = 1;

	float decelerationSpeed = 30;
	public float currentSpeed;
	float topSpeed = 150;

	private float maxTorque = 50;
	private int gasLevel;
	private int brakeLevel=0;

	//messages
	public GUIText GameOverText;
	public GUIText GameCompleteText;
	public GameObject finish;
	public GameObject water;
	public GameObject explosion;

	private bool menu = false;


	//textures
	public Texture brakeMap;
	public Texture normalMap;
	public GameObject truckBody;
	public GameObject car;

	//sound

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().centerOfMass = new Vector3(0,0,0);
		gasLevel = 0;
		brakeLevel = 0;
		GameOverText.enabled = false;
		GameCompleteText.enabled = false;

	}

	// Update is called once per frame
	void Update () {
		WheelFLTrans.Rotate(WheelFL.rpm/60*360*Time.deltaTime,0,0);
		WheelFRTrans.Rotate(WheelFR.rpm/60*360*Time.deltaTime,0,0);
		WheelRLTrans.Rotate(WheelRL.rpm/60*360*Time.deltaTime,0,0);
		WheelRRTrans.Rotate(WheelRR.rpm/60*360*Time.deltaTime,0,0);

		float ttFL = WheelFL.steerAngle - WheelFLTrans.localEulerAngles.z;
		float ttFR = WheelFR.steerAngle - WheelFRTrans.localEulerAngles.z;
		Vector3 AngleFL = new Vector3 (WheelFLTrans.localEulerAngles.x,ttFL,WheelFLTrans.localEulerAngles.z); WheelFLTrans.localEulerAngles = AngleFL;
		Vector3 AngleFR = new Vector3 (WheelFRTrans.localEulerAngles.x,ttFR,WheelFRTrans.localEulerAngles.z); WheelFRTrans.localEulerAngles = AngleFR;
	}

	void FixedUpdate () {
		truckBody.GetComponent<Renderer>().material.mainTexture = normalMap;


		currentSpeed = (2*3.1415926f *WheelRL.radius*WheelRL.rpm*60/1000);
		currentSpeed = Mathf.Round(currentSpeed);


		if(currentSpeed < topSpeed)
		{
			//WheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
			//WheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");

			WheelRR.motorTorque = maxTorque * gasLevel;
			WheelRL.motorTorque = maxTorque * gasLevel;

		}
		else
		{
			WheelRR.motorTorque = 0;
			WheelRL.motorTorque = 0;
		}

		/*if(Input.GetButton("Vertical")==false) //if no gas, slow down the car
		{
			WheelRR.brakeTorque = decelerationSpeed;
			WheelRL.brakeTorque = decelerationSpeed;
		}
		else
		{
			WheelRR.brakeTorque = 0;
			WheelRL.brakeTorque = 0;
		}*/

		if (brakeLevel > 0 )
		{
			truckBody.GetComponent<Renderer>().material.mainTexture = brakeMap;
			WheelRR.brakeTorque = 150;
			WheelRL.brakeTorque = 150;
			WheelRR.motorTorque = 0;
			WheelRL.motorTorque = 0;



		}
		else if (gasLevel==0)
		{
			WheelRR.brakeTorque = 30;
			WheelRL.brakeTorque = 30;
		}
		else
		{
			WheelRR.brakeTorque = 0;
			WheelRL.brakeTorque = 0;
		}

		Vector3 accelerator = Input.acceleration;

		float speedFactor = GetComponent<Rigidbody>().velocity.magnitude/lowestSteerAtSpeed;
		float currentSteerAngle = Mathf.Lerp(lowSpeedSteerAngle,highSpeedSteerAngle,speedFactor);
		//currentSteerAngle *= Input.GetAxis("Horizontal");
		currentSteerAngle *= accelerator.x*2.0f;
		WheelFL.steerAngle = currentSteerAngle;
		WheelFR.steerAngle = currentSteerAngle;


		//desktop controls
		if (Application.platform == RuntimePlatform.OSXEditor) {
			WheelFL.steerAngle = 10 * Input.GetAxis("Horizontal");
			WheelFR.steerAngle = 10 * Input.GetAxis("Horizontal");
		}

		GetComponent<AudioSource>().pitch = currentSpeed / topSpeed + 0.5f;
		GetComponent<AudioSource>().volume = currentSpeed / topSpeed + 0.5f;

	}

	public void OnGUI()
	{
		int butWidth, butHeight;
		GUIStyle myButtonStyleBig = new GUIStyle (GUI.skin.button);
		myButtonStyleBig.fontSize = 40;

		butWidth = butHeight = 200;


		if(menu == true){
			//menu buttons
			if(GUI.RepeatButton (new Rect (Screen.width-butWidth,Screen.height-butHeight,butWidth,butHeight), "Main menu",myButtonStyleBig))
			{
				Application.LoadLevel ("Splashscreen"); 
			}
			
			if(GUI.RepeatButton (new Rect (Screen.width-butWidth,Screen.height-butHeight,butWidth,butHeight), "Restart",myButtonStyleBig))
			{
				Application.LoadLevel ("Game"); 
			}

		}





		//controls 
		if(GUI.RepeatButton (new Rect (Screen.width-butWidth,Screen.height-butHeight,butWidth,butHeight), "Acc",myButtonStyleBig))
		{
			gasLevel = 2;
		}
		else gasLevel =0;


		if(GUI.RepeatButton (new Rect (Screen.width-butWidth - butWidth,Screen.height-butHeight,butWidth,butHeight), "Reverse",myButtonStyleBig))
		{
			gasLevel = -1;
		}

		if (GUI.RepeatButton (new Rect (0, Screen.height - butHeight, butWidth, butHeight), "Brake", myButtonStyleBig)) {
			brakeLevel = 5;
			var curveR = WheelRR.sidewaysFriction;  // get a copy
			curveR.stiffness = 0.01f;          // change the value
			WheelRR.sidewaysFriction = curveR;

			var curveL = WheelRL.sidewaysFriction;  // get a copy
			curveL.stiffness = 0.01f;          // change the va
			WheelRL.sidewaysFriction = curveL;

		} else {
			brakeLevel = 0;
			
			var curveR = WheelRR.sidewaysFriction;  // get a copy
			curveR.stiffness = 0.15f;          // change the value
			WheelRR.sidewaysFriction = curveR;
			
			var curveL = WheelRL.sidewaysFriction;  // get a copy
			curveL.stiffness = 0.15f;          // change the va
			WheelRL.sidewaysFriction = curveL;
		}
		
		
		
		/*if(GUI.Button (new Rect (Screen.width-butWidth,Screen.height-butHeight,butWidth,butHeight), "Acc",myButtonStyleBig))
		{
			gasLevel =0.0f;
		}*/
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag=="Finish"){
			menu = true;
			GameCompleteText.enabled = true;
			truckBody.GetComponent<Renderer>().enabled = false;
			Destroy(car);
		}
		if(collision.gameObject.tag=="Water"){ 
			menu = true;
			Instantiate (explosion, transform.position, transform.rotation);
			GameOverText.enabled = true;
			truckBody.GetComponent<Renderer>().enabled = false;
			Destroy(car);
		}

	}
	
	
}
