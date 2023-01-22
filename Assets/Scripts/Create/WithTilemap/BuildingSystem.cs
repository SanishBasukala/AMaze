using System;
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

    [SerializeField] private GameObject lootPrefab;

    private Vector3Int playerPos;
    private Vector3Int highlightedTilePos;
    private bool highlighted;

    private void Update()
    {
        Item item = InventoryManager.instance.GetSelectedItem(false);

        playerPos = mainTilemap.WorldToCell(transform.position);
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
                else if (item.type == ItemType.Tool)
                {
                    Destroy(highlightedTilePos);
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

            if (InRange(playerPos, mouseGridPos, (Vector3Int)currentItem.range))
            {
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
            else
            {
                highlighted = false;
            }
        }
    }

    private bool InRange(Vector3Int positionA, Vector3Int positionB, Vector3Int range)
    {
        Vector3Int distance = positionA - positionB;
        if (Math.Abs(distance.x) >= range.x ||
            Math.Abs(distance.y) >= range.y)
        {
            return false;
        }
        return true;
    }

    private bool CheckCondition(RuleTileWithData tile, Item currentItem)
    {
        if (currentItem.type == ItemType.BuildingBlock)
        {
            if (!tile)
            {
                return true;
            }
            else if (currentItem.type == ItemType.Tool)
            {
                if (tile)
                {
                    if (tile.item.actionType == currentItem.actionType)
                    {
                        return true;
                    }
                }
            }

        }
        return false;
    }

    private void Build(Vector3Int position, Item itemToBuild)
    {
        tempTilemap.SetTile(position, null);
        highlighted = false;
        mainTilemap.SetTile(position, itemToBuild.tile);
    }
    private void Destroy(Vector3Int position)
    {
        tempTilemap.SetTile(position, null);
        highlighted = false;

        RuleTileWithData tile = mainTilemap.GetTile<RuleTileWithData>(position);
        mainTilemap.SetTile(position, null);

        Vector3 pos = mainTilemap.GetCellCenterWorld(position);
        GameObject loot = Instantiate(lootPrefab, pos, Quaternion.identity);
        loot.GetComponent<Loot>().Initialize(tile.item);
    }
}
