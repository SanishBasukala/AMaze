using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowingLevel : MonoBehaviour
{
    public string placeName;
    public GameObject text;
    public Text placeText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaceNameCo());
    }
    private IEnumerator PlaceNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(1.3f);
        text.SetActive(false);
    }
}
