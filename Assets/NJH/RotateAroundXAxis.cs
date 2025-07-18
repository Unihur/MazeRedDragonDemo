using UnityEngine;

public class RotateAroundXAxis : MonoBehaviour
{
    public float rotationSpeed = 100f; // 转动速度，单位：度/秒

    void Update()
    {
        // 绕 X 轴旋转
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
