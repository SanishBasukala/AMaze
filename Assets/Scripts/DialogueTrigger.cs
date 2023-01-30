using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    private void Start()
    {
        StartDialogue();
    }
    public void StartDialogue()
    {
        FindObjectOfType<DialogManager>().OpenDialogue(messages, actors);
    }
}

[Serializable]
public class Message
{
    public int actorId;
    public string message;
}
[Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}
