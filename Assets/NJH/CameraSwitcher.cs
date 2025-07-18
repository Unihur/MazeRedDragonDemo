using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

    private int currentCameraIndex = 0;

    void Start()
    {
        // 禁用除了当前相机之外的所有相机
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 检测用户输入来切换相机
        if (Input.GetKeyDown(KeyCode.V))
        {
            // 禁用当前相机
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // 增加索引来切换到下一个相机
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // 启用新的相机
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}

