using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject menuPanel; // �˵����
    public Slider volumeSlider;  // �������ڻ���

    // ͨ����ק�󶨵İ�ť
    public Button continueButton;    // ������Ϸ��ť
    public Button mainMenuButton;    // �˻������水ť
    public Button quitButton;        // �˳������水ť

    private bool isMenuActive = false; // �˵��Ƿ񼤻�

    void Start()
    {
        // ��ʼ���˵�״̬
        menuPanel.SetActive(false);

        // ������������ĳ�ʼֵ
        volumeSlider.value = AudioListener.volume;

        // �����������¼�
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // �󶨰�ť�¼�
        continueButton.onClick.AddListener(ContinueGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitToDesktop);
    }

    void Update()
    {
        // ���ESC������
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    // �л��˵���ʾ/����
    void ToggleMenu()
    {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);

        // ��ͣ��ָ���Ϸ
        Time.timeScale = isMenuActive ? 0 : 1;
    }

    // ������Ϸ
    void ContinueGame()
    {
        ToggleMenu();
    }

    // �˻�������
    void ReturnToMainMenu()
    {
        Time.timeScale = 1; // �ָ�ʱ��
        SceneManager.LoadScene("StartScene"); // �滻Ϊ��������泡������
    }

    // �˳�������
    void QuitToDesktop()
    {
        Application.Quit();
    }

    // ������������
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // ����ȫ������
    }
}