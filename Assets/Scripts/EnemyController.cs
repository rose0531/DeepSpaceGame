using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    private Rigidbody2D rb;
    public float health;
    private float currentHealth;
    public Image healthBar;

    public ParticleSystem enemyDamageParticles;

    public float moveSpeed, moveTime;
    private float moveTimeCounter;
    public float chaseSpeed;

    private bool isGrounded;
    public Transform enemyFeet;
    public float checkGroundRadius;
    public LayerMask whatIsGround;

    public float attackRadius;
    private Transform target;
    private int lastMove;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        target = PlayerManager.instance.player.transform;
        moveTimeCounter = 0;
        currentHealth = health;
        lastMove = 1;
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector2.Distance(target.position, transform.position);
        /* Check if grounded */
        //isGrounded = Physics2D.OverlapCircle(enemyFeet.position, checkGroundRadius, whatIsGround);

        if(distance <= attackRadius) //Player enters enemy attack radius
        {
            /* Move toward the player */
            transform.position = Vector2.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);
        }
        else
        {
            if(moveTimeCounter <= 0)
            {
                moveTimeCounter = moveTime;
                lastMove *= -1;
            }
            rb.velocity = new Vector2(moveSpeed * lastMove, rb.velocity.y);
        }       

        

        moveTimeCounter--;
	}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Instantiate(enemyDamageParticles, transform.position, Quaternion.identity);
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            healthBar.fillAmount = currentHealth / health;
        }
    }
}
