using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private int pointsToWin = 3;
    private int currentPoints = 0;
    public GameObject myFinalItems;
    // Start is called before the first frame update
    void Start()
    {
        pointsToWin = myFinalItems.transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPoints >= pointsToWin)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void AddPoint()
    {
        currentPoints++;
    }
}
