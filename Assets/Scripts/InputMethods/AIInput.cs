using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIInput : ICharacterInput{
    public float Horizontal { get; private set; }
    public bool Attack { get; private set; }
    public bool Jump { get; private set; }
    public bool HeldJump { get; private set; }

    public static event Action OnShoot;

    private float moveTime = 1;
    private float moveTimeCounter = 0;

    public enum EnemyState
    {
        stroll,
        attack,
        stagger,
        moveTowardsPlayer
    }

    public void ReadInput()
    {
        moveTimeCounter += Time.deltaTime;
        if(moveTimeCounter >= moveTime)
        {
            moveTimeCounter = 0;
            if (Horizontal == 0)
                Horizontal = 1;
            Horizontal *= -1;
        }
    }

}
