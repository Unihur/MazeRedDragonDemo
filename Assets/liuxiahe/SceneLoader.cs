using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SceneLoader : MonoBehaviour
{
    public Dropdown sceneDropdown; // ���������
    public Button startButton;     // ��ʼ��Ϸ��ť

    void Start()
    {
        // �󶨰�ť����¼�
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    void OnStartButtonClicked()
    {
        // ��ȡ������ѡ�е�ѡ������
        int selectedIndex = sceneDropdown.value;

        // ����ѡ���������س���
        switch (selectedIndex)
        {
            case 0:
                SceneManager.LoadScene("SampleScene"); // ���س���1
                break;
            case 1:
                SceneManager.LoadScene("Prototype"); // ���س���2
                break;
            case 2:
                SceneManager.LoadScene("room1end"); // ���س���3
                break;
            case 3:
                SceneManager.LoadScene("room2"); // ���س���4
                break;
            case 4:
                SceneManager.LoadScene("end"); // ���س���5
                break;
            default:
                break;
        }
    }
}