using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SceneLoader : MonoBehaviour
{
    public Dropdown sceneDropdown; // 下拉框组件
    public Button startButton;     // 开始游戏按钮

    void Start()
    {
        // 绑定按钮点击事件
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        // 获取下拉框选中的选项索引
        int selectedIndex = sceneDropdown.value;

        // 根据选项索引加载场景
        switch (selectedIndex)
        {
            case 0:
                SceneManager.LoadScene("SampleScene"); // 加载场景1
                break;
            case 1:
                SceneManager.LoadScene("Prototype"); // 加载场景2
                break;
            case 2:
                SceneManager.LoadScene("room1end"); // 加载场景3
                break;
            case 3:
                SceneManager.LoadScene("room2"); // 加载场景4
                break;
            case 4:
                SceneManager.LoadScene("end"); // 加载场景5
                break;
            default:
                break;
        }
    }
}