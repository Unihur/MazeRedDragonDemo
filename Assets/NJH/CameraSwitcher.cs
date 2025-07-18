using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

    private int currentCameraIndex = 0;

    void Start()
    {
        // ���ó��˵�ǰ���֮����������
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == currentCameraIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ����û��������л����
        if (Input.GetKeyDown(KeyCode.V))
        {
            // ���õ�ǰ���
            cameras[currentCameraIndex].gameObject.SetActive(false);

            // �����������л�����һ�����
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // �����µ����
            cameras[currentCameraIndex].gameObject.SetActive(true);
        }
    }
}

