using System.Diagnostics;
using UnityEngine;

public class ChangeCameraDistance : MonoBehaviour
{
    public string cameraName = "Main Camera"; // Main Camera ������
    public float newDistance = 18f; // �µľ���ֵ

    private void OnTriggerEnter(Collider other)
    {
        // �����봥�����������Ƿ�������
        if (other.CompareTag("Player"))
        {
            // ���� Main Camera ����
            GameObject mainCamera = GameObject.Find(cameraName);
            if (mainCamera != null)
            {
                // ��ȡ PlatformerCamera �ű�
                Platformer.PlatformerCamera platformerCamera = mainCamera.GetComponent<Platformer.PlatformerCamera>();
                if (platformerCamera != null)
                {
                    // �޸ľ���
                    platformerCamera.Distance = newDistance;
                }
                else
                {
                    //Debug.LogWarning("Platformer Camera�ű�δ�ҵ���");
                }
            }
            else
            {
                //Debug.LogWarning("Main Cameraδ�ҵ���");
            }
        }
    }
}