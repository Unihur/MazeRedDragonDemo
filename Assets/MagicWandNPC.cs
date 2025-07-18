using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicWandNPC : MonoBehaviour
{
    public GameObject KeyInfo;
    public GameObject TalkPlane;
    private Text talkText;
    private string[] dialogLines;
    private int currentLineIndex = 0;
    private bool isDialogueCompleted = false; // 对话完成标志

    void Start()
    {
        if (KeyInfo != null)
            KeyInfo.SetActive(false);
        if (TalkPlane != null)
        {
            TalkPlane.SetActive(false);
            talkText = TalkPlane.GetComponentInChildren<Text>();
        }

        dialogLines = new string[]
        {
            "你终于回来了，亲爱的勇者。",
            "在雾的此端，我们的家乡卡利亚。",
            "从地狱中归来的红龙吞噬了国王……",
            "它意图使用卡利亚王室的力量打开地狱与人界的通道……",
            "快去阻止他吧。",
            "国王将权杖一分为三，藏在城堡的三个角落。",
            "你要将它们找齐。",
            "然后，杀死红龙……"
        };
    }

    void Update()
    {
        if (KeyInfo.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            ShowNextDialogue();
        }
    }

    private void ShowNextDialogue()
    {
        if (isDialogueCompleted)
        {
            // 如果对话已完成，只显示 "前进吧"
            if (TalkPlane != null)
            {
                TalkPlane.SetActive(true);
                talkText.text = "前进吧";
            }
            return;
        }

        if (TalkPlane != null && currentLineIndex < dialogLines.Length)
        {
            TalkPlane.SetActive(true);
            talkText.text = dialogLines[currentLineIndex];
            currentLineIndex++;

            // 如果到了最后一行对话，设置对话完成标志并重置索引
            if (currentLineIndex >= dialogLines.Length)
            {
                isDialogueCompleted = true;
                currentLineIndex = 0;
                KeyInfo.SetActive(false); // 禁用 KeyInfo 因为对话已完成
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!isDialogueCompleted && KeyInfo != null)
                KeyInfo.SetActive(true);
            else if (isDialogueCompleted && TalkPlane != null)
            {
                TalkPlane.SetActive(true);
                talkText.text = "前进吧"; // 显示 "前进吧" 提示
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (KeyInfo != null)
                KeyInfo.SetActive(false);
            if (TalkPlane != null)
            {
                TalkPlane.SetActive(false);
            }
        }
    }
}