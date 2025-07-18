using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TimeChange : MonoBehaviour
{
    public GameObject KeyInfo; // 用于显示提示信息的UI GameObject
    public GameObject brickGroup1; // 砖块组1
    public GameObject brickGroup2; // 砖块组2

    private bool playerInTrigger = false;

    private void Start()
    {
        // 初始化时隐藏提示信息
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
            // 显示提示信息
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
            // 隐藏提示信息
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