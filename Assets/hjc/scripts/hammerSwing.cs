using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerSwing : MonoBehaviour
{
    // 摆动的最大角度
    public float maxAngle = 60f;
    // 摆动的速度
    public float swingSpeed = 1f;

    // 旋转角度的当前值
    private float currentAngle;

    void Update()
    {
        // 计算周期性的角度，使用 Mathf.Sin 生成一个从 -1 到 1 的周期性值
        currentAngle = Mathf.Sin(Time.time * swingSpeed) * maxAngle;

        // 将物体绕 Z 轴进行旋转，限制在 XZ 平面内
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}