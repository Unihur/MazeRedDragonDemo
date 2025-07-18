using System.Diagnostics;
using UnityEngine;

public class ChangeCameraDistanceTo6 : MonoBehaviour
{
    public string cameraName = "Main Camera"; // Main Camera ������
    public float newDistance = 6f; // �µľ���ֵ������Ϊ 6

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
                    // �޸ľ���Ϊ 6
                    platformerCamera.Distance = newDistance;
                }
                else
                {
                }
            }
            else
            {
            }
        }
    }
}