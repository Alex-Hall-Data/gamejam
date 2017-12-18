using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSound : MonoBehaviour {

	static buttonSound instance = null;

	public AudioClip start;
	public AudioClip  quit;
	public AudioClip playAgain;

	void Start() {
		if (instance != null && instance != this) {
			Destroy (gameObject);

		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);

		}

	}


	public void playStartSound(){
		AudioSource.PlayClipAtPoint (start, new Vector3(0f,0f,0f));
	}

	public void playQuitSound(){
		AudioSource.PlayClipAtPoint(quit, new Vector3(0f,0f,0f));
	}

	public void playAgainSound(){
		AudioSource.PlayClipAtPoint(playAgain, new Vector3(0f,0f,0f));
	}
}
