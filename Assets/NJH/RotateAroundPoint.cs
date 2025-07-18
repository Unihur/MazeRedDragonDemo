using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public Vector3 pivotPoint = Vector3.zero; // 旋转中心点（相对于物体的本地坐标）
    public Vector3 rotationAxis = Vector3.up; // 旋转轴（默认Y轴）
    public float rotationSpeed = 50f;         // 旋转速度

    void Update()
    {
        // 计算世界空间下的旋转中心
        Vector3 worldPivot = transform.TransformPoint(pivotPoint);

        // 围绕旋转中心点旋转
        transform.RotateAround(worldPivot, rotationAxis, -rotationSpeed * Time.deltaTime);
    }

    // Gizmos 可视化：显示旋转中心
    private void OnDrawGizmos()
    {
        // 将旋转中心点显示为一个红色小球
        Gizmos.color = Color.red;
        Vector3 worldPivot = transform.TransformPoint(pivotPoint);
        Gizmos.DrawSphere(worldPivot, 0.1f);
    }
}
