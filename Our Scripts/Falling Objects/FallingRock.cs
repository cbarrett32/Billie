using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour {

	public GameObject LowerNode;
	private Vector3 position; 
	private Quaternion rotation;
	private Rigidbody2D rb;

	//Used for checking if you're on the ground
	public LayerMask groundLayers;
	private float groundCheckRadius;
	public Transform groundCheckPoint;
	public bool isGrounded;
	private bool firstGrounded = true;

	private bool killVelocityOnce = true;

	public float vSpeed;

	private BoxCollider2D bc;
	// Use this for initialization
	void Start () {
		groundCheckRadius = .1f; 
		Quaternion zero = Quaternion.Euler (0, 0, 0);
		this.transform.rotation = zero;
		position = LowerNode.GetComponent<Transform> ().position;
		rotation = LowerNode.GetComponent<Transform> ().rotation;
		rb = GetComponent<Rigidbody2D> ();
		bc = GetComponent<BoxCollider2D> ();
		bc.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		//Draws a small circle near the 'feet' of the rock to see if it's touching the ground
		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayers);
		vSpeed = Input.GetAxis("Horizontal");

		//If the rope still exists, keep making the position of the boulder that of the lower node of the rope.
		if (LowerNode != null) {
			//transform.position = position;

			//transform.position.y = position.y;
			//transform.position.z = 0f;
			transform.position = new Vector3 (position.x, position.y, 0f);
			transform.position = new Vector3 (LowerNode.GetComponent<Transform> ().position.x, 
				LowerNode.GetComponent<Transform> ().position.y, 0f);

			transform.rotation = rotation;
			//rb.freezeRotation = true;

		}
		else
		{
			//The first time through, once the rope is destroyed, make the velocity of the boulder zero. error correct.
			if (killVelocityOnce) {
				killVelocityOnce = false;
				rb.velocity = Vector3.zero;
				rb.angularVelocity = 0f;
				//transform.Translate (Vector3.left * .45f);
				//transform.position = new Vector3 (position.x, position.y, 0f);
				//transform.rotation = rotation;
			}
			//When it hits the ground, make a bump sound
			if (isGrounded && firstGrounded) {
				firstGrounded = false;
				AudioSource boulderImpact = gameObject.GetComponent<AudioSource> ();
				boulderImpact.Play (); 
			}
		}
	}

	//Boulders get stuck to boulders, but destroyed by falling of cliffs.
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Spike") {
			rb.transform.rotation = Quaternion.identity;
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
			bc.enabled = true;
		}
		if (coll.gameObject.tag == "DeadlyThing") {
			//Debug.Log ("fall");
			Destroy(gameObject);
		}

	}

	//...this might not be doing anything.
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.tag == "Spike") {
			rb.constraints = RigidbodyConstraints2D.FreezeAll;
		}
	}

	//Bilie isn't allowed to move the boulder, but she also can't freeze it by touching it while it's moving.
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.name == "Billie") {
			//if (!(vSpeed > .01f) && !(vSpeed < -.01f)) {
				rb.constraints = RigidbodyConstraints2D.FreezePosition;
			//}
		}
	}
		
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.collider.name == "Billie") {
			rb.constraints = RigidbodyConstraints2D.None;
		}
		//also might not be doing anything
		if (coll.collider.tag == "Spike") {
			rb.constraints = RigidbodyConstraints2D.FreezePosition;
		}
	}



}
