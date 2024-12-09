using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BossRoomManager : MonoBehaviour
{
    public GameObject boss;
    public GameObject winCanvas;
    public GameObject dialogueCanvas;
    public TextMeshProUGUI dialogueText;

    public List<string> dialogueLines;
    private int currentDialogueIndex = 0;
    private bool isDialogueActive = false;
    public GameObject stoneGolemImg;

    private void Start()
    {
        StartCoroutine(StartDialogueAfterSecond());
        if (dialogueLines.Count > 0)
        {
            StartDialogue();
        }
    }
    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            ShowNextDialogue();
        }
        if (boss == null)
        {
            winCanvas.SetActive(true);
        }
    }
    private void StartDialogue()
    {
        isDialogueActive = true;
        currentDialogueIndex = 0;
        dialogueText.text = dialogueLines[currentDialogueIndex];
    }

    private void ShowNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex == 3)
        { 
            stoneGolemImg.SetActive(true);
        }
        if (currentDialogueIndex < dialogueLines.Count)
        {
            dialogueText.text = dialogueLines[currentDialogueIndex];
        }
        else
        {
            isDialogueActive = false;
            dialogueCanvas.SetActive(false);
        }
    }
    IEnumerator StartDialogueAfterSecond()
    {
        yield return new WaitForSeconds(0.75f);
        dialogueCanvas.SetActive(true);
    }
}
