using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;
    public Transform playerFeet;
    public float checkGroundRadius;
    private bool isGrounded;
    public LayerMask whatIsGround;
    private Animator animator;
    public Image fuelBar;
    public GameObject fuelFumesEffect;

    public float moveSpeed;
    public float jumpSpeed;
    public float maxAscendVelocity;
    public float maxDecendVelocity;
    public float jetPackForce;
    public float fuelCost;
    public float fuelAmount;
    private float fuelAmountCounter;
    public float refuelMultiplier;

    private float horizontalMoveInput;
    private bool isFacingRight;

	// Use this for initialization
	private void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isFacingRight = true;
        fuelAmount *= 60;
        fuelAmountCounter = fuelAmount;
        fuelFumesEffect.SetActive(false);
    }

    private void FixedUpdate()
    {
        /* Set animations to false */
        animator.SetBool("isWalking", false);

        /* Get horizontal movement */
        horizontalMoveInput = Input.GetAxisRaw("Horizontal"); // Left = -1, Right = 1

        /* Flip Player based on which direction they are facing */
        if (isFacingRight && horizontalMoveInput == -1)
            Flip();
        else if (!isFacingRight && horizontalMoveInput == 1)
            Flip();
        
        /* Set walking animation if Player is walking */
        if (horizontalMoveInput != 0)
            animator.SetBool("isWalking", true);

        rb.velocity = new Vector2(horizontalMoveInput * moveSpeed, rb.velocity.y);

        /* Instant jump if Player is on the ground */
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }

        /* Use fuel if Player is holding space while in the air */
        if (Input.GetKey(KeyCode.Space) && !isGrounded && fuelAmountCounter > 0)
        {
            fuelAmountCounter -= fuelCost;
            fuelBar.fillAmount = fuelAmountCounter / fuelAmount;
            fuelFumesEffect.SetActive(true);

            if (rb.velocity.y < 0) //add ascending force
            {
                rb.AddForce(new Vector2(0, -1 * rb.velocity.y + jetPackForce + 5f));
            }
            else //add descending force
            {
                rb.AddForce(new Vector2(0, jetPackForce));
            }
        }
        else
        {
            fuelFumesEffect.SetActive(false);
        }

        /* Constrain the ascending velocity and the descending velocity of the Player */
        if (rb.velocity.y > maxAscendVelocity)
            rb.velocity = new Vector2(rb.velocity.x, maxAscendVelocity);
        if (rb.velocity.y < -maxDecendVelocity)
            rb.velocity = new Vector2(rb.velocity.x, -maxDecendVelocity);
    }

    // Update is called once per frame
    private void Update () {
        /* Set animations to false */
        animator.SetBool("isGrounded", false);
        animator.SetBool("falling", false);

        /* Check if player is grounded */
        isGrounded = Physics2D.OverlapCircle(playerFeet.position, checkGroundRadius, whatIsGround);
        if (isGrounded)
        {
            animator.SetBool("isGrounded", true);

            /* Refuel while player is on the ground */
            StartCoroutine(refuel());
        }
        else
        {
            animator.SetBool("falling", true);
            
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
        if(transform.rotation.y == 0)
        {
            fuelBar.transform.Rotate(0f, 0f, 0f);
        }
        else
        {
            fuelBar.transform.Rotate(0f, 180f, 0f);
        }
    }

    private IEnumerator refuel()
    {
        if(fuelAmountCounter < fuelAmount)
            fuelAmountCounter += fuelCost * refuelMultiplier;
        fuelBar.fillAmount += fuelCost / fuelAmount * refuelMultiplier;
        yield return null;
    }
}
