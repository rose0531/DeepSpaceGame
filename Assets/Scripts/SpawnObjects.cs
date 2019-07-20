using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    private TileSpriteList tileSpriteList;
    private enum SpriteType { GroundTileTop1, GroundTileTop2, GroundTile1Ledge, GroundTileBottom, CeilingTileAcid};
    [SerializeField] private SpriteType spriteType;
    private GameObject tile;

	// Use this for initialization
	private void Start () {
        tileSpriteList = GameObject.FindGameObjectWithTag("StartRoom").GetComponent<TileSpriteList>();
        int randSprite;
        Sprite sprite = null;

        switch (spriteType)
        {
            case SpriteType.GroundTileTop1:
                randSprite = Random.Range(0, tileSpriteList.GroundTileSpriteListTop1.Length);
                sprite = tileSpriteList.GroundTileSpriteListTop1[randSprite];
                break;
            case SpriteType.GroundTileTop2:
                randSprite = Random.Range(0, tileSpriteList.GroundTileSpriteListTop2.Length);
                sprite = tileSpriteList.GroundTileSpriteListTop2[randSprite];
                break;
            case SpriteType.GroundTile1Ledge:
                sprite = tileSpriteList.GroundTile1Ledge;
                break;
            case SpriteType.GroundTileBottom:
                randSprite = Random.Range(0, tileSpriteList.GroundTileSpriteListBottom.Length);
                sprite = tileSpriteList.GroundTileSpriteListBottom[randSprite];
                break;
            case SpriteType.CeilingTileAcid:
                randSprite = Random.Range(0, tileSpriteList.CeilingTileSpriteListAcid.Length);
                sprite = tileSpriteList.CeilingTileSpriteListAcid[randSprite];
                break;
        }


        tile = Instantiate(Resources.Load("Prefab/Tile") as GameObject, transform.position, Quaternion.identity);
        tile.GetComponent<SpriteRenderer>().sprite = sprite;
        tile.transform.parent = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
