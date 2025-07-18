using UnityEngine;
using UnityEngine.SceneManagement; // 引入场景管理

public class SceneSwitcher : MonoBehaviour
{
    void Start()
    {
        Invoke("SwitchToNextScene", 5f); // 5 秒后调用切换场景的方法
    }

    void SwitchToNextScene()
    {
        // 获取当前场景索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 切换到下一个场景索引
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
