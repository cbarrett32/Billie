using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class AwarenessScript : MonoBehaviour {
	//Can be adjusted so he starts out noticing you for some reason. probably redundant
	private float m_StartingAwareness = 0f;
	//The red bar next to his body acting as a timer
	public Slider m_Slider;
	public GameObject SquashFrog;
	public AwarenessSound awarenesssound;
	public GameObject placeawareHere;
	private AlertedSound alertsound;
	public GameObject placealertHere;
	private bool playSoundOnce = false;

	//JCC is for gameover (currently unused), GameObject is for just the object
	JumpingCharacterController billie;

	private GameObject billieForPosition;
	private Animator m_Animator;

	[HideInInspector]
	public float timeUntilSeesYou;
	//The amount of the red bar
	public float m_CurrentAwareness;
	public bool youDead=false;

	void Start() {
		awarenesssound = GameObject.Find("Awareness").GetComponent<AwarenessSound> ();
		alertsound = GameObject.Find("Alerted").GetComponent<AlertedSound> ();

	}

	// Use this for initialization
	private void Awake () {
		
		billie = GameObject.Find ("Billie").GetComponent<JumpingCharacterController> ();
		billieForPosition = GameObject.Find ("Billie");
		m_CurrentAwareness = m_StartingAwareness;
		//Makes the slider fill up and represent the current amount
		SetAwarenessUI();
		m_Animator = GetComponent<Animator>();

	}

	//Called in EnemyBehaviour. If he notices you, the slider goes up. float amount is always Timer.deltaTime().
	//In EnemyBehaviour, this is called through fixedupdate, so it goes up steadily
	public void Notice (float amount) {
		if (!playSoundOnce) {
			awarenesssound.playSound ();
			playSoundOnce = true;
		}
		m_CurrentAwareness += amount;


		SetAwarenessUI ();

		//If the frogs current awareness is equal or greater than the max time until he notices you, kill you.
		if (m_CurrentAwareness >= timeUntilSeesYou) {
			//Method that does as the name implies
			KillsYou ();
		}
	}

	//Same thing as the Notice Method, but counts down instead of up.
	public void OutOfRange (float amount) {
		if (m_CurrentAwareness >= 0) {
			m_CurrentAwareness -= amount;
			SetAwarenessUI ();
			playSoundOnce = false;
			awarenesssound.stopSound ();
		}

	}

	//makes the slider represent the awareness number
	private void SetAwarenessUI ()
	{
		m_Slider.value = m_CurrentAwareness;
	}
		
	private void KillsYou () {
		//Create a new guard frog above Billie
		if(!youDead)
		{
			m_Animator.SetBool ("youDead", true); 
			//Play trumpet sound
			alertsound.playSound ();
			//Create the Squash frog above Billie's head
			GameObject go = Instantiate(SquashFrog);
			go.transform.position = billieForPosition.transform.position;
			go.transform.Translate (Vector3.up * 25.0f);
			//After the Squash frog is created, don't let billie move anymore.
			billieForPosition.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			//billie.showgameover();
			youDead=true;
		}
		
	}

}
