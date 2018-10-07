using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using Gamekit2D;
using Kilt.EasyRopes2D;

public class Detect_click : MonoBehaviour {
	public GameObject canvas;

	UpdateCursor_lever_button lever;
	UpdateCursor_ramp_button ramp;
	UpdateCursor_wedge_button wedge;
	UpdateCursor_eraser_button eraser;
	UpdateCursor_crossbow_button crossbow;
	HandleCursor cursor;
	WedgePlank plank;
	plankz1 pulleyplank1;
	plankz2 pulleyplank2;
	ShootDirection shoot;
	UpdateCursor_pulley_button pulley;


	public Tilemap tm;
	public Sprite ill_lever;
	public Sprite ill_ramp;
	public Sprite ill_wedge;

	string selected;

	public bool can_place_lever;
	public bool can_place_ramp;
	public bool can_place_wedge;
	public bool can_place_crossbow;
	public bool can_place_pulley;
	public bool can_erase;
	bool help_rotate;
	public bool hitramp;
	public bool hitlever;
	bool hitspike;
	public bool donot_rotate;
	public bool cont;
	public bool first_shoot = true;
	public bool first_erase = true;
	Vector2 pprightPos;
	Vector2 ppleftPos;

	// object to be placed:
	GameObject object_lever;
	int lever_i = 0;
	GameObject object_ramp;
	int ramp_i = 0;
	GameObject object_wedge;
	int wedge_i = 0;
	GameObject object_pulley;
	public int pulley_i = 0;

	GameObject panel_rotate;
	int softreset;

	private Vector3 bg_pos;
	private Vector3 m_pos;


	private Animator animator;


	// Use this for initialization
	void Start () {
		// objects to be placed

		object_lever = GameObject.Find ("lever_placed");
		object_ramp = GameObject.Find ("ramp_placed");
		object_wedge = GameObject.Find ("Wedge_placed");
		object_pulley = GameObject.Find("pulley_placed");

		// scripts attached to inventories:
		lever = GameObject.Find ("Lever").GetComponent<UpdateCursor_lever_button> ();
		ramp = GameObject.Find ("Ramp").GetComponent<UpdateCursor_ramp_button> ();
		wedge = GameObject.Find ("Wedge").GetComponent<UpdateCursor_wedge_button> ();
		eraser = GameObject.Find ("Eraser").GetComponent<UpdateCursor_eraser_button> ();
		crossbow = GameObject.Find ("Crossbow").GetComponent<UpdateCursor_crossbow_button> ();
		plank = GameObject.Find ("PlankSprite").GetComponent<WedgePlank> ();
		pulleyplank1 = GameObject.Find ("PlankRight").GetComponent<plankz1> ();
		pulleyplank2 = GameObject.Find ("PlankLeft").GetComponent<plankz2> ();
		shoot = GameObject.Find ("Gun Location").GetComponent<ShootDirection> ();
		pulley = GameObject.Find ("Pulley").GetComponent<UpdateCursor_pulley_button> ();

		// cursor:
		cursor = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<HandleCursor> ();

		pprightPos = pulleyplank1.transform.position;
		ppleftPos = pulleyplank2.transform.position;

		// help box:
		panel_rotate = GameObject.Find ("help_rotate");
		panel_rotate.SetActive (false);
		// 1: has soft reset; 0: new game
		softreset = PlayerPrefs.GetInt("reset");

		// animator:
		animator = GameObject.Find("Billie").GetComponent<Animator>();

	}

	
	// Update is called once per frame
	void Update () {
		
		if (EventSystem.current.currentSelectedGameObject != null)
		{
			// detect which inventory button is selected [lever? ramp? pulley? wedge? eraser?]
			if (EventSystem.current.currentSelectedGameObject.name == "Lever" && lever.carrying) {
				lever.SetLever ();
				selected = "Lever";
			} else if (EventSystem.current.currentSelectedGameObject.name == "Ramp" && ramp.carrying) {
				ramp.SetRamp ();
				selected = "Ramp";
			} else if (EventSystem.current.currentSelectedGameObject.name == "Wedge" && wedge.carrying) {
				wedge.SetWedge ();
				selected = "Wedge";
			} else if (EventSystem.current.currentSelectedGameObject.name == "Crossbow" && crossbow.carrying) {
				crossbow.SetCrosshair ();
				selected = "Crossbow";
			} else if (EventSystem.current.currentSelectedGameObject.name == "Eraser" && eraser.carrying) {
				eraser.SetEraser ();
				selected = "Eraser";
			} else if (EventSystem.current.currentSelectedGameObject.name == "Pulley" && pulley.carrying) {
				pulley.SetPulley ();
				selected = "Pulley";
			} else {
				cursor.setMouse ();
			}
		}


		// do actions
		place ();



	}

