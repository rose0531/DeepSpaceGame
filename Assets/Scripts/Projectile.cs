using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] private float m_Speed;
    [SerializeField] private string[] tags;

    private void Update()
    {
        transform.Translate(Vector3.right * m_Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if(other.gameObject.CompareTag(tags[i]))
            {
                return;
            }
        }
        Debug.Log("destroyed");
        Destroy(gameObject);
    }
}
