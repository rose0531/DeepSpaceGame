using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour {
    public float destroyTime;
    private GameObject popupTextPrefab;
    private GameObject instance;
    [SerializeField] private float enemyDamagePopupTextRandomness;

    private void Awake()
    {
        popupTextPrefab = Resources.Load<GameObject>("Prefab/PopupTextHolder");     // Load the popupText prefab.
    }

    private void Start()
    {
        //Destroy(gameObject, destroyTime);
    }

    public void SpawnPopupText(float damage)
    {
        ShowPopupText(damage.ToString());
    }

    //TODO: spawn text on enemy position
    private void ShowPopupText(string text)
    {

        Debug.Log("Text spawned! " + text);
        // Instantiate popupText gameobject at the enemies position and set it's parent to be the canvas.


        // Convert the enemies world position to the position on the camera's screen.
        // Also add some randomness to the new screen position.
        Vector2 enemyPositionRandomness = new Vector2(transform.position.x + Random.Range(-enemyDamagePopupTextRandomness, enemyDamagePopupTextRandomness), transform.position.y + Random.Range(-enemyDamagePopupTextRandomness, enemyDamagePopupTextRandomness));
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(enemyPositionRandomness);

        // Get the child TextMeshProUGUI on the popupText gameobject and change the text.
        popupTextPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;

        // Anchor the popupText to the bottom left of the canvas.
        popupTextPrefab.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
        popupTextPrefab.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);

        // Set the anchored position of the popupText to the new screen position.
        popupTextPrefab.GetComponent<RectTransform>().anchoredPosition = screenPosition;
    }

}
