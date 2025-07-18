using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer; // 添加命名空间引用

public class SceneChange : MonoBehaviour
{
    public string targetSceneName = "room2"; // 目标场景名称

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 获取玩家的 CharacterInventory 组件
            CharacterInventory inventory = other.GetComponent<CharacterInventory>();
            if (inventory != null)
            {
                inventory.SaveInventory(); // 保存库存数据
            }

            // 切换场景
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                Debug.LogError("Target scene name is not set!");
            }
        }
    }
}