using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SaveHandler : MonoBehaviour, IDataPersistence
{
	public static SaveHandler instance;
	public Tilemap tilemap;
	public Text[] slotState;

	//private string slotText1;
	//private string slotText2;
	//private string slotText3;

	private string filename;

	public bool inCreate;
	public Camera mainCamera;
	public Camera playerCamera;
	public GameObject player;

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
	Menuscript menuscript = new();
	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(this);
	}

	private void Update()
	{

		if (inCreate)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}

		slotState[0].text = PlayerPrefs.GetString("Slot1");
		slotState[3].text = PlayerPrefs.GetString("Slot1");
		slotState[1].text = PlayerPrefs.GetString("Slot2");
		slotState[4].text = PlayerPrefs.GetString("Slot2");
		slotState[2].text = PlayerPrefs.GetString("Slot3");
		slotState[5].text = PlayerPrefs.GetString("Slot3");

		if (File.Exists(Application.dataPath + "/AMaze1.json"))
		{
			PlayerPrefs.SetString("Slot1", "AMaze1");
			slotState[0].text = "AMaze1";
			slotState[3].text = "AMaze1";
		}
		if (File.Exists(Application.dataPath + "/AMaze2.json"))
		{
			PlayerPrefs.SetString("Slot2", "AMaze2");
			slotState[1].text = "AMaze2";
			slotState[4].text = "AMaze2";
		}
		if (File.Exists(Application.dataPath + "/AMaze3.json"))
		{
			PlayerPrefs.SetString("Slot3", "AMaze3");
			slotState[2].text = "AMaze3";
			slotState[5].text = "AMaze3";
		}


	}

	private string[] jsonURLs = {
							"https://drive.google.com/uc?export=download&id=18fLu_SPhc39-_RzkbPEY8fRJqrBoQkF1",
							"https://drive.google.com/uc?export=download&id=1rq9LznA1J5LvydSofoMd3kJvDIwulLWd",
							"https://drive.google.com/uc?export=download&id=13xvs7HJIxWUq_KUiYlIddVae549gvn_f"
	};

	public void LoadFromDrive(int mapID)
	{
		if (mapID == 1)
		{
			StartCoroutine(GetData(jsonURLs[0]));
		}
		else if (mapID == 2)
		{
			StartCoroutine(GetData(jsonURLs[1]));
		}
		else if (mapID == 3)
		{
			StartCoroutine(GetData(jsonURLs[2]));
		}
	}
	// Getting web request
	IEnumerator GetData(string url)
	{
		UnityWebRequest request = UnityWebRequest.Get(url);

		yield return request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.ConnectionError)
		{
			Debug.Log("Error");
		}
		else
		{
			Debug.Log("Success");
			string data = request.downloadHandler.text;
			LoadLevel(data);
		}

		request.Dispose();
	}

	// For saving data of slots
	public void SaveData(ref GameData data)
	{
		data.slotText1 = PlayerPrefs.GetString("Slot1");
		data.slotText2 = PlayerPrefs.GetString("Slot2");
		data.slotText3 = PlayerPrefs.GetString("Slot3");
	}
	// For loading data of slots
	public void LoadData(GameData data)
	{
		PlayerPrefs.SetString("Slot1", data.slotText1);
		PlayerPrefs.SetString("Slot2", data.slotText2);
		PlayerPrefs.SetString("Slot3", data.slotText3);
	}
	public void SaveSlots(int slotNumber)
	{
		CheckExistingFile(slotNumber);
		inCreate = true;
		if (slotNumber == 1)
		{
			filename = "/AMaze1.json";
			PlayerPrefs.SetString("Slot1", "AMaze1");
		}
		else if (slotNumber == 2)
		{
			filename = "/AMaze2.json";
			PlayerPrefs.SetString("Slot2", "AMaze2");
		}
		else if (slotNumber == 3)
		{
			filename = "/AMaze3.json";
			PlayerPrefs.SetString("Slot3", "AMaze3");
		}
		else
		{
			Debug.Log("Slot does not exist");
		}
	}

	private void CheckExistingFile(int slotNumber)
	{
		if (slotNumber == 1)
		{
			if (!(PlayerPrefs.GetString("Slot1") == "Empty"))
			{
				PopUpDialog.Instance.ShowDialog("Do you want to create a new map in slot 1?", () =>
				{
					PlayerPrefs.SetString("Slot1", "Empty");
					tilemap.ClearAllTiles();
				}, () =>
				{
					menuscript.GetCreateScene();
				});
			}
		}
		else if (slotNumber == 2)
		{
			if (!(PlayerPrefs.GetString("Slot2") == "Empty"))
			{
				PopUpDialog.Instance.ShowDialog("Do you want to create a new map in slot 2?", () =>
				{
					PlayerPrefs.SetString("Slot2", "Empty");
					tilemap.ClearAllTiles();
				}, () =>
				{
					menuscript.GetCreateScene();
				});
			}
		}
		else if (slotNumber == 3)
		{
			if (!(PlayerPrefs.GetString("Slot3") == "Empty"))
			{
				PopUpDialog.Instance.ShowDialog("Do you want to create a new map in slot 3?", () =>
				{
					PlayerPrefs.SetString("Slot3", "Empty");
					tilemap.ClearAllTiles();
				}, () =>
				{
					menuscript.GetCreateScene();
				});
			}
		}
		else
		{
			Debug.Log("Slot number out of bound");
		}
	}
	public void SaveButton()
	{
		inCreate = false;
		SaveLevel(filename);
	}
	public void SaveLevel(string filename)
	{
		LevelData levelData = new();

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
		string json = JsonUtility.ToJson(levelData, true);
		File.WriteAllText(Application.dataPath + filename, json);
	}

	public void LoadSlots(int slotNumber)
	{
		if (slotNumber == 1)
		{
			if (!(PlayerPrefs.GetString("Slot1") == "Empty"))
			{
				LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze1.json"));
			}
			else
			{
				OkDialog.Instance.ShowDialog("You have not created a map in this slot.", () =>
				{
					menuscript.GetCreateScene();
				});
			}
		}
		else if (slotNumber == 2)
		{
			if (!(PlayerPrefs.GetString("Slot2") == "Empty"))
			{
				LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze2.json"));
			}
			else
			{
				OkDialog.Instance.ShowDialog("You have not created a map in this slot.", () =>
				{
					menuscript.GetCreateScene();
				});
			}
		}
		else if (slotNumber == 3)
		{
			if (!(PlayerPrefs.GetString("Slot3") == "Empty"))
			{
				LoadLevel(File.ReadAllText(Application.dataPath + "/AMaze3.json"));
			}
			else
			{
				OkDialog.Instance.ShowDialog("You have not created a map in this slot.", () =>
				{
					menuscript.GetCreateScene();
				});
			}
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

		FindObjectOfType<PlayerHealth>().inLoad = true;

		playerCamera.gameObject.SetActive(true);
		mainCamera.gameObject.SetActive(false);

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
