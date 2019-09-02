using UnityEngine;

public class SpawnObjects : MonoBehaviour {

    // Types of objects you can spawn.
    public enum ObjectType { Tile, RoomLayout, Enemy, Nothing};

    // Variables for Tile spawning.
    public enum TileType {    GroundTileTop1,
                              GroundTileTop2,
                              GroundTile1Ledge,
                              GroundTileBottom,
                              CeilingTileAcid};

    public ObjectType objectType;
    public TileType tileType;
    private TileSpriteList tileSpriteList;
    private GameObject tile;

    // Variables for Room Layout spawning.
    public GameObject roomLayout;

    // Use this for initialization
    private void Start () {

        if (objectType == ObjectType.Tile)
        {
            tileSpriteList = GameObject.FindGameObjectWithTag("StartRoom").GetComponent<TileSpriteList>();

            int rand;
            Sprite sprite = null;

            switch (tileType)
            {
                case TileType.GroundTileTop1:
                    rand = Random.Range(0, tileSpriteList.GroundTileSpriteListTop1.Length);
                    sprite = tileSpriteList.GroundTileSpriteListTop1[rand];
                    break;
                case TileType.GroundTileTop2:
                    rand = Random.Range(0, tileSpriteList.GroundTileSpriteListTop2.Length);
                    sprite = tileSpriteList.GroundTileSpriteListTop2[rand];
                    break;
                case TileType.GroundTile1Ledge:
                    sprite = tileSpriteList.GroundTile1Ledge;
                    break;
                case TileType.GroundTileBottom:
                    rand = Random.Range(0, tileSpriteList.GroundTileSpriteListBottom.Length);
                    sprite = tileSpriteList.GroundTileSpriteListBottom[rand];
                    break;
                case TileType.CeilingTileAcid:
                    rand = Random.Range(0, tileSpriteList.CeilingTileSpriteListAcid.Length);
                    sprite = tileSpriteList.CeilingTileSpriteListAcid[rand];
                    break;
            }

            tile = Instantiate(Resources.Load("Prefab/Tile") as GameObject, transform.position, Quaternion.identity);
            tile.GetComponent<SpriteRenderer>().sprite = sprite;
            tile.transform.parent = transform;
        }else if (objectType == ObjectType.RoomLayout)
        {
            if(roomLayout != null)
            {
                Debug.Log("Spawning room layout");
                GameObject layout = Instantiate(roomLayout, transform.position, Quaternion.identity);
                layout.transform.parent = transform;
            }
        }else if (objectType == ObjectType.Enemy)
        {
            //Spawn enemy
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, 0.25f);
    }
}
