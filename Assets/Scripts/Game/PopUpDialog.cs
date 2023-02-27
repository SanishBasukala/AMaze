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
    [SerializeField] private Button noButton;
    private void Awake()
    {
        yesButton = transform.Find("YesButton").GetComponent<Button>();
        noButton = transform.Find("NoButton").GetComponent<Button>();
        textMeshPro = transform.Find("AlertText").GetComponent<TextMeshProUGUI>();


        Instance = this;
        Hide();
    }
    public void ShowDialog(string dialogText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);
        textMeshPro.text = dialogText;
        yesButton.onClick.AddListener(() =>
        {
            Hide();
            yesAction();
        });
        noButton.onClick.AddListener(() =>
        {
            Hide();
            noAction();
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
