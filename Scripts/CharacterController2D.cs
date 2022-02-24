using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	//Various variables needed to implement full movement
	private bool facingRight = true;

	private Vector3 m_Velocity = Vector3.zero; //Used to keep track of our speed

	const float grndRadius = .2f; //Used for detecting the ground from the position groundCheck

	private bool grnd; //Are we grounded or not

	const float clngRadius = .2f;//Used for detecting the ceiling from the position ceilingCheck

	private Rigidbody2D rigBody2D;

	//Variable that are accesible directly in the unity editor
	[SerializeField] private float jumpForce = 400f;							// Jump force
	[Range(0, 1)] [SerializeField] private float crouchSpeed = .60f;			// Speed multiplier when crouching
	[Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;	// For smoothing the movement
	private bool m_AirControl = true;											// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// Allow us the choose which layers we want to define as ground. In our case it is the layer called "Middle"
	[SerializeField] private Transform groundCheck;							// The emplacement where we want to see if there is ground.
	[SerializeField] private Transform ceilingCheck;							// The emplacement where we want to see if there is a ceiling
	[SerializeField] private Collider2D crouchColliderDisable;                // We disable the top collider of the player when crouching

	public BoolEvent OnCrouchEvent;
	private bool wasCrching = false;
	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }



	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;



	private void Awake()
	{
		//Initiating the event Attribute the start
		rigBody2D = GetComponent<Rigidbody2D>();

				if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{

		bool wasGrounded = grnd;
		grnd = false;

		//We are considered grounded if when we are casting a circle of radius grndRadius and center groundCheck touches an object that is the ground
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, grndRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grnd = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// We first start by checking if the character is able to stand up
		if (!crouch)
		{
			// Similarly to the ground check above, we trace a circle of radius clngRadius and for center our ceilingCheck
			if (Physics2D.OverlapCircle(ceilingCheck.position, clngRadius, m_WhatIsGround))
			{
				crouch = true; // We force the crouching even if the player does not push the crouching button
			}
		}

		//We can control the player only if we are grounded or if the air control setting is turned on
		if (grnd || m_AirControl)
		{
			// We first check if we are crouching
			if (crouch)
			{
				if (!wasCrching)
				{
					wasCrching = true;
					OnCrouchEvent.Invoke(true);
				}
				// Then we multiply our base movespeed by the crouchespeed factor.
				move *= crouchSpeed;

				// We also disable the top collider to allow a lower hitbox
				if (crouchColliderDisable != null)
					crouchColliderDisable.enabled = false;
			} else
			{
				// When we are not crouching, we reenable it.
				if (crouchColliderDisable != null)
					crouchColliderDisable.enabled = true;
				if (wasCrching)
				{
					wasCrching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			// We move our charcter by getting the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, rigBody2D.velocity.y);
			// And we smoothing while applying it to the character
			rigBody2D.velocity = Vector3.SmoothDamp(rigBody2D.velocity, targetVelocity, ref m_Velocity, movementSmoothing);

			// When we are moving right when the player moves left, we flip the sprites
			if (move < 0 && facingRight)
			{
				Flip();
			}
			// Same here
			else if (move > 0 && !facingRight)
			{
				Flip();
			}
		}
		// When the user jump
		if (grnd && jump)
		{
			// We add a vertical force to the character using the parameter jumpForce
			grnd = false;
			rigBody2D.AddForce(new Vector2(0f, jumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
