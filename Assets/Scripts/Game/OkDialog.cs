using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OkDialog : MonoBehaviour
{
    public static OkDialog Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button okButton;
    private void Awake()
    {
        try
        {
            okButton = transform.Find("OkButton").GetComponent<Button>();
            textMeshPro = transform.Find("AlertText").GetComponent<TextMeshProUGUI>();
        }
        catch (NullReferenceException e)
        {
            Debug.Log(e);
        }

        Instance = this;
        Hide();
    }
    public void ShowDialog(string dialogText, Action okAction)
    {
        gameObject.SetActive(true);
        textMeshPro.text = dialogText;
        okButton.onClick.AddListener(() =>
        {
            Hide();
            okAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
