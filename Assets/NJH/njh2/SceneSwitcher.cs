using UnityEngine;
using UnityEngine.SceneManagement; // ���볡������

public class SceneSwitcher : MonoBehaviour
{
    void Start()
    {
        Invoke("SwitchToNextScene", 5f); // 5 �������л������ķ���
    }

    void SwitchToNextScene()
    {
        // ��ȡ��ǰ��������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // �л�����һ����������
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
