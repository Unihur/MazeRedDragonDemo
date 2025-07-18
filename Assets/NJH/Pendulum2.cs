using UnityEngine;

public class Pendulum2 : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Pendulum Settings")]
    public float swingForce = 10f; // 摆动的推动力
    public bool reverseDirection = false; // 是否反向摆动

    private void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody>();

        // 初始化施加推动力
        AddSwingForce();
    }

    private void FixedUpdate()
    {
        // 保持周期性运动
        if (rb != null)
        {
            AddSwingForce();
        }
    }

    private void AddSwingForce()
    {
        // 根据摆锤角度施加周期性推动力
        float angle = Vector3.SignedAngle(transform.up, Vector3.down, transform.forward); // 当前与竖直方向的夹角
        float torque = Mathf.Sin(angle * Mathf.Deg2Rad) * swingForce; // 计算力矩大小

        // 如果需要反向摆动，改变力矩符号
        if (reverseDirection)
        {
            torque = -torque;
        }

        // 施加扭矩
        rb.AddTorque(Vector3.forward * torque);
    }
}