	bool legalPlacement(Vector3 position) {
		//Debug.Log ("Mouse Position: " + position.x + " Left plank pos: " + ppleftPos.x +  " Right plank pos: " + pprightPos.x);

		if (true) {
			if (position.x < ppleftPos.x || position.x > (pprightPos.x + 4.0f)) {
				return true;
			} else {

				return false;
			}
		} /*else {
			if (position.x < ppleftPos.x || position.x > (pprightPos.x + 4.0f)) {
				return false;
			} else {

				return true;
			}
		}*/
	}

	void place() {
		if (selected == "Lever") {
			// when deselecting the object from the inventory, disable the last "else if"
			if (lever.can_place_to_false) {
				can_place_lever = false;
			}

			// when selecting the object from the inventory
			else if (lever.ready_to_place && !can_place_lever) {
				can_place_lever = true;

			} 

			// after selection, place the lever object to where the cursor points to
			else if (lever.ready_to_place && can_place_lever) {
				m_pos = Input.mousePosition;
				m_pos.z = 10;
				m_pos = Camera.main.ScreenToWorldPoint (m_pos);


				Vector3Int newPos = new Vector3Int ((int)m_pos.x, (int)m_pos.y, 0);
				Vector3Int cellPosition = tm.WorldToCell (newPos);

				RaycastHit2D hit = Physics2D.Raycast (m_pos, Vector2.zero);

				if (hit.collider != null) {
					if (hit.collider.tag == "simplemachine") {
						hitlever = true;
						donot_rotate = true;
					}
					if (hit.collider.tag == "SpikeTrigger") {
						hitspike = true;
					}
				} else {
					hitlever = false;
					donot_rotate = false;
					hitspike = false;
				}

				if (!tm.HasTile (cellPosition) && legalPlacement (m_pos) && !hitlever && !hitspike) {
					if (Input.GetMouseButtonDown (0)) {
						if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.tag == "SimpleMachineButton" && EventSystem.current.currentSelectedGameObject.name != "Lever") {
							BackToDeselect ();
						} else {
							GameObject leveri = Instantiate (object_lever, m_pos, transform.rotation, canvas.transform);

							int usage = PlayerPrefs.GetInt ("usage");
							PlayerPrefs.SetInt ("usage", usage + 1);

							leveri.name = "lever" + lever_i.ToString ();
							lever_i += 1;
							lever.ready_to_place = false;
							lever.carrying = false;
							can_place_lever = false;
						}
					}
				} else {
					cursor.setIllLever ();
					if (Input.GetMouseButtonDown (0)) {
						ResetLever ();
					}
				}
			}
		} 

