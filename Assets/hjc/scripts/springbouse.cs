using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float jumpHeight = 5f; // 弹跳的高度

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // 直接设置玩家的垂直速度
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, CalculateJumpVelocity(jumpHeight), playerRigidbody.velocity.z);
            }
        }
    }

    // 计算达到指定高度所需的速度
    private float CalculateJumpVelocity(float height)
    {
        // 使用公式 v = sqrt(2 * g * h)，这里 g 是重力加速度（假设为 9.81）
        return Mathf.Sqrt(2f * 9.81f * height);
    }
}