using UnityEngine;

public class PlaceObjectsOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform square;

    [SerializeField]
    private int height;
    [SerializeField]
    private int width;

    private Vector3 mousePosition;
    private Node[,] nodes;
    private Plane plane;
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        plane = new Plane(Vector3.up, transform.position);
    }



    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
    }

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var enter))
        {
            mousePosition = ray.GetPoint(enter);
            print(mousePosition);
        }
    }
    private void CreateGrid()
    {
        nodes = new Node[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 worldPosition = new(i, j);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell" + name;
                nodes[i, j] = new Node(true, worldPosition, obj);
                name++;
            }
        }
    }
}

public class Node
{
    public bool isPlaceable;
    public Vector2 cellPosition;
    public Transform obj;


    public Node(bool isPlaceable, Vector3 cellPosition, Transform obj)
    {
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}
