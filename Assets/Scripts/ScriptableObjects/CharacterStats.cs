using UnityEngine;

[CreateAssetMenu]
public class CharacterStats : ScriptableObject
{
    public float HP;
    public float MaxAscendVelocity;
    public float MaxDecendVelocity;
    public float MoveSpeed;
    public float JumpForce;
    public bool IsFacingRight;
    public bool IsGrounded;
    public bool IsDead;

    private void OnEnable()
    {
        IsFacingRight = true;
        IsGrounded = true;
        IsDead = false;
    }
}
