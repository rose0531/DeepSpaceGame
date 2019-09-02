using UnityEngine;
using System;


public abstract class BaseState {
    protected GameObject gameObject;
    protected Transform transform;
    protected Rigidbody2D rb2d;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 vel = Vector2.zero;
    protected AI enemyAI;

    public BaseState(GameObject gameObject, AI enemyAI)
    {
        this.enemyAI = enemyAI;
        this.gameObject = gameObject;
        transform = gameObject.transform;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public abstract Type Tick();

    public Quaternion RotateEnemyTowards(Vector3 target)
    {
        // Convert target Vector3 Transform to Vector2.
        Vector2 target2D = new Vector2(target.x, target.y);

        // Convert enemy Vector3 Transform to Vector2.
        Vector2 transform2D = new Vector2(transform.position.x, transform.position.y);

        // Get the direction you want to rotate the enemy towards.
        Vector2 direction = target2D - transform2D;

        // Get the Z angle we need to rotate the enemy.
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate via the Z angle and return the Quaternion.
        return Quaternion.Euler(0f, 0f, rotZ);
    }

    public Vector2 Transform2D(Transform t)
    {
        return new Vector2(t.position.x, t.position.y);
    }

    public void CheckSpriteOrientation()
    {
        // Flip sprite depending on which direction it's going.
        if ((transform.rotation.z <= 0.5f && transform.rotation.z >= -0.5f && !enemyAI.facingRight) ||
            ((transform.rotation.z > 0.5f || transform.rotation.z < -0.5f) && enemyAI.facingRight))
        {
            enemyAI.facingRight = !enemyAI.facingRight;
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }
}
