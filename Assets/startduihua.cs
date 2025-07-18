using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDuihua : MonoBehaviour
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
            "勇士啊，你终于回来了，",
            "邪恶的红龙为我们的回乡之路，",
            "设立了无数机关……",
            "但是，你是如此勇武的勇者，",
            "那些困难对你来说不过是小菜一碟吧。",
            "在城堡之外，红龙的眷属，",
            "“铁锤”正助纣为虐，",
            "阻碍着勇者们的回乡之路。",
            "伟大的勇者",
            "请帮我们将他击败",
            "为我们开辟回乡的路！"
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