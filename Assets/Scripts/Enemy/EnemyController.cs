using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyController : CharacterController{
    [SerializeField] private FloatEvent onEnemyDamaged;

    public override void Awake()
    {
        base.Awake();
        input = new AIInput();                                                      // Use AI input system.
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        onEnemyDamaged.Raise(damage);
    }

    //Testing damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Damage taken: " + 5);
            TakeDamage(5);
        }
    }
}
