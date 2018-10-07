using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootDirection : MonoBehaviour {
	public GameObject billie;
	public Sprite shoot14;
	public Sprite shoot13;
	public Sprite shoot12;
	public Sprite shoot11;
	public Sprite shoot10;
	public Sprite shoot9;
	public Sprite shoot8;
	public Sprite shoot7;
	public Sprite shoot6;
	public Sprite shoot5;
	public Sprite shoot4;
	public Sprite shoot3;
	public Sprite shoot2;
	public Sprite shoot1;


	public GameObject bulletPrefab;
	public float velocity = 500.0f;
	private Vector3 m_pos;
	UpdateCursor_crossbow_button crossbow;
	private Animator animator;

	// Use this for initialization
	void Start () {
		crossbow = GameObject.Find ("Crossbow").GetComponent<UpdateCursor_crossbow_button> ();
		animator = GameObject.Find("Billie").GetComponent<Animator>();
	}

	// Update is called once per frame
	void LateUpdate () {
		if (crossbow.carrying && animator.GetCurrentAnimatorStateInfo(0).IsName("Crossbow")) {
			Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			diff.Normalize ();

			float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler (0f, 0f, rot_z); 

			if (rotation.eulerAngles.z >= 0 && rotation.eulerAngles.z < 6) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot1;
				animator.SetInteger ("crossbow_angle", 1);
			} else if (rotation.eulerAngles.z >= 6 && rotation.eulerAngles.z < 12) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot2;
				animator.SetInteger ("crossbow_angle", 1);
			} else if (rotation.eulerAngles.z >= 12 && rotation.eulerAngles.z < 18) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot3;
				animator.SetInteger ("crossbow_angle", 3);
			} else if (rotation.eulerAngles.z >= 18 && rotation.eulerAngles.z < 24) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot4;
				animator.SetInteger ("crossbow_angle", 3);
			} else if (rotation.eulerAngles.z >= 24 && rotation.eulerAngles.z < 30) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot5;
				animator.SetInteger ("crossbow_angle", 5);
			} else if (rotation.eulerAngles.z >= 30 && rotation.eulerAngles.z < 36) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot6;
				animator.SetInteger ("crossbow_angle", 5);
			} else if (rotation.eulerAngles.z >= 36 && rotation.eulerAngles.z < 42) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot7;
				animator.SetInteger ("crossbow_angle", 7);
			} else if (rotation.eulerAngles.z >= 42 && rotation.eulerAngles.z < 48) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot8;
				animator.SetInteger ("crossbow_angle", 7);
			} else if (rotation.eulerAngles.z >= 48 && rotation.eulerAngles.z < 54) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot9;
				animator.SetInteger ("crossbow_angle", 9);
			} else if (rotation.eulerAngles.z >= 54 && rotation.eulerAngles.z < 60) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot10;
				animator.SetInteger ("crossbow_angle", 9);
			} else if (rotation.eulerAngles.z >= 60 && rotation.eulerAngles.z < 66) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot11;
				animator.SetInteger ("crossbow_angle", 11);
			} else if (rotation.eulerAngles.z >= 66 && rotation.eulerAngles.z < 72) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot12;
				animator.SetInteger ("crossbow_angle", 11);
			} else if (rotation.eulerAngles.z >= 72 && rotation.eulerAngles.z < 78) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot13;
				animator.SetInteger ("crossbow_angle", 14);
			} else if (rotation.eulerAngles.z >= 78 && rotation.eulerAngles.z < 270) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot14;
				animator.SetInteger ("crossbow_angle", 14);
			} else if (rotation.eulerAngles.z >= 270 && rotation.eulerAngles.z < 360) {
				billie.GetComponent<SpriteRenderer> ().sprite = shoot1;
				animator.SetInteger ("crossbow_angle", 1);
			}
		}

	}

	void Update () {
		if (crossbow.carrying) {
			Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
			diff.Normalize ();

			float rot_z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
			Quaternion rotation = Quaternion.Euler (0f, 0f, rot_z); 

			if (rotation.eulerAngles.z >= 0 && rotation.eulerAngles.z < 6) {
				animator.SetInteger ("crossbow_angle", 1);
			} else if (rotation.eulerAngles.z >= 6 && rotation.eulerAngles.z < 12) {
				animator.SetInteger ("crossbow_angle", 1);
			} else if (rotation.eulerAngles.z >= 12 && rotation.eulerAngles.z < 18) {
				animator.SetInteger ("crossbow_angle", 3);
			} else if (rotation.eulerAngles.z >= 18 && rotation.eulerAngles.z < 24) {
				animator.SetInteger ("crossbow_angle", 3);
			} else if (rotation.eulerAngles.z >= 24 && rotation.eulerAngles.z < 30) {
				animator.SetInteger ("crossbow_angle", 5);
			} else if (rotation.eulerAngles.z >= 30 && rotation.eulerAngles.z < 36) {
				animator.SetInteger ("crossbow_angle", 5);
			} else if (rotation.eulerAngles.z >= 36 && rotation.eulerAngles.z < 42) {
				animator.SetInteger ("crossbow_angle", 7);
			} else if (rotation.eulerAngles.z >= 42 && rotation.eulerAngles.z < 48) {
				animator.SetInteger ("crossbow_angle", 7);
			} else if (rotation.eulerAngles.z >= 48 && rotation.eulerAngles.z < 54) {
				animator.SetInteger ("crossbow_angle", 9);
			} else if (rotation.eulerAngles.z >= 54 && rotation.eulerAngles.z < 60) {
				animator.SetInteger ("crossbow_angle", 9);
			} else if (rotation.eulerAngles.z >= 60 && rotation.eulerAngles.z < 66) {
				animator.SetInteger ("crossbow_angle", 11);
			} else if (rotation.eulerAngles.z >= 66 && rotation.eulerAngles.z < 72) {
				animator.SetInteger ("crossbow_angle", 11);
			} else if (rotation.eulerAngles.z >= 72 && rotation.eulerAngles.z < 78) {
				animator.SetInteger ("crossbow_angle", 14);
			} else if (rotation.eulerAngles.z >= 78 && rotation.eulerAngles.z < 270) {
				animator.SetInteger ("crossbow_angle", 14);
			} else if (rotation.eulerAngles.z >= 270 && rotation.eulerAngles.z < 360) {
				animator.SetInteger ("crossbow_angle", 1);
			}
		}

	}

	public void Shoot()
	{

		//This one makes it rotate slightly off, and not move the correct direction
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.Euler(0f, 0f, rot_z); 



		//find mouse position
		var pos = Input.mousePosition;
		pos.z = transform.position.z - Camera.main.transform.position.z;
		pos = Camera.main.ScreenToWorldPoint(pos);

		AudioSource CrossbowC = gameObject.GetComponent<AudioSource> ();
		CrossbowC.volume = Random.Range (0.3f, 0.5f);
		CrossbowC.pitch = Random.Range (0.95f, 1.05f);
		CrossbowC.Play ();

		//find angle between mouse position and you
		var q = Quaternion.FromToRotation(Vector3.up, pos - transform.position);
		//Instantiate new bolt at crossbow position
		GameObject go = Instantiate(bulletPrefab, transform.position, q);
		//shrink bolt
		go.transform.localScale = new Vector3 (.3f, .3f, .3f);
		Rigidbody2D rb = go.GetComponent<Rigidbody2D> ();
		rb.AddForce(go.transform.up * velocity);
	}
}
