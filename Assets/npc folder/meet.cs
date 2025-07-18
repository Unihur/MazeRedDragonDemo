using UnityEngine;
using UnityEngine.UI; // 确保包含了UI命名空间


public class NPC : MonoBehaviour
{
    [SerializeField] private Text promptText;

    private void Start()
    {
        // 确保在游戏开始时提示文本是隐藏的
        HidePromptToPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞体是否属于玩家
        if (other.CompareTag("Player"))
        {
            // 当玩家进入触发器区域时激活对话逻辑
            StartCoroutine(WaitForKeyPress(other));
        }
    }

    private System.Collections.IEnumerator WaitForKeyPress(Collider player)
    {
        // 显示提示信息给玩家，例如UI文本或HUD消息
        ShowPromptToPlayer();

        // 等待直到玩家按下指定按键（例如F）
        while (!Input.GetKeyDown(KeyCode.F))
        {
            yield return null; // 等待一帧
        }

        // 玩家按下了F键，现在可以显示对话框了
        ShowDialogue(player);

        // 隐藏提示信息
        HidePromptToPlayer();
    }

    private void ShowPromptToPlayer()
    {
        // 假设你有一个名为promptText的UI Text组件来显示提示信息
        if (promptText != null)
        {
            promptText.text = "Press F to talk"; // 设置提示信息
            promptText.enabled = true; // 显示提示信息
        }
    }

    private void ShowDialogue(Collider player)
    {
        // 实现代码以显示对话框
    }

    private void HidePromptToPlayer()
    {
        // 如果promptText存在，则将其隐藏
        if (promptText != null)
        {
            promptText.enabled = false; // 隐藏提示信息
        }
    }
}



