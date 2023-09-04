using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    private float inputX;
    private float groundRadius;
    private float jumpForce;
    private int movementSpeedX;
    private Vector2 movement;
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private bool isFacingRight;
    private bool isPlayerTouchingGround;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private Transform groundLocation;

    // Use this for initialization
    void Start()
    {
        movementSpeedX = 5;
        groundRadius = 0.2f;
        jumpForce = 400f;
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        isFacingRight = true;
        isPlayerTouchingGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        

        if(isPlayerTouchingGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetBool("Grounded", false);
            myRigidBody.AddForce(new Vector2(0, jumpForce));
        }
    }

    void FixedUpdate()
    {

        isPlayerTouchingGround = Physics2D.OverlapCircle(groundLocation.position, groundRadius, whatIsGround);

        myAnimator.SetBool("Grounded", isPlayerTouchingGround);
        myAnimator.SetFloat("VerticalSpeed", myRigidBody.velocity.y);

        inputX = Input.GetAxis("Horizontal");
        movement = new Vector2(movementSpeedX * inputX, myRigidBody.velocity.y);
        myRigidBody.velocity = movement;

        myAnimator.SetFloat("HorizontalMovement", Mathf.Abs(inputX));

        if (inputX > 0 && isFacingRight == false)
            FlipSanta();
        else if (inputX < 0 && isFacingRight == true)
            FlipSanta();
    }

    void FlipSanta()
    {
        isFacingRight = !isFacingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
