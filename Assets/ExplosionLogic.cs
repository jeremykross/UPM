using UnityEngine;
using System.Collections;

public class ExplosionLogic : MonoBehaviour {
	public ParticleSystem ps;
	public GameObject score;
	public TextMesh comboCounter;
	public CircleCollider2D bounds;

    public bool isAlive = true;

	protected float startTime; 
	protected float duration = 0.1f;
	protected float startRadius = 2.5f;
	protected float endRadius = 0f;

	public int ComboCount = 1;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
		bounds = GetComponent<CircleCollider2D>();

		GameObject root = GameObject.Find("Root");
		GlobalLogic logic = root.GetComponent<GlobalLogic>();

		logic.IncrementScore(10 * ComboCount);

		TextMesh comboCounterMesh = (TextMesh)Instantiate(comboCounter, transform.position, Quaternion.identity);
		comboCounterMesh.text = "" + ComboCount;

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(ps != null && !ps.IsAlive()) {
			Destroy(ps.gameObject);
		}

		float elapsed = Time.time - startTime;
		float ratio = elapsed/duration;

		bounds.radius = startRadius + (endRadius - startRadius)*ratio;
        if (bounds.radius < .001) {
            isAlive = false;
        }
	}
}