		if (selected == "Pulley") {
			// when deselecting the object from the inventory, disable the last "else if"
			if (pulley.can_place_to_false) {
				can_place_pulley = false;
			}

			// when selecting the object from the inventory
			else if (pulley.ready_to_place && !can_place_pulley) {
				can_place_pulley = true;

			} 

			// after selection, place the pulley object to where the cursor points to
			else if (pulley.ready_to_place && can_place_pulley ) {
				if (Input.GetMouseButtonDown (0)) {
					if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.tag == "SimpleMachineButton" && EventSystem.current.currentSelectedGameObject.name != "Pulley") {
						BackToDeselect ();
					} else {
						cont = true;
						m_pos = Input.mousePosition;
						m_pos.z = 10;
						m_pos = Camera.main.ScreenToWorldPoint (m_pos);


						Vector3Int newPos = new Vector3Int ((int)m_pos.x, (int)m_pos.y, 0);
						Vector3Int cellPosition = tm.WorldToCell (newPos);
						// instantiate when the player is not clicking on the tile
						if (!tm.HasTile (cellPosition) && !legalPlacement (m_pos)) {
							GameObject pulleyi = Instantiate (object_pulley, m_pos, transform.rotation);

							int usage = PlayerPrefs.GetInt ("usage");
							PlayerPrefs.SetInt ("usage", usage + 1);

							pulleyi.name = "pulley" + pulley_i.ToString ();
							foreach (Transform child in pulleyi.transform) {
								child.name = child.name + pulley_i.ToString ();
								foreach (Transform child1 in child.transform) {
									child1.name = child1.name + pulley_i.ToString ();
									foreach (Transform child2 in child1.transform) {
										child2.name = child2.name + pulley_i.ToString ();
										foreach (Transform child3 in child2.transform) {
											child3.name = child3.name + pulley_i.ToString ();
											foreach (Transform child4 in child3.transform) {
												child4.name = child4.name + pulley_i.ToString ();
											}
										}
									}
								}
							}
							pulley_i += 1;
						} 
					
						// if clicking on the tile... change color?
						else {
						
						}
					

						// after placing, back to normal nothing-selected stage
						pulley.ready_to_place = false;
						pulley.carrying = false;
						can_place_pulley = false;
					}
				}
			}
		} 


		else if (selected == "Ramp") {
			if (ramp.can_place_to_false) {
				can_place_ramp = false;
			}
				
			else if (ramp.ready_to_place && !can_place_ramp) {
				can_place_ramp = true;

			} 
				
			else if (ramp.ready_to_place && can_place_ramp) {

				m_pos = Input.mousePosition;
				m_pos.z = 10;
				m_pos = Camera.main.ScreenToWorldPoint (m_pos);

				if (!help_rotate) {
					int hasClosed3 = PlayerPrefs.GetInt ("closed_rotate");
					if (hasClosed3 == 0) {
						// show helpbox
						if (panel_rotate != null) {
							panel_rotate.SetActive (true);
							float ttl = 4.0f;
							Destroy (panel_rotate, ttl);
							help_rotate = true;
						}
					}
				}

				Vector3Int newPos = new Vector3Int ((int)m_pos.x, (int)m_pos.y, 0);
				Vector3Int cellPosition = tm.WorldToCell (newPos);

				RaycastHit2D hit = Physics2D.Raycast (m_pos, Vector2.zero);

				if (hit.collider != null) {
					if (hit.collider.tag == "simplemachine") {
						hitramp = true;
						donot_rotate = true;
					}
					if (hit.collider.tag == "SpikeTrigger") {
						hitspike = true;
					}
				} else {
					hitramp = false;
					donot_rotate = false;
					hitspike = false;
				}

				if (!tm.HasTile (cellPosition) && legalPlacement (m_pos) && !hitramp && !hitspike) {

					if (Input.GetMouseButtonDown (0)) {

						if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Crossbow") {
							ResetRamp ();
							CrossbowSelected ();
						} else {

							GameObject rampi = Instantiate (object_ramp, m_pos, transform.rotation, canvas.transform);

							int usage = PlayerPrefs.GetInt ("usage");
							PlayerPrefs.SetInt ("usage", usage + 1);

							rampi.transform.name = "ramp" + ramp_i.ToString ();
							ramp_i += 1;

							ramp.ready_to_place = false;
							ramp.carrying = false;
							can_place_ramp = false;
						}
					}
				} else {
					cursor.setIllRamp ();
					if (Input.GetMouseButtonDown (0)) {
						ResetRamp ();
					}
				}
			}

		}

		else if (selected == "Wedge") {
			
			if (wedge.can_place_to_false) {
				can_place_wedge = false;
			}

			else if (wedge.ready_to_place && !can_place_wedge) {
				can_place_wedge = true;
			} 

			else if (wedge.ready_to_place && can_place_wedge) {
				m_pos = Input.mousePosition;
				m_pos.z = 10;
				m_pos = Camera.main.ScreenToWorldPoint (m_pos);

				Vector2 plankpos = plank.transform.position;
				//plank placement
				//where we want the wedge to be placed
				Vector2 adjustedPlace = new Vector2(plankpos.x -1.95f, plankpos.y +1.68f);
				//and rotated
				Quaternion adjustedAngle = Quaternion.Euler (0, 0, 240);
				if (Vector2.Distance (plankpos, m_pos) < 10) {
					if (Input.GetMouseButtonDown (0)) {
						if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.tag == "SimpleMachineButton" && EventSystem.current.currentSelectedGameObject.name != "Wedge") {
							BackToDeselect ();

						} else {
							GameObject wedgei = Instantiate (object_wedge, adjustedPlace, adjustedAngle, canvas.transform);

							int usage = PlayerPrefs.GetInt ("usage");
							PlayerPrefs.SetInt ("usage", usage + 1);

							wedgei.name = "wedge" + wedge_i.ToString ();
							wedge_i += 1;
							wedge.ready_to_place = false;
							wedge.carrying = false;
							can_place_wedge = false;
						}
					}

				} else {
					cursor.setIllWedge ();
				}
			}
		}

		else if (selected == "Crossbow") {
			if (crossbow.can_place_to_false) {
				can_place_crossbow = false;
				crossbow.ready_to_place = false;

			}

			else if (crossbow.ready_to_place && !can_place_crossbow) {
				can_place_crossbow = true;

			} 

			else if (crossbow.ready_to_place && can_place_crossbow) {
				crossbow.SetCrosshair ();
				if (Input.GetMouseButtonDown (0)) {
					
					if (first_shoot) {
						first_shoot = false;
					}

					if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Eraser") {
						ResetCrossbow ();
						EraserSelected ();
					} else if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Pulley") {
						ResetCrossbow ();
						PulleySelected ();
					} else if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Wedge") {
						ResetCrossbow ();
						WedgeSelected ();
					} else if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Ramp") {
						ResetCrossbow ();
						RampSelected ();
					} else if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.name == "Lever") {
						ResetCrossbow ();
						LeverSelected ();
					}


					else {
						shoot.Shoot (); 
					}
