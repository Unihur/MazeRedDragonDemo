using UnityEngine;
using UnityEngine.SceneManagement;
using Platformer; // ��������ռ�����

public class SceneChange : MonoBehaviour
{
    public string targetSceneName = "room2"; // Ŀ�곡������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ��ȡ��ҵ� CharacterInventory ���
            CharacterInventory inventory = other.GetComponent<CharacterInventory>();
            if (inventory != null)
            {
                inventory.SaveInventory(); // ����������
            }

            // �л�����
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