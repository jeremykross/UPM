using UnityEngine;
using System.Collections;

public class StartInsaneMode : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Collider2D collider = Physics2D.OverlapPoint(worldPoint);
		if(collider.gameObject == this.gameObject) {
			GlobalLogic.Preferences.Add("DIFFICULTY", "INSANE");
			Application.LoadLevel("gamescene");
		}
	}
}