//					crossbow.ready_to_place = false;
//					crossbow.carrying = false;
//					can_place_crossbow = false;
//					animator.SetBool ("carry_crossbow", false);
				}
			}

		}


		else if (selected == "Eraser") {
			if (eraser.can_place_to_false) {
				eraser.ready_to_place = false;
				can_erase = false;
				donot_rotate = false;

			}

			else if (eraser.ready_to_place && !can_erase) {
				can_erase = true;
			} 

			else if (eraser.ready_to_place && can_erase && !donot_rotate) {
				// tell the RotateObjectTowards script to not rotate, when trying to erase.
				donot_rotate = true;
			}
			else if (eraser.ready_to_place && can_erase && donot_rotate) {
				eraser.SetEraser ();
				// if trying to erase a simple machine (with button attached)
				if (EventSystem.current.currentSelectedGameObject != null) {
					if (EventSystem.current.currentSelectedGameObject.name == "Crossbow") {
						ResetEraser ();
						CrossbowSelected ();
					} else if (EventSystem.current.currentSelectedGameObject.name == "Pulley") {
						ResetEraser ();
						PulleySelected ();
					} else if (EventSystem.current.currentSelectedGameObject.name == "Wedge") {
						ResetEraser ();
						WedgeSelected ();
					} else if (EventSystem.current.currentSelectedGameObject.name == "Ramp") {
						ResetEraser ();
						RampSelected ();
					} else if (EventSystem.current.currentSelectedGameObject.name == "Lever") {
						ResetEraser ();
						LeverSelected ();
					}

					else if (EventSystem.current.currentSelectedGameObject.name == "inventory_ui") {
						if (first_erase) {
							first_erase = false;
						}
						if (EventSystem.current.currentSelectedGameObject.transform.parent != null) {
							Destroy (EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
							//GameObject.Find (EventSystem.current.currentSelectedGameObject.transform.parent.name).SetActive (false);
						}

						int usage = PlayerPrefs.GetInt ("usage");
						PlayerPrefs.SetInt ("usage", usage - 1);

						AudioSource ObjectErase = gameObject.GetComponent<AudioSource> ();
						ObjectErase.Play ();

//						eraser.ready_to_place = false;
//						eraser.carrying = false;
//						can_erase = false;
//						donot_rotate = false;

					} 
				} 
				// else, treat it as a false click
				else {
					if (Input.GetMouseButtonDown (0)) {
//						eraser.ready_to_place = false;
//						eraser.carrying = false;
//						can_erase = false;
//						donot_rotate = false;
					}
				}
			}
		}




		

	}


	public void BackToDeselect() {
		lever.ready_to_place = false;
		lever.carrying = false;
		can_place_lever = false;

		pulley.ready_to_place = false;
		pulley.carrying = false;
		can_place_pulley = false;

		ramp.ready_to_place = false;
		ramp.carrying = false;
		can_place_ramp = false;

		wedge.ready_to_place = false;
		wedge.carrying = false;
		can_place_wedge = false;

		can_place_crossbow = false;
		crossbow.ready_to_place = false;
		crossbow.carrying = false;
		animator.SetBool ("carry_crossbow", false);

		eraser.ready_to_place = false;
		can_erase = false;
		eraser.carrying = false;

	}

	void ResetCrossbow() {
		crossbow.can_place_to_false = true;
		can_place_crossbow = false;
		crossbow.ready_to_place = false;
		crossbow.carrying = false;
		animator.SetBool ("carry_crossbow", false);
	}

	void CrossbowSelected() {
		crossbow.carrying = true;
		crossbow.can_place_to_false = false;
		crossbow.ready_to_place = true;
		can_place_crossbow = true;
		animator.SetBool ("carry_crossbow", true);
	}


	void ResetEraser() {
		eraser.can_place_to_false = true;
		eraser.ready_to_place = false;
		can_erase = false;
		eraser.carrying = false;
	}

	void EraserSelected() {
		eraser.carrying = true;
		eraser.can_place_to_false = false;
		eraser.ready_to_place = true;
		can_erase = true;
		donot_rotate = false;
	}


	void ResetRamp() {
		ramp.ready_to_place = false;
		can_place_ramp = false;
		ramp.carrying = false;
		ramp.can_place_to_false = true;
		hitramp = false;
		hitspike = false;
		donot_rotate = false;
	}

	void RampSelected() {
		ramp.carrying = true;
		ramp.can_place_to_false = false;
		ramp.ready_to_place = true;
		can_place_ramp = true;
	}


	void ResetLever() {
		lever.ready_to_place = false;
		can_place_lever = false;
		lever.carrying = false;
		lever.can_place_to_false = true;
	}

	void LeverSelected() {
		lever.carrying = true;
		lever.can_place_to_false = false;
		lever.ready_to_place = true;
		can_place_lever = true;
	}


	void ResetPulley() {
		pulley.ready_to_place = false;
		can_place_pulley = false;
		pulley.carrying = false;
		pulley.can_place_to_false = true;
	}

	void PulleySelected() {
		pulley.carrying = true;
		pulley.can_place_to_false = false;
		pulley.ready_to_place = true;
		can_place_pulley = true;
	}

	void ResetWedge() {
		wedge.ready_to_place = false;
		can_place_wedge = false;
		wedge.carrying = false;
		wedge.can_place_to_false = true;
	}

	void WedgeSelected() {
		wedge.carrying = true;
		wedge.can_place_to_false = false;
		wedge.ready_to_place = true;
		can_place_wedge = true;
	}


				
}
