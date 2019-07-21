using UnityEngine;

[CreateAssetMenu]
public class CharacterStats : ScriptableObject
{
    public float HP;
    public bool IsDead;

    private void OnEnable()
    {
        IsDead = false;
    }
}
