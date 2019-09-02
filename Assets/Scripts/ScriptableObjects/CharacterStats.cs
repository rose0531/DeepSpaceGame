using UnityEngine;
using System;

[CreateAssetMenu]
public class CharacterStats : ScriptableObject
{
    public int MaxHealth;
    public bool IsDead;
    public Action<float> OnHealthChanged = delegate { };

    private void OnEnable()
    {
        IsDead = false;
    }
}
