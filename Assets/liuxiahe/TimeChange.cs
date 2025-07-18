using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public GameObject KeyInfo; // ������ʾ��ʾ��Ϣ��UI GameObject
    public GameObject brickGroup1; // ש����1
    public GameObject brickGroup2; // ש����2

    private bool playerInTrigger = false;

    private void Start()
    {
        // ��ʼ��ʱ������ʾ��Ϣ
        if (KeyInfo != null)
        {
            KeyInfo.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            // ��ʾ��ʾ��Ϣ
            if (KeyInfo != null)
            {
                KeyInfo.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            // ������ʾ��Ϣ
            if (KeyInfo != null)
            {
                KeyInfo.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            ToggleBrickGroups();
        }
    }

    private void ToggleBrickGroups()
    {
        if (brickGroup1 != null)
        {
            brickGroup1.SetActive(!brickGroup1.activeSelf);
        }

        if (brickGroup2 != null)
        {
            brickGroup2.SetActive(!brickGroup2.activeSelf);
        }
    }
}