using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float m_Speed;

    private void Update()
    {
        transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && 
            !other.gameObject.CompareTag("SpawnPoint") &&
            !other.gameObject.CompareTag("Trigger"))
        {
            Debug.Log("Destroyed");
            Destroy(gameObject);
        }
    }
}
