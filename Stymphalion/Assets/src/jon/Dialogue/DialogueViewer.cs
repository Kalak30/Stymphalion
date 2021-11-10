using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumDialogue;
using UnityEngine.UI;
using TMPro;

public class DialogueViewer : MonoBehaviour
{
    public Transform choices;
    public TextMeshProUGUI choiceTemplate;
    public QD_DialogueHandler handler;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI speakerName;
    private List<TextMeshProUGUI> activeChoices = new List<TextMeshProUGUI>();
    private bool ended;
    private List<TextMeshProUGUI> inactiveChoices = new List<TextMeshProUGUI>();

    public void Choose(int choice)
    {
        if (ended)
            return;

        Next(choice);
    }

    public void Next(int choice = -1)
    {
        if (ended)
            return;

        // Go to the next message
        handler.NextMessage(choice);
        // Set the new text
        SetText();
        // End if there is no next message
        if (handler.currentMessageInfo.ID < 0)
            ended = true;
    }

    public void setDialogue(int quest_no)
    {
        switch (quest_no)
        {
            case 1:
                handler.SetConversation("Quest 1");
                SetText();
                break;
        }
    }

    private void ClearChoices()
    {
        for (int i = activeChoices.Count - 1; i >= 0; --i)
        {
            // Use object pooling with the choices to prevent unecessary garbage collection
            activeChoices[i].gameObject.SetActive(false);
            activeChoices[i].text = "";
            inactiveChoices.Add(activeChoices[i]);
            activeChoices.RemoveAt(i);
        }
    }

    private void GenerateChoices()
    {
        // Exit if not a choice
        if (handler.currentMessageInfo.Type != QD_NodeType.Choice)
            return;
        // Clear the old choices
        ClearChoices();
        // Generate new choices
        QD_Choice choice = handler.GetChoice();
        int added = 0;
        // Use inactive choices instead of making new ones, if possible
        while (inactiveChoices.Count > 0 && added < choice.Choices.Count)
        {
            int i = inactiveChoices.Count - 1;
            TextMeshProUGUI cText = inactiveChoices[i];
            cText.text = choice.Choices[added];
            ChoiceButton button = cText.GetComponent<ChoiceButton>();
            button.m_number = added;
            cText.gameObject.SetActive(true);
            activeChoices.Add(cText);
            inactiveChoices.RemoveAt(i);
            added++;
        }
        // Make new choices if any left to make
        while (added < choice.Choices.Count)
        {
            TextMeshProUGUI newChoice = Instantiate(choiceTemplate, choices);
            newChoice.text = choice.Choices[added];
            ChoiceButton button = newChoice.GetComponent<ChoiceButton>();
            button.m_number = added;
            newChoice.gameObject.SetActive(true);
            activeChoices.Add(newChoice);
            added++;
        }
    }

    private void SetText()
    {
        // Clear everything
        speakerName.text = "";
        messageText.gameObject.SetActive(false);
        messageText.text = "";
        ClearChoices();

        // If at the end, don't do anything
        if (ended)
            return;

        // Generate choices if a choice, otherwise display the message
        if (handler.currentMessageInfo.Type == QD_NodeType.Message)
        {
            QD_Message message = handler.GetMessage();
            speakerName.text = message.SpeakerName;
            messageText.text = message.MessageText;
            messageText.gameObject.SetActive(true);
        }
        else if (handler.currentMessageInfo.Type == QD_NodeType.Choice)
        {
            speakerName.text = "Player";
            GenerateChoices();
        }
    }

    private void Update()
    {
        // Don't do anything if the conversation is over
        if (ended)
            return;

        // Check if the space key is pressed and the current message is not a choice
        if (handler.currentMessageInfo.Type == QD_NodeType.Message && Input.GetKeyUp(KeyCode.Space))
            Next();
    }
}