using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class IdleFrog : MonoBehaviour
{
	private AlertedSound alertsound;
	private FrogDeath frogdeath;

	public LayerMask groundedLayerMask;
	public float groundedRaycastDistance = 0.1f;

	//Start CharacterController2D
	Rigidbody2D m_Rigidbody2D;
	CapsuleCollider2D m_Capsule;
	Vector2 m_PreviousPosition;
	Vector2 m_CurrentPosition;
	Vector2 m_NextMovement;
	ContactFilter2D m_ContactFilter;
	RaycastHit2D[] m_HitBuffer = new RaycastHit2D[5];
	RaycastHit2D[] m_FoundHits = new RaycastHit2D[3];
	Collider2D[] m_GroundColliders = new Collider2D[3];
	Vector2[] m_RaycastPositions = new Vector2[3];

	Rigidbody2D myRigidbody;
	AwarenessScript affectSlider;

	public LayerMask groundLayers;
	private float groundCheckRadius;
	public Transform groundCheckPoint;
	public bool isGrounded;
	public bool IsCeilinged { get; protected set; }
	public Vector2 Velocity { get; protected set; }
	public Rigidbody2D Rigidbody2D { get { return m_Rigidbody2D; } }
	public Collider2D[] GroundColliders { get { return m_GroundColliders; } }
	public ContactFilter2D ContactFilter { get { return m_ContactFilter; } }
	//End CharacterController2D


	static Collider2D[] s_ColliderCache = new Collider2D[16];

	public Vector3 moveVector { get { return m_MoveVector; } }
	public Transform Target { get { return m_Target; } }

	public bool spriteFaceLeft = false;

	//public float speed;
	public float gravity = 10.0f;

	private bool shouldBeCountingDown = true;

	[Range(0.0f,360.0f)]
	private float viewDirection = 0.0f;
	[Range(0.0f, 360.0f)]
	public float viewFov;
	//How far away you are before he sees you.
	public float viewDistance = 5.0f;
	//no longer used i believe
	private float timeBeforeTargetLost = 3.0f;

	//How big is pacing distance is
	public float walkDistance = 10.0f;
	private float timeUntilTurnAround;
	private bool turnAround = false;

	protected SpriteRenderer m_SpriteRenderer;
	protected Collider2D m_Collider;
	protected Animator m_Animator;

	protected Vector3 m_MoveVector;
	protected Transform m_Target;
	protected float m_TimeSinceLastTargetView;

	private Vector3 position;


	//as we flip the sprite instead of rotating/scaling the object, this give the forward vector according to the sprite orientation
	protected Vector2 m_SpriteForward;
	protected Bounds m_LocalBounds;
	protected Vector3 m_LocalDamagerPosition;

	protected RaycastHit2D[] m_RaycastHitCache = new RaycastHit2D[8];
	protected ContactFilter2D m_Filter;

	public float timeUntilSeesYou;

	protected bool m_Dead = false;
	private bool pacing = true;

	//Mine
	protected bool initSpotted = false;
	protected bool sliderDown;
	private GameObject billie;

	JumpingCharacterController billiejump;

	public GameObject SquashFrog;
	private bool playSoundOnce = true;



	private void Awake()
	{
		m_Animator = GetComponent<Animator>();
		myRigidbody = gameObject.GetComponent<Rigidbody2D> ();
		affectSlider = myRigidbody.GetComponent<AwarenessScript> ();
		affectSlider.m_Slider.maxValue = timeUntilSeesYou;
		affectSlider.timeUntilSeesYou = timeUntilSeesYou;

		timeUntilTurnAround = walkDistance;
		m_MoveVector = new Vector3 (3, 0);
		m_Collider = GetComponent<Collider2D>();
		m_SpriteRenderer = GetComponent<SpriteRenderer>();


		billie = GameObject.Find ("Billie");


		//Start CharCon2d
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Capsule = GetComponent<CapsuleCollider2D>();

		m_CurrentPosition = m_Rigidbody2D.position;
		m_PreviousPosition = m_Rigidbody2D.position;

		m_ContactFilter.layerMask = groundedLayerMask;
		m_ContactFilter.useLayerMask = true;
		m_ContactFilter.useTriggers = false;
		//SetHorizontalSpeed (speed);

		//Physics2D.queriesStartInColliders = false;

		m_Animator.SetBool ("HasSeenYou", true);
		position = GetComponent<Transform> ().position;
	}

	private void OnEnable()
	{

		m_Dead = false;
		m_Collider.enabled = true;
	}

	private void Start()
	{
		alertsound = GameObject.Find("Alerted").GetComponent<AlertedSound> ();
		frogdeath = GameObject.Find("FrogDeath").GetComponent<FrogDeath> ();

		billiejump = GameObject.Find ("Billie").GetComponent<JumpingCharacterController> ();
		m_Filter = new ContactFilter2D();
		m_Filter.layerMask = groundedLayerMask;
		m_Filter.useLayerMask = true;
		m_Filter.useTriggers = false;
		groundCheckRadius = .1f; //col.size.x * .5f

	}

	void FixedUpdate()
	{
		if (m_Dead)
			return;


		if(transform.position.x < position.x) {
			transform.position = position;
		}
			
		shouldBeCountingDown = false;
		//is he touching the ground
		isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayers);
		//if he hasn't seen billie, the timer should always be counting down, (and reseting at 0).
		if (!initSpotted) {
			//	Debug.Log ("before call");
			shouldBeCountingDown=true;
			//if he should be moving, move
				//m_Animator.SetFloat("xVelocity", 0);
		}


		UpdateTimers();
		ScanForPlayer ();
		CheckTargetStillVisible ();

		//CharCon2d
	}



	//Where the Guard Frog counts down to see if you're within range for long enough.
	void UpdateTimers()
	{

		//I wrote the AwarenessScript script. It controls the bar timer that goes up next to the guard enemy
		//If you're not in range, count down. otherwise he's seen you


		//Might need this one
		//m_Animator.SetBool ("HasSeenYou", true);
		//Permanently counting down to when he should turn around.

		//if he notices you, call this every frame.
		if(!sliderDown) {	//Method i created that moves the awareness slider up next to the frog
			affectSlider.Notice (Time.deltaTime);

		} else {
			//Otherwise, OutOfRange is a method i created that moves the awareness slider down.
			//it has a minimum amount, so is always bottoming out unless he notices you.
			affectSlider.OutOfRange (Time.deltaTime);
		}

		//used for the spitter enemy projectiles, not us
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		//If frog sees you, create a squash frog above you, and you're no longer allowed to move
		if (coll.gameObject.name == "Billie") {
			m_Animator.SetBool ("youDead", true); 

			alertsound.playSound ();
			GameObject go = Instantiate(SquashFrog);
			go.transform.position = billie.transform.position;
			go.transform.Translate (Vector3.up * 25.0f);
			billie.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
			affectSlider.youDead = true;
			affectSlider.awarenesssound.stopSound ();


			//billiejump.showgameover ();
		}

		if (coll.gameObject.tag == "canmovepulley") {
			if (playSoundOnce) {
				frogdeath.playSound ();
				playSoundOnce = false;
			}

		}



	}
	//if a frog falls off a cliff, he dies
	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "DeadlyThing") {
			//Debug.Log ("fall");
			Destroy(gameObject);
		}
		//if you shoot him, bullet dies and there's a tink sound
		if (coll.tag == "Bullet") {
			//Play *tink* sound 
			AudioSource arrowHitFrog = gameObject.GetComponent<AudioSource> ();
			arrowHitFrog.Play ();
			GameObject go = coll.gameObject;
			Destroy (go);
		}
	}

	//Where he identifies you
	public void ScanForPlayer()
	{
		//Moves him towards you
		Transform billieTransform = billie.GetComponent<Transform>();
		Vector3 dir = billieTransform.position - transform.position;

		if (dir.sqrMagnitude > viewDistance * viewDistance)
		{
			//Debug.Log (dir.sqrMagnitude + " " + viewDistance * viewDistance);
			sliderDown = true;
			return;
		}

		//Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? Mathf.Sign(m_SpriteForward.x) * -viewDirection : Mathf.Sign(m_SpriteForward.x) * viewDirection) * m_SpriteForward;

		//float angle = Vector3.Angle(testForward, dir);

		/*if (angle > viewFov * 0.5f)
		{
			Debug.Log ("this out");
			return;
		} */

		m_Target = billieTransform;

		m_TimeSinceLastTargetView = timeBeforeTargetLost;
		sliderDown = false;


		initSpotted=true;

	}



	public void CheckTargetStillVisible()
	{
		if (m_Target == null) {
			return;
		}

		/*	Vector3 toTarget = m_Target.position - transform.position;
		Debug.Log ("First");
		if (toTarget.sqrMagnitude < viewDistance)
		{
			Vector3 testForward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * m_SpriteForward;
			if (m_SpriteRenderer.flipX) testForward.x = -testForward.x;

			float angle = Vector3.Angle(testForward, toTarget);

			if (angle <= viewFov * 0.5f)
			{
				//we reset the timer if the target is at viewing distance.
				m_TimeSinceLastTargetView = timeBeforeTargetLost;
			}   
		}*/ 

		//If you've been out of range long enough (so it can't see you any more, and the slider is totally down,
		//call the forget method
		if (affectSlider.m_CurrentAwareness <= 0.0f)
		{

			ForgetTarget();
		} 

	}

	//Once he forgets you, target becomes null, which sets the animator to keep walking again
	public void ForgetTarget()
	{
		initSpotted = false;
		m_Target = null;
	}




	/*#if UNITY_EDITOR
	private void OnDrawGizmosSelected()
	{
		//draw the cone of view
		Vector3 forward = spriteFaceLeft ? Vector2.left : Vector2.right;
		forward = Quaternion.Euler(0, 0, spriteFaceLeft ? -viewDirection : viewDirection) * forward;

		if (GetComponent<SpriteRenderer>().flipX) forward.x = -forward.x;

		Vector3 endpoint = transform.position + (Quaternion.Euler(0, 0, viewFov * 0.5f) * forward);

	}
	#endif*/

}
