using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject menuPanel; // 菜单面板
    public Slider volumeSlider;  // 音量调节滑块

    // 通过拖拽绑定的按钮
    public Button continueButton;    // 继续游戏按钮
    public Button mainMenuButton;    // 退回主界面按钮
    public Button quitButton;        // 退出到桌面按钮

    private bool isMenuActive = false; // 菜单是否激活

    void Start()
    {
        // 初始化菜单状态
        menuPanel.SetActive(false);

        // 设置音量滑块的初始值
        volumeSlider.value = AudioListener.volume;

        // 绑定音量滑块事件
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // 绑定按钮事件
        continueButton.onClick.AddListener(ContinueGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitToDesktop);
    }

    void Update()
    {
        // 检测ESC键按下
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // 切换菜单显示/隐藏
    void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);

        // 暂停或恢复游戏
        Time.timeScale = isMenuActive ? 0 : 1;
    }

    // 继续游戏
    void ContinueGame()
    {
        ToggleMenu();
    }

    // 退回主界面
    void ReturnToMainMenu()
    {
        Time.timeScale = 1; // 恢复时间
        SceneManager.LoadScene("StartScene"); // 替换为你的主界面场景名称
    }

    // 退出到桌面
    void QuitToDesktop()
    {
        Application.Quit();
    }

    // 调节整体音量
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // 设置全局音量
    }
}