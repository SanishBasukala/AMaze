using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SaveHandler : MonoBehaviour
{
    public static SaveHandler instance;
    public Tilemap tilemap;
    public Text[] slotState;
    private int saveSlot;
    private string filename;

    public bool inCreate;
    public Camera mainCamera;
    public Camera playerCamera;

    private List<int> tileData = new();
    private List<Vector3Int> tilePositionData = new();
    private List<int> prefabsData = new();
    private List<Vector2> prefabPositionData = new();

    private List<GameObject> prefabs = new();
    private List<Vector2> prefabPositions = new();
    private List<TileBase> tiles = new();
    private List<Vector3Int> tilePositions = new();

    public InventoryManager inventoryManager;
    private void Awake()
    {
        saveSlot = 0;
        if (instance == null) instance = this;
        else Destroy(this);
    }
    public void SaveSlot1()
    {
        inCreate = true;

        filename = "/AMaze1.json";
        saveSlot = 1;
    }
    public void SaveSlot2()
    {
        inCreate = true;
        filename = "/AMaze2.json";
        saveSlot = 2;
    }
    public void SaveSlot3()
    {
        inCreate = true;
        filename = "/AMaze3.json";
        saveSlot = 3;
    }
    public void SaveButton()
    {
        if (saveSlot == 1)
        {
            slotState[0].text = "AMaze1";
            slotState[3].text = "AMaze1";
        }
        else if (saveSlot == 2)
        {
            slotState[1].text = "AMaze2";
            slotState[4].text = "AMaze2";
        }
        else if (saveSlot == 3)
        {
            slotState[2].text = "AMaze3";
            slotState[5].text = "AMaze3";
        }
        SaveLevel(filename);
    }
    public void SaveLevel(string filename)
    {
        BoundsInt bounds = tilemap.cellBounds;
        LevelData levelData = new();

        //for (int x = bounds.min.x; x < bounds.max.x; x++)
        //{
        //    for (int y = bounds.min.y; y < bounds.max.y; y++)
        //    {
        //        TileBase temp = tilemap.GetTile(new Vector3Int(x, y, 0));
        //        CustomTile tempTile = tiles.Find(t => t.tile == temp);
        //        if (tempTile != null)
        //        {
        //            levelData.tiles.Add(tempTile.id);
        //            levelData.pos_x.Add(x);
        //            levelData.pos_y.Add(y);
        //        }
        //    }
        //}
        foreach (int tile in tileData)
        {
            levelData.tiles.Add(tile);
        }
        foreach (Vector3Int tilePos in tilePositionData)
        {
            levelData.tilePosition.Add(tilePos);
        }

        foreach (int pre in prefabsData)
        {
            levelData.prefabs.Add(pre);
        }
        foreach (Vector2 prePos in prefabPositionData)
        {
            levelData.prefabPosition.Add(prePos);
        }

        //File.WriteAllText(Application.dataPath + "/AMaze.json", json);

        string json = JsonUtility.ToJson(levelData, true);
        File.WriteAllText(Application.dataPath + filename, json);
    }

    public void LoadSlot1()
    {
        LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze1.json"));
    }
    public void LoadSlot2()
    {
        LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze2.json"));
    }
    public void LoadSlot3()
    {
        LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze3.json"));
    }
    public void LoadLevel(string json)
    {
        inCreate = false;
        playerCamera.gameObject.SetActive(true);
        mainCamera.gameObject.SetActive(false);

        //json = File.ReadAllText(Application.dataPath + "/AMaze.json");
        LevelData data = JsonUtility.FromJson<LevelData>(json);
        tilemap.ClearAllTiles();

        // for tiles
        for (int i = 0; i < data.tiles.Count; i++)
        {
            tiles.Add(inventoryManager.tiles[data.tiles[i] - 2]);
            tilePositions.Add(new Vector3Int(Mathf.FloorToInt(data.tilePosition[i].x), Mathf.FloorToInt(data.tilePosition[i].y), 0));
        }

        for (int i = 0; i < tiles.Count; i++)
        {
            tilemap.SetTile(tilePositions[i], tiles[i]);
        }
        // for prefabs
        for (int i = 0; i < data.prefabs.Count; i++)
        {
            prefabs.Add(inventoryManager.prefabs[data.prefabs[i] - 3]);
            prefabPositions.Add(new Vector2(data.prefabPosition[i].x, data.prefabPosition[i].y));
        }

        for (int i = 0; i < prefabs.Count; i++)
        {
            GameObject prefabInstance = Instantiate(prefabs[i], prefabPositions[i], Quaternion.identity);
            prefabInstance.transform.SetParent(transform);
        }
    }
    public void CollectTiles(Vector3Int pos, int tileId)
    {
        tileData.Add(tileId);
        tilePositionData.Add(pos);
    }
    public void CollectPrefabs(Vector2 pos, int prefabId)
    {
        print("add " + prefabId + " position " + pos);
        prefabsData.Add(prefabId);
        prefabPositionData.Add(pos);
    }
}

public class LevelData
{
    public List<int> tiles = new();
    public List<Vector3Int> tilePosition = new();
    public List<int> prefabs = new();
    public List<Vector2> prefabPosition = new();
}
