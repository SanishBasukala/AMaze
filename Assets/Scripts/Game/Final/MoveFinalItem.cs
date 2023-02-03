using UnityEngine;

public class MoveFinalItem : MonoBehaviour
{
    public Camera cam;
    public GameObject correctForm;
    private bool moving;
    private bool inPosition;

    private float startPosX, startPosY;

    private Vector3 resetPosition;
    // Start is called before the first frame update
    void Start()
    {
        resetPosition = this.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inPosition)
        {
            if (moving)
            {
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = cam.ScreenToWorldPoint(mousePos);

                this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, this.gameObject.transform.localPosition.z);
            }
        }

    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = cam.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            moving = true;
        }
    }

    private void OnMouseUp()
    {
        moving = false;

        if (Mathf.Abs(this.transform.localPosition.x - correctForm.transform.localPosition.x) <= 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - correctForm.transform.localPosition.y) <= 0.5f)
        {
            this.transform.position = new Vector3(correctForm.transform.position.x, correctForm.transform.position.y, correctForm.transform.position.z);
            inPosition = true;

            GameObject.Find("PointHandler").GetComponent<FinalDoor>().AddPoint();
        }
        else
        {
            this.transform.localPosition = new Vector3(resetPosition.x, resetPosition.y, resetPosition.z);
        }
    }
}
