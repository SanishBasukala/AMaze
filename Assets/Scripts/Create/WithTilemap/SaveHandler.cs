using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SaveHandler : MonoBehaviour, IDataPersistence
{
    public static SaveHandler instance;
    public Tilemap tilemap;
    public Text[] slotState;

    private string slotText1;
    private string slotText2;
    private string slotText3;

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

    [SerializeField] private GameObject prefabCollector;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
    private void Update()
    {
        slotState[0].text = this.slotText1;
        slotState[3].text = this.slotText1;
        //Check if this is null or empty and if not null empty then show 2 button, edit current and create new
        slotState[1].text = this.slotText2;
        slotState[4].text = this.slotText2;
        slotState[2].text = this.slotText3;
        slotState[5].text = this.slotText3;
    }
    // For saving data of slots
    public void SaveData(ref GameData data)
    {
        data.slotText1 = this.slotText1;
        data.slotText2 = this.slotText2;
        data.slotText3 = this.slotText3;
    }
    // For loading data of slots
    public void LoadData(GameData data)
    {
        this.slotText1 = data.slotText1;
        this.slotText2 = data.slotText2;
        this.slotText3 = data.slotText3;
    }

    public void SaveSlot1()
    {
        inCreate = true;
        filename = "/AMaze1.json";
        slotText1 = "AMaze1";
        Time.timeScale = 0f;
    }
    public void SaveSlot2()
    {
        inCreate = true;
        filename = "/AMaze2.json";
        slotText2 = "AMaze2";
        Time.timeScale = 0f;
    }
    public void SaveSlot3()
    {
        inCreate = true;
        filename = "/AMaze3.json";
        slotText3 = "AMaze3";
        Time.timeScale = 0f;
    }
    public void SaveButton()
    {
        SaveLevel(filename);
    }
    public void SaveLevel(string filename)
    {
        //BoundsInt bounds = tilemap.cellBounds;
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

        // Saving tiles
        foreach (int tile in tileData)
        {
            levelData.tiles.Add(tile);
        }
        foreach (Vector3Int tilePos in tilePositionData)
        {
            levelData.tilePosition.Add(tilePos);
        }
        // Saving prefabs
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
        if (!(slotText1 == "Empty"))
        {
            LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze1.json"));
        }
        else
        {
            Debug.LogError("File not found");
            //ALSO CREATE A NEW CANVAS FOR NO FILES
        }
    }
    public void LoadSlot2()
    {
        if (!(slotText2 == "Empty"))
        {
            LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze2.json"));
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
    public void LoadSlot3()
    {
        print("called load 3");
        if (!(slotText3 == "Empty"))
        {
            LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze3.json"));
        }
        else
        {
            Debug.LogError("File not found");
        }
    }
    public void LoadLevel(string json)
    {
        prefabCollector.SetActive(false);
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
