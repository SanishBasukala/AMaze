using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    [SerializeField]
    private TileBase highlightTile;
    [SerializeField]
    private Tilemap mainTilemap;
    [SerializeField]
    private Tilemap tempTilemap;

    private Vector3Int highlightedTilePos;
    private bool highlighted;

    private void Update()
    {
        Item item = InventoryManager.instance.GetSelectedItem(false);
        if (item != null)
        {
            HighlightTile(item);
        }
    }
    private Vector3Int GetMourseOnGirdPos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int mouseCellPos = mainTilemap.WorldToCell(mousePos);
        mouseCellPos.z = 0;

        return mouseCellPos;
    }

    private void HighlightTile(Item currentItem)
    {
        Vector3Int mouseGridPos = GetMourseOnGirdPos();

        if (highlightedTilePos != mouseGridPos)
        {
            tempTilemap.SetTile(highlightedTilePos, null);

            TileBase tile = mainTilemap.GetTile(mouseGridPos);

            if (tile)
            {
                tempTilemap.SetTile(mouseGridPos, highlightTile);
                highlightedTilePos = mouseGridPos;

                highlighted = true;
            }
            else
            {
                highlighted = false;
            }
        }


    }
}
