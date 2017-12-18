using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//ship speed
	public float speed = 15f;
	private float padding = 0f;
	public float projectileSpeed=10f;
	public float fireRate=0.2f;
	public float health = 500;
	public AudioClip fireSound;
	public AudioClip loseSound;
	public AudioClip asteroidDestroyedSound;
	private Color currentColor;
	private Vector3 pos;

	public GameObject projectile;
	public GameObject thrusterPrefab;

	//x limits
	float ymin;
	float ymax;
	float xmax;
	float xmin;

	// Use this for initialization
	void Start () {
		//get z distance from camera to player
		float distance = transform.position.z - Camera.main.transform.position.z;

		//get up limit by calculating in worldpoints from camera view
		Vector3 upmost = Camera.main.ViewportToWorldPoint(new Vector3(0,1,distance));

		//get down limit
		Vector3 downmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));

		//get left limit
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));

		//get right
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
	
		ymax = upmost.y-padding;
		ymin = downmost.y+padding;
		xmax = rightmost.x;
		xmin = leftmost.x;


		//UpdateColor ();
	
	
	}
	
	// Update is called once per frame
	void Update () {

		pos=Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x,Input.GetTouch(0).position.y,0f));
		transform.position = new Vector3 (pos.x, pos.y, 0f);
	



		//MoveWithMouse();

		if (Input.GetMouseButtonUp (0)) {
			InvokeRepeating("FireLaser",0.0001f,fireRate) ;
		}
		if (Input.GetMouseButtonDown (0)) {
			CancelInvoke ("FireLaser");
		}



	}


	//collisions
	void OnTriggerEnter2D(Collider2D collider){


			AudioSource.PlayClipAtPoint (loseSound,transform.position);
			LevelManager man = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
			man.LoadLevel ("End Menu");
			Destroy (gameObject);

	}



	//move function
	void MoveWithMouse(){

		Vector3 mouseCoordinates = new Vector3 (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
		mouseCoordinates *= 0.1f;
		//mouseCoordinates *= Time.deltaTime;
		transform.Translate (mouseCoordinates);

		//restrict player to gamespace
		float newx =Mathf.Clamp (transform.position.x, xmin, xmax);
		float newY = Mathf.Clamp (transform.position.y, ymin, ymax);
		transform.position = new Vector3 (newx, newY, transform.position.z);
	}
 // fire function

	void FireLaser(){
		GameObject beam = Instantiate(projectile, new Vector3(transform.position.x,transform.position.y,1),Quaternion.Euler(0,0,90)) as GameObject;
			beam.GetComponent<Rigidbody2D>().velocity = new Vector3 (projectileSpeed, 0, 0);
			AudioSource.PlayClipAtPoint (fireSound,transform.position);
			
		SpriteRenderer beamSR = beam.GetComponent<SpriteRenderer> ();
		beamSR.color = currentColor;
	}

	void UpdateColor(Color col){
		
		//update color of spaceship
		SpriteRenderer overlaySR = GetComponentInChildren<SpriteRenderer>();
		overlaySR.color = col;
		currentColor = col;
		AudioSource.PlayClipAtPoint (asteroidDestroyedSound,transform.position);

	}


}
