using UnityEngine;
using System.Collections;

public class StarLogic : MonoBehaviour
{
	public GameObject explosion;   

	// Use this for initialization
	void Start ()
	{
		var rigidBody = GetComponent<Rigidbody2D> ();
		rigidBody.AddForce (new Vector2 (0, 1000));
	}
	
	// Update is called once per frame
	void Update () {
		var screenPos = Camera.main.WorldToViewportPoint(transform.position);
		if (screenPos.y > 1) {
			Destroy(gameObject);
		}

	}

	void OnTriggerEnter2D(Collider2D collision) {
		if(collision.gameObject.tag.Equals("shell")) {
			var shell = collision.gameObject;
			if(shell.GetComponent<ShellLogic>().isAlive) {
			Destroy(shell);
			Destroy (gameObject);
			var particles = ((GameObject)Instantiate(explosion, transform.position, Quaternion.identity)).GetComponent<ParticleSystem>();
			particles.Play();
			}
		}
	}

}
