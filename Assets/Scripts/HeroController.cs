using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

	public enum HeroState { Idle, Run};
	public GameObject mouseFX;

	CharacterController controller;
	public float runSpeed;
	private Vector3 moveTargetPos;
	private Quaternion moveTargetRot;
	private int layerGround = 8;
	private HeroState heroState;
	private AnimationManager nanimationManager;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();
		nanimationManager = GetComponent<AnimationManager> ();
		moveTargetPos = this.transform.position;
		moveTargetRot = this.transform.rotation;
		heroState = HeroState.Idle;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateGravity ();

		ClickMove ();
	}

	void UpdateGravity(){
		controller.Move (new Vector3 (0.0f, -10.0f * Time.deltaTime, 0.0f));

	}

	void ClickMove(){
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(ray, out hit, 100, 1 << layerGround)){
				if(hit.collider != null && hit.collider.tag == "Ground"){
					Instantiate(mouseFX, new Vector3(hit.point.x, hit.point.y + 0.2f, hit.point.z), hit.collider.transform.rotation);

					moveTargetPos = hit.point;
					heroState = HeroState.Run;
					nanimationManager.Run();
				}
			}
		}

		if (heroState == HeroState.Run) {
			Vector3 targetPoint;
			targetPoint.x = moveTargetPos.x;
			targetPoint.z = moveTargetPos.z;
			targetPoint.y = transform.position.y;
			//ttransform.LookAt(targetPoint);
			moveTargetRot = Quaternion.LookRotation (targetPoint - transform.position);
			//transform.rotation = moveTargetRot;
			transform.rotation = Quaternion.Lerp (this.transform.rotation, moveTargetRot, Time.deltaTime * 10);

			float dist = Vector3.Distance (this.transform.position, moveTargetPos);
			if (dist < 0.5f) {
					heroState = HeroState.Idle;
					nanimationManager.Idle();

			} else {
					controller.Move (transform.forward * runSpeed * Time.deltaTime);
			}
		}
	}

	void ButtonMove(){
		float transX = 0.0f;
		float transZ = 0.0f;
		float inputV = Input.GetAxis ("Vertical");
		float inputH = Input.GetAxis ("Horizontal");
		if ( inputV!= 0) {
			if(inputV > 0)
				transZ = runSpeed * Time.deltaTime;
			else
				transZ = - runSpeed * Time.deltaTime;
		}
		
		if ( inputH!= 0) {
			if(inputH > 0)
				transX = runSpeed * Time.deltaTime;
			else
				transX = - runSpeed * Time.deltaTime;
		}
		
		controller.Move (transform.TransformDirection(new Vector3 (transX, 0, transZ)));
	}
}
