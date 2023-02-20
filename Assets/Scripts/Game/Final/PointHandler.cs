using System.Collections;
using UnityEngine;

public class PointHandler : MonoBehaviour
{
    private int pointsToWin = 6;
    private int currentPoints;
    void Update()
    {
        if (currentPoints >= pointsToWin)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(WaitOneSec());
            Menuscript menuscript = new();
            menuscript.ChangeScene();
        }
    }
    public void AddPoint()
    {
        currentPoints++;
    }
    IEnumerator WaitOneSec()
    {
        yield return new WaitForSeconds(1f);
    }
}
