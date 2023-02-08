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

    private List<GameObject> prefabsData = new();
    private List<Vector2> prefabPositionData = new();

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
        foreach (GameObject pre in prefabsData)
        {
            levelData.prefabs.Add(pre);
        }
        foreach (Vector2 prePos in prefabPositionData)
        {
            levelData.prefabsPosition.Add(prePos);
        }

        string json = JsonUtility.ToJson(levelData, true);
        //File.WriteAllText(Application.dataPath + "/AMaze.json", json);
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

        for (int i = 0; i < data.tiles.Count; i++)
        {
            tilemap.SetTile(data.pos[i], data.tiles[i]);
        }
        for (int i = 0; i < data.prefabs.Count; i++)
        {
            Instantiate(data.prefabs[i], data.prefabsPosition[i], Quaternion.identity);
        }
    }

    public void CollectPrefabs(Vector2 pos, GameObject Prefab)
    {
        prefabsData.Add(Prefab);
        prefabPositionData.Add(pos);
    }
}

public class LevelData
{
    public List<TileBase> tiles = new();
    public List<Vector3Int> pos = new();
    public List<GameObject> prefabs = new();
    public List<Vector2> prefabsPosition = new();
}
