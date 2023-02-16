using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpDialog : MonoBehaviour
{
    public static PopUpDialog Instance { get; private set; }

    //to use in other place
    //PopUpDialog.Instance.ShowDialog("this is it", () =>
    //{
    //Debug.Log("Yes");
    //}, () =>
    //{
    //    Debug.Log("No");
    //});

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button yesButton;
    private void Awake()
    {
        Instance = this;

        ShowDialog("this is it", () =>
        {
            Debug.Log("Yes");
        });

        //Hide();
    }
    public void ShowDialog(string dialogText, Action yesAction)
    {
        //gameObject.SetActive(true);
        textMeshPro.text = dialogText;
        yesButton.onClick.AddListener(() =>
        {
            Hide();
            yesAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
