using UnityEngine;

public class MoveObjectZAxis : MonoBehaviour
{
    // 移动的速度
    public float speed = 2.0f;

    // Z轴移动的范围
    public float zRange = 5.0f;

    // 初始位置
    private Vector3 startPosition;

    void Start()
    {
        // 记录物体初始位置
        startPosition = transform.position;
    }

    void Update()
    {
        // 计算Z轴上的偏移量，使用正弦函数实现平滑来回移动
        float zOffset = Mathf.Sin(Time.time * speed) * zRange;

        // 更新物体的位置
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + zOffset);
    }
}
