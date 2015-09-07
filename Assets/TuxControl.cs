using UnityEngine;
using System.Collections;

public class TuxControl : MonoBehaviour
{
	// Use this for initialization
	public GameObject star;
	public Color defaultColor;
	public int hitCount;
	public bool reviving = false;

	void Start ()
	{
		var bottomCenter = Camera.main.ViewportToWorldPoint (new Vector3 (0.5f, 0, 1));
		var halfHeight = this.GetComponent<SpriteRenderer> ().sprite.bounds.extents.y;
		this.transform.position = new Vector3 (bottomCenter.x, bottomCenter.y + halfHeight * this.transform.lossyScale.y, bottomCenter.z);

		defaultColor = Camera.main.backgroundColor;

	}

	// Update is called once per frame
	void Update ()
	{

		if(reviving) return;

        Vector2 touchPos = Input.mousePosition;

        if(Input.touchSupported) {
            if(Input.touchCount > 0) {
                touchPos = Input.GetTouch(0).position;
            } else {
                return;
            }
        }

        var mousePosWorld = Camera.main.ScreenToWorldPoint (touchPos);
        var mousePosViewport = Camera.main.ScreenToViewportPoint (touchPos);
        var toTouch = mousePosWorld - transform.position;

        if (mousePosViewport.y > 0.5f) { 
            if(Input.GetMouseButtonDown (0)) {
                Instantiate (star, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
        } else if (toTouch.magnitude > .5) {
            transform.Translate (toTouch.normalized.x * Time.deltaTime * 20, 0, 0);
        } 
       
	}

	public void OnTriggerEnter2D(Collider2D collision) {
		var other = collision.gameObject;
		if(other.tag.Equals("shell") && !reviving) {
			ShellLogic shellLogic = other.GetComponent<ShellLogic>();
			if(shellLogic.isAlive) {
			GlobalLogic logic = GameObject.Find("Root").GetComponent<GlobalLogic>();
			hitCount++;
			StartCoroutine(hit ());
			}
		}
	}

	public IEnumerator zoomTo(float zoomInBy, Vector3 zoomTo) {
		var oldPos = Camera.main.transform.position;
		var zoomInDegree = .1f;
		var startOrthoSize = Camera.main.orthographicSize;
		for(float zoomed=0f; zoomed<1f; zoomed+=zoomInDegree) {
			Camera.main.transform.position = 
				Vector3.Lerp(oldPos, (oldPos + zoomTo), zoomed);
			Camera.main.orthographicSize = startOrthoSize + zoomed * zoomInBy;
			yield return null;
		}
	}

	public IEnumerator tapCount(int count) {
		var timeOut = 5.0f;

		var defaultBG = Camera.main.backgroundColor;

		while(timeOut > 0.0f && timeOut < 10.0f) {
			timeOut += Time.unscaledDeltaTime * (hitCount/2);

			Camera.main.backgroundColor = Color.Lerp(defaultBG, Color.red, timeOut/10.0f);

			if(Input.GetMouseButtonDown(0)) {
				timeOut -= 1f;
			}

			yield return null;
		}

		if(timeOut >= 10.0f) {
			Time.timeScale = 1;
			Application.LoadLevel("StartScreen");
		}
	}

	public IEnumerator hit() {
		reviving = true;
		Camera.main.backgroundColor = Color.black;
		Time.timeScale = 0;
		yield return StartCoroutine(zoomTo (-3f, new Vector3(transform.position.x, -3f, 0)));
		yield return StartCoroutine(tapCount((int)Mathf.Pow(hitCount, 2)));
		yield return StartCoroutine(zoomTo (3f, new Vector3(-transform.position.x, 3f, 0))); 
		Time.timeScale = 1;
		Camera.main.backgroundColor = defaultColor;
		reviving = false;

		foreach(GameObject shell in GameObject.FindGameObjectsWithTag("shell")) {
			ShellLogic logic = shell.GetComponent<ShellLogic>();
			logic.isAlive = false;
		}
	}
}
