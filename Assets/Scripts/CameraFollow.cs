using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [SerializeField] Camera cam;
    [SerializeField] float zoom, maxCamSize, minCamSize;
    private Vector3 dragOrigin;

    [SerializeField] SpriteRenderer mapRenderer;
    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    //Camera follows the player
    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
    private void Update()
    {
        PanCamera();
        zoomInOut();
    }
    //Panning camera
    private void PanCamera()
    {
        //save positn of mouse in world space when deag starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        //calculate distance between drag origin and new positon if it is still held down
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            //move the camera by that distance

            cam.transform.position = ClampCamera(cam.transform.position + difference);

        }
    }
    //For zoom in and out
    private void zoomInOut()
    {
        if (cam.orthographic)
        {
            float newSize = cam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoom;
            cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        }
        else
        {
            float newSize = cam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * zoom;
            cam.fieldOfView = Mathf.Clamp(newSize, minCamSize, maxCamSize);
        }

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
