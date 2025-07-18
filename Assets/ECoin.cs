using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public Text hintText; // 引用UI文本
    public string hintMessage = "按 E 提交金币给骑士加强攻击"; // 提示信息

    private void OnTriggerEnter(Collider other)
    {
        // 检测碰撞对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 更新提示文本
            hintText.text = hintMessage;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检测碰撞对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 清空提示文本
            hintText.text = "";
        }
    }
}