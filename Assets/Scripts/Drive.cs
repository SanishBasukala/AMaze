using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

//Json element data
public struct Data
{
    public string Name;
}

public class Drive : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI uiText;

    private string jsonURL = "https://drive.google.com/uc?export=download&id=18fLu_SPhc39-_RzkbPEY8fRJqrBoQkF1";

    private void Start()
    {
        StartCoroutine(GetData(jsonURL));
    }

    IEnumerator GetData(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error");
        }
        else
        {
            Debug.Log("Success");
            Data data = JsonUtility.FromJson<Data>(request.downloadHandler.text);
            Another(data);
        }

        request.Dispose();
    }
    private void Another(Data data)
    {
        uiText.text = data.Name;
    }
}
