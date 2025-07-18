using UnityEngine;

public class SpringPad : MonoBehaviour
{
    public float springForce = 10f; // 向上的弹力
    public float forwardForce = 5f; // 向前的弹力
    public Transform launchDirection; // 弹射方向
    public float gravity = -9.8f; // 手动模拟的重力加速度
    public float airDrag = 0.1f; // 空气阻力

    private bool isFlying = false; // 判断是否在空中
    private Vector3 velocity; // 当前速度
    private Rigidbody playerRb;

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是角色进入触发区域
        if (other.CompareTag("Player"))
        {
            // 获取角色的 Rigidbody
            playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // 清空速度
                velocity = Vector3.zero;

                // 计算弹射初始速度
                Vector3 launchForce = launchDirection.up * springForce + launchDirection.forward * forwardForce;
                velocity = launchForce;

                // 禁用 Rigidbody 的物理引擎控制
                playerRb.isKinematic = true;

                // 标记为飞行状态
                isFlying = true;
            }
        }
    }

    private void Update()
    {
        if (isFlying)
        {
            // 模拟重力
            velocity.y += gravity * Time.deltaTime;

            // 模拟空气阻力
            velocity *= (1f - airDrag * Time.deltaTime);

            // 更新角色位置
            playerRb.transform.position += velocity * Time.deltaTime;

            // 检测是否接触地面（停止飞行）
            if (playerRb.transform.position.y <= 4f) 
            {
                isFlying = false;
                playerRb.isKinematic = false; // 重新启用物理引擎
            }
        }
    }
}
