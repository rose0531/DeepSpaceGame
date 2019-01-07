using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public float HUDPositionX;
    public float HUDPositionY;
    public float healthBoxSpacing;

    public Image fullHealthBox;
    public Image emptyHealthBox;

    public void SpawnHealth(int health)
    {
        float spacing = 0;
        for(int i = 0; i < health; i++)
        {
            Image healthBox = Instantiate(fullHealthBox, transform.position, Quaternion.identity) as Image;
            healthBox.transform.SetParent(transform);
            healthBox.rectTransform.localPosition = new Vector3(-400 + spacing + healthBox.rectTransform.rect.width, 300 - healthBox.rectTransform.rect.height, 0);
            spacing += healthBoxSpacing;
        }
    }
}
