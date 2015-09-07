using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreLogic : MonoBehaviour {
	protected int mScore;
	protected Text mText;

	public int score {
		get {
			return mScore;
		}
		set {
			mScore = value;
			mText.text = "" + mScore;
		}
	}

	// Use this for initialization
	void Start () {
		mScore = 0;
		mText = GetComponent<Text>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
