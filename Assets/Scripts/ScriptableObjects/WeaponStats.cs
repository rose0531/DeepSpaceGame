using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponStats : ScriptableObject {

    public int Damage;
    public float ProjectileSpeed;
    public ParticleSystem ImpactEffect;
}
