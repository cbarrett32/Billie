using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SquashFrog : MonoBehaviour {
	//All used to see if he's grounding
	public bool isGrounded;            // Whether or not the player is grounded.
	public LayerMask groundLayers;
	private float groundCheckRadius;
	public Transform groundCheckPoint;

	private bool firstGrounded = false;

	JumpingCharacterController billie;


	// Use this for initialization
	void Awake() {
		groundCheckRadius = .1f; //col.size.x * .5f
		billie = GameObject.Find ("Billie").GetComponent<JumpingCharacterController> ();


	}
	void Update() {
		//Is Squash Frog touching the ground

		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayers);

		//Once he falls down and hits the ground, its game over.
		if (isGrounded) {
			if (firstGrounded == false) {
				AudioSource squashFrogCrash = gameObject.GetComponent<AudioSource> ();
				squashFrogCrash.Play ();
				firstGrounded = true;
			}
			billie.showgameover();
			//Debug.Log ("game over");
		}
	}
	void OnTriggerEnter2D(Collider2D coll)
	{
		//When squash frog hits billie, kill her (but not actually, only make it look like it by moving her to a different layer
		if (coll.gameObject.name == "Billie") {
			//Destroy (coll.gameObject);
			Debug.Log("hits billie");
			//billie.squashFrogStops = true;
			billie.GetComponent<SortingGroup>().sortingLayerName = "All the way Lvl 1";
			StartCoroutine (lastResort());
		}


	}

	IEnumerator lastResort() {
		yield return new WaitForSeconds (.3f);
		AudioSource squashFrogCrash = gameObject.GetComponent<AudioSource> ();
		squashFrogCrash.Play ();
		billie.showgameover ();
	}
}
