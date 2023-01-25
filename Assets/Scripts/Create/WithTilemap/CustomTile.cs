using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Customtile", menuName = "LevelEditor/Tile")]
public class CustomTile : ScriptableObject
{
    public TileBase tile;
    public string id;
}
