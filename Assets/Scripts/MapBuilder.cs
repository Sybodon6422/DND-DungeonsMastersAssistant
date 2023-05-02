using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBuilder : MonoBehaviour
{
    #region singleton
    private static MapBuilder _instance;
    public static MapBuilder Instance { get { return _instance; } }
    private void Start()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
        if(useSceneMap){
        LoadMap();
        }
    }
    #endregion
    [SerializeField] private bool useSceneMap = true;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private Vector2Int mapSize;
    public TileBase grassTile, forestTile, waterTile, sandTile, mountainTile;

    private Grid.TileType[,] tileData;

    public void SaveMap()
    {
        tileData = new Grid.TileType[mapSize.x,mapSize.y];
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var checkTile = tileMap.GetTile(new Vector3Int(x,y,0));
                if(checkTile == grassTile) { tileData[x,y] = Grid.TileType.grass; }
                else if(checkTile == forestTile) { tileData[x,y] = Grid.TileType.forest; }
                else if(checkTile == waterTile) { tileData[x,y] = Grid.TileType.water; }
                else if(checkTile == sandTile) { tileData[x,y] = Grid.TileType.sand; }
                else if(checkTile == mountainTile) { tileData[x,y] = Grid.TileType.mountain; }
            }
        }

        MapSaveHandler mapper = new MapSaveHandler();
        mapper.SaveGame(new MapSaveData("New Map",tileData,mapSize));
    }

    public void LoadMap()
    {
        MapSaveHandler mapper = new MapSaveHandler();
        var mapData = mapper.LoadFirstMap();
        tileData = mapData.tileData;
        mapSize = mapData.GetMapSize();
        
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                var checkTile = tileData[x,y];
                Vector3Int currentTilePosition = new Vector3Int(x,y,0);

                switch (checkTile)
                {
                case Grid.TileType.grass:
                    tileMap.SetTile(currentTilePosition, grassTile);
                    break;
                case Grid.TileType.forest:
                    tileMap.SetTile(currentTilePosition, forestTile);
                    break;
                case Grid.TileType.water:
                    tileMap.SetTile(currentTilePosition, waterTile);
                    break;
                case Grid.TileType.sand:
                    tileMap.SetTile(currentTilePosition, sandTile);
                    break;
                case Grid.TileType.mountain:
                    tileMap.SetTile(currentTilePosition, mountainTile);
                    break;
                }
            }
        }

        FinishMapLoad();
    }

    private void FinishMapLoad()
    {
        GameBoard.Instance.SetupGameBoard(mapSize,tileData);
        Destroy(this);
    }
}
