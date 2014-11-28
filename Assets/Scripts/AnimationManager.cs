using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	public AnimationClip aniIdle;
	public AnimationClip aniRun;
	public GameObject mainModel;
	// Use this for initialization
	void Start () {
		Idle ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Idle(){
		mainModel.animation.CrossFade (aniIdle.name);

	}

	public void Run(){
		mainModel.animation.CrossFade (aniRun.name);

	}
}
