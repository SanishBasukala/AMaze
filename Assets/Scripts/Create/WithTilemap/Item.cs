using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObject/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public int itemId;


    public TileBase tile;
    public GameObject myPrefab;
    public ItemType type;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
}

public enum ItemType
{
    BuildingBlock,
    PanTool,
    Prefab
}