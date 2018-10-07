using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBridge : MonoBehaviour {

	public GameObject LowerNode;
	private Vector3 position; 
	private Quaternion rotation;
	private Rigidbody2D rb;
	private bool killVelocityOnce = true;

	//Used for checking if you're on the ground
	public LayerMask groundLayers;
	private float groundCheckRadius;
	public Transform groundCheckPoint;
	public bool isGrounded;
	private bool firstGrounded = true;


	// Use this for initialization
	void Start () {
		groundCheckRadius = .1f; //col.size.x * .5f
		Quaternion zero = Quaternion.Euler (0, 0, 0);
		this.transform.rotation = zero;
		position = LowerNode.GetComponent<Transform> ().position;
		rotation = LowerNode.GetComponent<Transform> ().rotation;
		rb = GetComponent<Rigidbody2D> ();

	}

	// Update is called once per frame
	void Update () {
		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayers);


		if (LowerNode != null) {
			//transform.position = position;

			//transform.position.y = position.y;
			//transform.position.z = 0f;
			transform.position = new Vector3 (position.x, position.y, 0f);

			transform.rotation = rotation;
			//rb.freezeRotation = true;

		}
		else
		{
			if (killVelocityOnce) {
				transform.position = new Vector3 (position.x, position.y, 0f);

				transform.rotation = rotation;
				killVelocityOnce = false;
				rb.angularVelocity = 0f;
				rb.velocity = Vector3.zero;
			}

			if (isGrounded && firstGrounded) {
				firstGrounded = false;
				AudioSource bridgeImpact = gameObject.GetComponent<AudioSource> ();
				bridgeImpact.Play (); 
			}

			//rb.position
			//rb.freezeRotation = false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.name == "Billie") {
				rb.constraints = RigidbodyConstraints2D.FreezePosition;

		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		if (coll.collider.name == "Billie") {
			rb.constraints = RigidbodyConstraints2D.None;
		}

	}

}
