using System.Diagnostics;
using UnityEngine;

public class ChangeCameraDistance : MonoBehaviour
{
    public string cameraName = "Main Camera"; // Main Camera 的名称
    public float newDistance = 18f; // 新的距离值

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的物体是否是主角
        if (other.CompareTag("Player"))
        {
            // 查找 Main Camera 物体
            GameObject mainCamera = GameObject.Find(cameraName);
            if (mainCamera != null)
            {
                // 获取 PlatformerCamera 脚本
                Platformer.PlatformerCamera platformerCamera = mainCamera.GetComponent<Platformer.PlatformerCamera>();
                if (platformerCamera != null)
                {
                    // 修改距离
                    platformerCamera.Distance = newDistance;
                }
                else
                {
                    //Debug.LogWarning("Platformer Camera脚本未找到！");
                }
            }
            else
            {
                //Debug.LogWarning("Main Camera未找到！");
            }
        }
    }
}