using UnityEngine;
using System.Collections;

public class ComboCounterLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var physics = GetComponent<Rigidbody2D>();
		physics.AddForce(new Vector2(0, 200 + Random.value * 200));
		physics.angularVelocity = 1000.0f;
	}
	
	// Update is called once per frame
	void Update () {
		var viewportPos = Camera.main.WorldToViewportPoint(transform.position);
		if(viewportPos.y < 0) {
			Destroy(gameObject);
		}
	}
}
