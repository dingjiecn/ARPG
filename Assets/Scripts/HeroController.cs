using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	CharacterController controller;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		UpdateGravity ();
	}

	void UpdateGravity(){
		controller.Move (new Vector3 (0.0f, -10.0f * Time.deltaTime, 0.0f));

	}
}
