using UnityEngine;

public class RotateAndMove : MonoBehaviour
{
    public float rotationSpeed = 100f; // 转动速度，单位：度/秒
    public Vector3 rotationAxis = Vector3.right; // 旋转轴

    public float moveDistance = 5f; // 左右移动的距离
    public float moveSpeed = 2f; // 移动的速度

    private Vector3 startPosition; // 物体的初始位置

    void Start()
    {
        // 记录初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 按指定轴和速度旋转
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);

        // 左右来回移动
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = startPosition + new Vector3(0, 0, offset); 
    }
}
