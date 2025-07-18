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
    private bool isDialogueCompleted = false; // �Ի���ɱ�־

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
            "��ʿ���������ڻ����ˣ�",
            "а��ĺ���Ϊ���ǵĻ���֮·��",
            "�������������ء���",
            "���ǣ����������������ߣ�",
            "��Щ���Ѷ�����˵������С��һ���ɡ�",
            "�ڳǱ�֮�⣬�����ľ�����",
            "��������������ΪŰ��",
            "�谭�������ǵĻ���֮·��",
            "ΰ�������",
            "������ǽ�������",
            "Ϊ���ǿ��ٻ����·��"
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
            // ����Ի�����ɣ�ֻ��ʾ "ǰ����"
            if (TalkPlane != null)
            {
                TalkPlane.SetActive(true);
                talkText.text = "ǰ����";
            }
            return;
        }

        if (TalkPlane != null && currentLineIndex < dialogLines.Length)
        {
            TalkPlane.SetActive(true);
            talkText.text = dialogLines[currentLineIndex];
            currentLineIndex++;

            // ����������һ�жԻ������öԻ���ɱ�־����������
            if (currentLineIndex >= dialogLines.Length)
            {
                isDialogueCompleted = true;
                currentLineIndex = 0;
                KeyInfo.SetActive(false); // ���� KeyInfo ��Ϊ�Ի������
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
                talkText.text = "ǰ����"; // ��ʾ "ǰ����" ��ʾ
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