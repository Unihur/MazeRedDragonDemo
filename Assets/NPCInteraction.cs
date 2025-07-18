using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
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
            "呵呵，又一个……",
            "你也是渴望杀死红龙，加冕新王的人？",
            "不自量力！",
            "不过，如果你真的想继续向前……",
            "那就带上我的这把剑吧。",
            "我这把老骨头已经膝盖中了一箭，无法向前。",
            "但是它似乎还渴望着战斗。",
            "前面是一片破碎的空间，",
            "似乎同时存在两条时间线，",
            "通过那异色的球体在时间线内穿梭，",
            "找到我的剑……",
            "把它，插进红龙的心脏吧……"
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