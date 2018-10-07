using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class JumpingCharacterController : MonoBehaviour
{
    public float playerSpeed = 5.0f;
    public bool isFacingRight = true;
    public int jumpForce = 100;
	private CapsuleCollider2D col;
	public LayerMask groundLayers;
	private float groundCheckRadius;
	public Transform groundCheckPoint;
	private Quaternion originalRotate;
	public float vSpeed;
	public GameObject[] ignores;
	GameObject[] gameoverObjects;
	GameObject[] menuObjects;
	HandleCursor cursor;

	AudioSource footsound;
	GameObject envtsounds;

	private DeathSound deathSound;


	private ForceExperiments appliedForce;

    private bool isJumpPressed = false;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
	private RigidbodyConstraints2D pos;
	public bool squashFrogStops = false;
	public bool isGrounded;            // Whether or not the player is grounded.


    // Awake is used to initialize any variables before the game starts
    // Called after all objects are initialized so you can safely communicate with other objects
    //
    void Awake()
    {
		originalRotate = transform.rotation;
		appliedForce = GetComponent<ForceExperiments> ();
        rb = GetComponent<Rigidbody2D>();
		col = GetComponent<CapsuleCollider2D> ();
		groundCheckRadius = .1f; //col.size.x * .5f
        animator = GetComponent<Animator>();
		//isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayers);
		spriteRenderer = GetComponent<SpriteRenderer>();
		pos = RigidbodyConstraints2D.FreezeRotation;

    }

    // Start is called when a script is enabled. If you need to make sure something is initialized
    // put it in awake.
    //
    void Start()
    {
		footsound = GameObject.Find ("Billie").GetComponent<AudioSource> ();
		envtsounds = GameObject.Find ("Environmental Sound");
		deathSound = GameObject.Find("DeathSound").GetComponent<DeathSound> ();
		cursor = GameObject.Find ("Main Camera").GetComponent<HandleCursor> ();
		Time.timeScale = 1;
		gameoverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameOver");
		menuObjects = GameObject.FindGameObjectsWithTag("MenuButton");
		hidegameover();

	}


    // Update is called once per frame
    void Update()
    {
		//if (!squashFrogStops)
			isGrounded = Physics2D.OverlapCircle (groundCheckPoint.position, groundCheckRadius, groundLayers);
		//else
		//	isGrounded = false;
        vSpeed = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxisRaw("Horizontal");
        isJumpPressed = Input.GetButtonDown("Jump");

        // if the input is to the left
        if (vSpeed < 0.0)
        {
            // if the player is facing right
            if (isFacingRight)
            {
                // tell the sprite renderer to flip along the X-axis
                spriteRenderer.flipX = true;

                // the player is no longer facing right
                isFacingRight = false;
            }
        }
        else if (vSpeed > 0.0)
        {
            if (!isFacingRight)
            {
                // tell the sprite renderer to stop flipping along the X-axis
                // this should have the player facing right
                //
                spriteRenderer.flipX = false;

                // the player is facing right again
                isFacingRight = true;

            }
        }


        // we want the absolute value of the vSpeed for our animations since we want to 
        // use the 'negative' speed to just be speed in the left direction
        //

        if (vInput != 0.0f)
        {
            animator.SetFloat("xVelocity", Mathf.Abs(vSpeed));
        }
        else
        {
            animator.SetFloat("xVelocity", 0.0f);
        }
        animator.SetFloat("xInput", vInput);


		if(appliedForce.hasBeenShot==false)
        	rb.velocity = new Vector2(vSpeed * playerSpeed, rb.velocity.y);
		if (isGrounded)
			appliedForce.hasBeenShot = false;

		if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Space))
			animator.SetTrigger ("isJumping");
    }

	public void showgameover() {
		cursor.setMouse ();
		Time.timeScale = 0;
		footsound.mute = true;
		envtsounds.SetActive (false);
		foreach(GameObject g in gameoverObjects){
			Debug.Log (g);
			g.SetActive(true);
		}

		foreach (GameObject button in menuObjects)
		{

			button.GetComponent<Button>().interactable = false;

		}
	}

	public void hidegameover() {
		footsound.mute = false;
		envtsounds.SetActive (true);
		foreach(GameObject g in gameoverObjects){
			Time.timeScale = 1;
			g.SetActive(false);
		}
		foreach (GameObject button in menuObjects)
		{

			button.GetComponent<Button>().interactable = true;

		}
	}
    // Called multiple times per frame depending on the frame rate
    // locked in sync with the physics engine so physics manipulation
    // should take place here - particularly with RigidBodies
    void FixedUpdate()
    {
        // if the player pressed the jump button
        // apply a rigidbody force to launch the player up in the air
        //

		if ( isJumpPressed && isGrounded)
        {
            // up direction with a magnitude of jumpForce
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse );

            // player event is consumed
            isJumpPressed = false;


        }

		//transform.rotation = originalRotate;
		rb.freezeRotation = true;
    }

	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.name.Contains("Node") || coll.gameObject.name.Contains("rope") || coll.gameObject.name.Contains("Chunk") ){
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(), coll.collider);
		}

	}

	void OnTriggerEnter2D (Collider2D coll) {
		if (coll.gameObject.tag == "DeadlyThing") {
			deathSound.playSound ();
			showgameover ();
		}
		if (coll.gameObject.tag == "Spike") {
			deathSound.playSound ();
   			showgameover ();
		}

	}

	void LateUpdate()
	{

	}


}

