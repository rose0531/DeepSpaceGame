using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class PopupText : MonoBehaviour {
    public float destroyTime;
    private GameObject popupTextHolderPrefab;
    private GameObject instance;
    [SerializeField] private float enemyDamagePopupTextRandomness;
    [SerializeField] private float rotationRange;
    private const int SIZE = 2;
    [SerializeField] private Color[] highDamageColorRange = new Color[SIZE];
    [SerializeField] private Color[] midDamageColorRange = new Color[SIZE];
    [SerializeField] private Color[] lowDamageColorRange = new Color[SIZE];

    private void OnValidate()
    {
        if (highDamageColorRange.Length != SIZE)
            Array.Resize(ref highDamageColorRange, SIZE);
        if (midDamageColorRange.Length != SIZE)
            Array.Resize(ref midDamageColorRange, SIZE);
        if (lowDamageColorRange.Length != SIZE)
            Array.Resize(ref lowDamageColorRange, SIZE);
    }

    private void Awake()
    {
        // Load the popupTextHolder prefab.
        popupTextHolderPrefab = Resources.Load<GameObject>("Prefab/PopupTextCanvas");
    }

    private void Update()
    {
        if(instance != null)
        {
            Destroy(instance, destroyTime);
        }
    }

    public void SpawnPopupTextDamage(float damage, int currentHealth, Vector3 position)
    {
        float damageRelativeToHealth = damage / currentHealth;
        Color[] textColor;
        if (damageRelativeToHealth < 0.3f)
            textColor = lowDamageColorRange;
        else if (damageRelativeToHealth >= 0.3f && damageRelativeToHealth < 0.6f)
            textColor = midDamageColorRange;
        else
            textColor = highDamageColorRange;

        ShowPopupText(damage.ToString(), textColor, position);
    }

    private void ShowPopupText(string text, Color[] textColor, Vector3 position)
    {
        // Get position of enemy and add some randomness to the position.
        Vector2 enemyPositionRandomness = new Vector2(position.x + UnityEngine.Random.Range(-enemyDamagePopupTextRandomness, enemyDamagePopupTextRandomness),
                                                      position.y + UnityEngine.Random.Range(-enemyDamagePopupTextRandomness, enemyDamagePopupTextRandomness));

        // Instantiate the popupTextHolder at Vector3.zero.
        instance = Instantiate(popupTextHolderPrefab, enemyPositionRandomness, Quaternion.identity);
        Transform textMeshPro = instance.transform.Find("TextMeshPro");

        // Get the TextMeshPro script from the child of the instance and set the text.
        textMeshPro.GetComponent<TextMeshProUGUI>().text = text;

        // Pick random range between the colors provided in the textColor array.
        Color randomColor = new Color(UnityEngine.Random.Range(textColor[0].r, textColor[1].r),
                                      UnityEngine.Random.Range(textColor[0].g, textColor[1].g),
                                      UnityEngine.Random.Range(textColor[0].b, textColor[1].b));

        textMeshPro.GetComponent<TextMeshProUGUI>().faceColor = randomColor;

        // Set the position of the TextMeshPro RectTransform to the enemy position with randomness.
        textMeshPro.GetComponent<RectTransform>().anchoredPosition = enemyPositionRandomness;

        // Set random rotation.
        Quaternion angle = Quaternion.AngleAxis(UnityEngine.Random.Range(-rotationRange, rotationRange), Vector3.forward);
        textMeshPro.GetComponent<RectTransform>().rotation = angle;

        // Set instance parent.
        instance.transform.SetParent(transform);
    }

}
