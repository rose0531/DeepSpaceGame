using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour {

    public CharacterStats stats;                                // Hold character stats. (Ex: HP, Damage, etc.).
    private int currentHealth;                                  // Current health of the character.
    public Action<float> OnHealthChanged = delegate { };        // Event to trigger when the character takes damage.
    private PopupText popupText;

    private void Awake()
    {
        currentHealth = stats.MaxHealth;
        popupText = GetComponent<PopupText>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        damage = (int)UnityEngine.Random.Range(10f, 100f);

        // Spawn our popup damage text.
        popupText.SpawnPopupTextDamage(damage, 100, transform.position);

        StartCoroutine(DamageFlash(0.1f));

        if(currentHealth < 0)
        {
            //Destroy(gameObject);
        }

        OnHealthChanged(GetHealthPercentage());
    }

    public void Heal(int heal)
    {
        currentHealth += heal;
        if(currentHealth > stats.MaxHealth)
        {
            currentHealth = stats.MaxHealth;
        }
    }

    public float GetHealthPercentage()
    {
        return (float)currentHealth / stats.MaxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    private IEnumerator DamageFlash(float timeBetweenFlash)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color orignalColor = sr.color;
        sr.color = new Color(1, 90f/255f, 90f/255f, 1);
        yield return new WaitForSeconds(timeBetweenFlash);
        sr.color = orignalColor;
        yield return new WaitForSeconds(timeBetweenFlash);
    }
}
