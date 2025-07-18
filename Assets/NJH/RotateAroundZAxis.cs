using UnityEngine;

public class RotateAroundZAxis : MonoBehaviour
{
    public float rotationSpeed = 100f; // 转动速度，单位：度/秒

    void Update()
    {
        // 绕 Z 轴旋转
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
