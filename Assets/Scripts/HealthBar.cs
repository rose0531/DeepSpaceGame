using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField] private Image bar;
    [SerializeField] private float healthUpdateSpeed = 0.2f;

    // Use Start() instead of Awake() because HealthSystem is created in Awake in EnemyAI.
    // If you use Awake here, healthSystem will be null because EnemyAI hasn't created it yet.
    private void Start()
    {
        GetComponentInParent<HealthSystem>().OnHealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float amount)
    {
        StartCoroutine(UpdateHealth(amount));
    }

    private IEnumerator UpdateHealth(float amount)
    {
        float currentHealthAmount = bar.fillAmount;
        float elapsed = 0f;

        while(elapsed < healthUpdateSpeed)
        {
            elapsed += Time.deltaTime;
            bar.fillAmount = Mathf.Lerp(currentHealthAmount, amount, elapsed / healthUpdateSpeed);
            yield return null;
        }

        bar.fillAmount = amount;
    }
}
