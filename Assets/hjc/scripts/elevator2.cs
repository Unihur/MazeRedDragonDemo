using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator2 : MonoBehaviour
{
    public float moveDistance = 5.0f; // 电梯上升的距离
    public float moveSpeed = 2.0f;    // 电梯移动速度
    public float waitTime = 2.0f;     // 上升后等待的时间

    private Vector3 startPosition;    // 电梯的初始位置
    private Vector3 targetPosition;   // 电梯的目标位置
    private bool isMovingUp = false;  // 是否正在上升
    private bool isMovingDown = false; // 是否正在下降
    private float waitTimer = 0.0f;   // 等待计时器

    void Start()
    {
        // 记录电梯的初始位置
        startPosition = transform.position;
        // 计算目标位置
        targetPosition = startPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (isMovingUp)
        {
            // 电梯上升
            MoveElevator(targetPosition);

            // 如果到达目标位置，开始等待
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMovingUp = false;
                waitTimer = waitTime;
            }
        }
        else if (waitTimer > 0)
        {
            // 等待时间
            waitTimer -= Time.deltaTime;

            // 如果等待时间结束，开始下降
            if (waitTimer <= 0)
            {
                isMovingDown = true;
            }
        }
        else if (isMovingDown)
        {
            // 电梯下降
            MoveElevator(startPosition);

            // 如果回到初始位置，停止下降
            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                isMovingDown = false;
            }
        }
    }

    // 移动电梯
    void MoveElevator(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    // 当玩家进入触发区域时调用
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMovingUp && !isMovingDown)
        {
            isMovingUp = true; // 开始上升
        }
    }
}