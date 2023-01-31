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

    public GameObject myPrefab;
    public Vector3Int changeposition;

    private void Start()
    {

        //Instantiate(myPrefab, highlightedTilePos, Quaternion.identity);


        //GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        //Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        //transform.position = gridLayout.CellToWorld(cellPosition);
        //print(gridLayout);
        //print(cellPosition);
        //print(transform.position);
    }
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
                else if (item.type == ItemType.Prefab)
                {
                    BuildPrefab();
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
        if (currentItem.type == ItemType.BuildingBlock || currentItem.type == ItemType.Prefab)
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
    private void BuildPrefab()
    {
        Instantiate(myPrefab, highlightedTilePos * changeposition, Quaternion.identity);
    }
}