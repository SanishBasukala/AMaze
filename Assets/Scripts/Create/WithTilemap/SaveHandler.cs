using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
public class SaveHandler : MonoBehaviour
{
    public static SaveHandler instance;
    public Tilemap tilemap;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.A)) SaveLevel();
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L)) LoadLevel();
    }
    public void SaveLevel()
    {
        BoundsInt bounds = tilemap.cellBounds;
        LevelData levelData = new();

        Debug.Log("level saved");

        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (temp != null)
                {
                    levelData.tiles.Add(temp);
                    levelData.pos.Add(new Vector3Int(x, y, 0));
                }
            }
        }
        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + "/AMaze.json", json);
    }

    public void LoadLevel()
    {
        string json = File.ReadAllText(Application.dataPath + "/AMaze.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);

        tilemap.ClearAllTiles();

        for (int i = 0; i < data.tiles.Count; i++)
        {
            tilemap.SetTile(data.pos[i], data.tiles[i]);
        }
    }
}

public class LevelData
{
    public List<TileBase> tiles = new();
    public List<Vector3Int> pos = new();
}
