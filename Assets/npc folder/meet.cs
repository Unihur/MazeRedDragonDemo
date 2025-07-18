using UnityEngine;
using UnityEngine.UI; // ȷ��������UI�����ռ�


public class NPC : MonoBehaviour
{
    [SerializeField] private Text promptText;

    private void Start()
    {
        // ȷ������Ϸ��ʼʱ��ʾ�ı������ص�
        HidePromptToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����ײ���Ƿ��������
        if (other.CompareTag("Player"))
        {
            // ����ҽ��봥��������ʱ����Ի��߼�
            StartCoroutine(WaitForKeyPress(other));
        }
    }

    private System.Collections.IEnumerator WaitForKeyPress(Collider player)
    {
        // ��ʾ��ʾ��Ϣ����ң�����UI�ı���HUD��Ϣ
        ShowPromptToPlayer();

        // �ȴ�ֱ����Ұ���ָ������������F��
        while (!Input.GetKeyDown(KeyCode.F))
        {
            yield return null; // �ȴ�һ֡
        }

        // ��Ұ�����F�������ڿ�����ʾ�Ի�����
        ShowDialogue(player);

        // ������ʾ��Ϣ
        HidePromptToPlayer();
    }

    private void ShowPromptToPlayer()
    {
        // ��������һ����ΪpromptText��UI Text�������ʾ��ʾ��Ϣ
        if (promptText != null)
        {
            promptText.text = "Press F to talk"; // ������ʾ��Ϣ
            promptText.enabled = true; // ��ʾ��ʾ��Ϣ
        }
    }

    private void ShowDialogue(Collider player)
    {
        // ʵ�ִ�������ʾ�Ի���
    }

    private void HidePromptToPlayer()
    {
        // ���promptText���ڣ���������
        if (promptText != null)
        {
            promptText.enabled = false; // ������ʾ��Ϣ
        }
    }
}



