using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObject/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase tile;
    public ItemType type;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
}

public enum ItemType
{
    BuildingBlock,
    Eraser
}