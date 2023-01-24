using UnityEngine;
using UnityEngine.EventSystems;
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
        Item item = InventoryManager.instance.GetSelectedItem(); //false
        if (item != null)
        {
            HighlightTile(item);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (highlighted)
            {
                if (item.type == ItemType.BuildingBlock)
                {
                    Build(highlightedTilePos, item);
                }
            }
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
            if (CheckCondition(mainTilemap.GetTile<RuleTileWithData>(mouseGridPos), currentItem))
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

    private bool CheckCondition(RuleTileWithData tile, Item currentItem)
    {
        if (currentItem.type == ItemType.BuildingBlock)
        {
            if (!tile)
            {
                return true;
            }
        }
        return false;
    }

    private void Build(Vector3Int position, Item itemToBuild)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            InventoryManager.instance.GetSelectedItem(); //true

            tempTilemap.SetTile(position, null);
            highlighted = false;
            mainTilemap.SetTile(position, itemToBuild.tile);
        }

    }
}