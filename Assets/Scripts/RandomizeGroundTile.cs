using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeGroundTile : MonoBehaviour {

    public Sprite[] sprites;

    private void Awake()
    {
        int randomSprite = UnityEngine.Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[randomSprite];
    }
}
