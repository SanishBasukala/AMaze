using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public GameObject backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;

    private void Start()
    {
        isActive = true;
    }
    private void Update()
    {
        if (isActive)
        {
            Time.timeScale = 0f;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0) && isActive == true)
        {
            NextMessage();
        }
    }
    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        DisplayMessage();
    }
    void DisplayMessage()
    {
        try
        {
            Message messageToDisplay = currentMessages[activeMessage];
            messageText.text = messageToDisplay.message;

            Actor actorToDisplay = currentActors[messageToDisplay.actorId];
            actorName.text = actorToDisplay.name;
            actorImage.sprite = actorToDisplay.sprite;
        }
        catch (NullReferenceException)
        {
            return;
        }

    }
    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            Time.timeScale = 1f;
            isActive = false;
            backgroundBox.SetActive(false);
        }
    }
}
