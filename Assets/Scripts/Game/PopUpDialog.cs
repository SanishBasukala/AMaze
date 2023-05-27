using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpDialog : MonoBehaviour
{
	public static PopUpDialog Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI textMeshPro;
	[SerializeField] private Button yesButton;
	[SerializeField] private Button noButton;
	private void Awake()
	{
		try
		{
			yesButton = transform.Find("YesButton").GetComponent<Button>();
			noButton = transform.Find("NoButton").GetComponent<Button>();
			textMeshPro = transform.Find("AlertText").GetComponent<TextMeshProUGUI>();
		}
		catch (NullReferenceException e)
		{
			Debug.Log(e);
		}


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
