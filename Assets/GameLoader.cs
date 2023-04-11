using UnityEngine.Tilemaps;
using UnityEngine;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;
    void Start()
    {
        //start loading map

        MapSaveHandler handle = new MapSaveHandler();
        var mapData = handle.LoadFirstMap();
        
    }

    void BuildMap(MapSaveData mapData)
    {
        
    }
}
