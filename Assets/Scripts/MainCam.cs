using UnityEngine;
using System.Collections;

public class MainCam : MonoBehaviour {

	public GameObject target;

	public float damping = 6.0f;
	public bool smooth = true;

	public float zoomMax;
	public float zoomMin;
	private Vector3 position;
	private float distancelerp;
	private float distance;
	private float scrollspeed = 15.0f;
	// Use this for initialization
	void Start () {
		distance = zoomMax;
		distancelerp = distance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate () {
		if (target) {
			if (smooth)
			{
				// Look at and dampen the rotation
				//Quaternion rotation = Quaternion.LookRotation(target.transform.position - transform.position);
				//transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
			}
			else
			{
				// Just lookat
				//transform.LookAt(target.transform);
			}

			ZoomCam();

		}
	}

	void ZoomCam(){

		float zoom = Input.GetAxis ("Mouse ScrollWheel");
		if (zoom != 0) {
			distance = Vector3.Distance(transform.position, target.transform.position);
			distance = Mathf.Clamp(distance - zoom * scrollspeed, zoomMin, zoomMax);
		}
		distancelerp = Mathf.Lerp (distancelerp, distance, Time.deltaTime * 5);
		position = target.transform.position + transform.rotation * new Vector3 (0, 0, -distancelerp);
		transform.position = position;
	}

}
