using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour {

    public ICharacterInput input;                       // Input method for the character. (ie. Mouse/Keyboard, XBox, PS4, AI input, etc.)
    public Rigidbody2D Rb { get; private set; }         // Access to the RigidBody2D of the character.
    public CharacterStats characterStats;

    [SerializeField] protected Transform characterFeet; // Transform of where the characters feet are.
    [SerializeField] protected float checkGroundRadius; // OverlapCircle radius we use to check if our player is touching the ground.
    [SerializeField] protected LayerMask whatIsGround;  // Layer name that the ground is on.

    public virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        input.ReadInput();

        // Update player transform to face which ever direction the player is walking.
        if (input.Horizontal < 0 && characterStats.IsFacingRight)
            Flip();
        if (input.Horizontal > 0 && !characterStats.IsFacingRight)
            Flip();
    }

    //protected virtual void FixedUpdate()
    public virtual void FixedUpdate()
    {
        // Update player horizontal movement.
        UpdateHorizontalMovement(input.Horizontal);

        // Update player vertical movement if player jumped.
        characterStats.IsGrounded = Physics2D.OverlapCircle(characterFeet.position, checkGroundRadius, whatIsGround);
        if (!characterStats.IsGrounded)
            Debug.Log("IsGrounded: " + characterStats.IsGrounded);

        // If character jumped and they are grounded, then apply the jump force.
        if (input.Jump && characterStats.IsGrounded)
        {
            ApplyJumpForce();
            Debug.Log("Apply Jump Force");
        }

        // Restrict player ascend and descend velocity.
        if (Rb.velocity.y > characterStats.MaxAscendVelocity)
            Rb.velocity = new Vector2(Rb.velocity.x, characterStats.MaxAscendVelocity);
        if (Rb.velocity.y < -characterStats.MaxDecendVelocity)
            Rb.velocity = new Vector2(Rb.velocity.x, -characterStats.MaxDecendVelocity);
    }

    public void UpdateHorizontalMovement(float xMovement)
    {
        // Update the character's horizontal movement.
        Rb.velocity = new Vector2(xMovement * characterStats.MoveSpeed, Rb.velocity.y);
    }

    public void ApplyJumpForce()
    {
        // Apply the jump force (located in stats) to the character.
        Rb.velocity = Vector2.up * characterStats.JumpForce;
    }

    public void ApplyForce(float xForce, float yForce)  // Custom force
    {
        // Apply a custom force to the character. (Ex: JetPack uses this to apply vertical force to the character)
        Rb.AddForce(new Vector2(xForce, yForce));
    }

    public virtual void TakeDamage(float damage)
    {
        // Character takes damage.
        characterStats.HP -= damage;

        if(characterStats.HP <= 0)
        {
            // Destroy character
            characterStats.IsDead = true;
        }
    }

    protected virtual void Flip()
    {
        // Flip which way the player is facing.
        characterStats.IsFacingRight = !characterStats.IsFacingRight;

        // Rotate y axis by 180 to flip the player transform.
        transform.Rotate(0f, 180f, 0f);
    }

    private Vector2 PixelPerfectClamp(Vector2 moveVector, float pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));

        return vectorInPixels / pixelsPerUnit;
    }
}
