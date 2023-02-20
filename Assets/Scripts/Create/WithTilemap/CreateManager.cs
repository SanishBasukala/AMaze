using System.Collections;
using UnityEngine;

public class CreateManager : MonoBehaviour
{
    [SerializeField] private GameObject loadEndText;
    [SerializeField] private GameObject createAndLoadGroup;
    [SerializeField] private GameObject UICanvas;
    public void LoadEnd()
    {
        print(loadEndText);
        //loadEndText.SetActive(true);
        //StartCoroutine(ResetCreate());
    }

    private IEnumerator ResetCreate()
    {
        loadEndText.SetActive(true);
        yield return new WaitForSeconds(1f);
        loadEndText.SetActive(false);
        createAndLoadGroup.SetActive(true);
        UICanvas.SetActive(false);
    }
}
