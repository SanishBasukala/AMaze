using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HoverText : MonoBehaviour
{
    public string textString;
    public GameObject text;
    public Text hoverText;
    public GameObject saveAndLoad;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(PlaceNameCo());
    }
    private IEnumerator PlaceNameCo()
    {
        Time.timeScale = 1f;
        text.SetActive(true);
        hoverText.text = textString;
        yield return new WaitForSeconds(1.3f);
        text.SetActive(false);
        saveAndLoad.SetActive(true);

    }
}