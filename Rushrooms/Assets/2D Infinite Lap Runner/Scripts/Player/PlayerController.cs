using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float runForce;
	[SerializeField] private float jumpForce;
	[SerializeField] private float maxSpeed;
	[SerializeField] private LayerMask groundMask;

	//the following 3 bools controls if the player can jump. Only when all these variable are true, a player can jump.
	private bool isGrounded = false;
	private bool jump = false;
	private bool canJump = true;

	private float groundCheckRadius = 0.45f;

	private Rigidbody2D thisRigidbody2D;
	private Transform thisTransform;
	private Transform groundCheckPoint;
	private Animator anim;

	void Start ()
	{
		thisRigidbody2D = GetComponent<Rigidbody2D>();

		thisTransform = transform;

		groundCheckPoint = thisTransform.Find ("GroundCheckPoint");

		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		//if the jump button or left mouse button is held down we set jump to true.
		//On ios or android device, a single screen touch is equivalent to a left mouse click.
		//As we only need left mouse click in this game, the game can be runned on mobile device theoretically.
		if (Input.GetButton ("Jump") || Input.GetMouseButton (0)) 
			jump = true;
		else
			jump = false;

		//in order to prevent player from keeping holding the jump key or left mouse, we add a second jump variable. Player need to release the key before they can perform a new jump.
		if (Input.GetButtonUp ("Jump") || Input.GetMouseButtonUp (0))
			canJump = true;
	}
	
	void FixedUpdate ()
	{
		//we create a circle to see if any objects on the Ground layer lies in the circle. If not we are not grounded.
		isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundMask);

		anim.SetBool ("Grounded", isGrounded); //play differnet animations (run or jump) according to isGrounded

		if (jump && canJump && isGrounded) //when are 3 bools are ture, we perform a jump
		{
			isGrounded = false;

			GetComponent<AudioSource>().Play ();

			thisRigidbody2D.AddRelativeForce (Vector2.up * jumpForce, ForceMode2D.Impulse);

			canJump = false;
		}
		else if (!jump && !isGrounded) //We will change how high the player can jump based how long the jump key or lef mouse button is held. If the key is released we add a downward force to let the player fall faster.
		{
			thisRigidbody2D.AddRelativeForce (-Vector2.up * 20f);
		}

		//only add force to the player when it's under certain speed, prevent the player from running to fast
		if (thisTransform.InverseTransformDirection (thisRigidbody2D.velocity).x < maxSpeed)
		{
			thisRigidbody2D.AddRelativeForce (Vector2.right * runForce);
		}
	}
}
