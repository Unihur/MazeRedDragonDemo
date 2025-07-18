using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text hintText; // ����UI�ı�
    public string hintMessage = "�� E �ύ��Ҹ���ʿ��ǿ����"; // ��ʾ��Ϣ

    private void OnTriggerEnter(Collider other)
    {
        // �����ײ�����Ƿ������
        if (other.CompareTag("Player"))
        {
            // ������ʾ�ı�
            hintText.text = hintMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // �����ײ�����Ƿ������
        if (other.CompareTag("Player"))
        {
            // �����ʾ�ı�
            hintText.text = "";
        }
    }
}