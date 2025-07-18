using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator2 : MonoBehaviour
{
    public float moveDistance = 5.0f; // ���������ľ���
    public float moveSpeed = 2.0f;    // �����ƶ��ٶ�
    public float waitTime = 2.0f;     // ������ȴ���ʱ��

    private Vector3 startPosition;    // ���ݵĳ�ʼλ��
    private Vector3 targetPosition;   // ���ݵ�Ŀ��λ��
    private bool isMovingUp = false;  // �Ƿ���������
    private bool isMovingDown = false; // �Ƿ������½�
    private float waitTimer = 0.0f;   // �ȴ���ʱ��

    void Start()
    {
        // ��¼���ݵĳ�ʼλ��
        startPosition = transform.position;
        // ����Ŀ��λ��
        targetPosition = startPosition + Vector3.up * moveDistance;
    }

    void Update()
    {
        if (isMovingUp)
        {
            // ��������
            MoveElevator(targetPosition);

            // �������Ŀ��λ�ã���ʼ�ȴ�
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMovingUp = false;
                waitTimer = waitTime;
            }
        }
        else if (waitTimer > 0)
        {
            // �ȴ�ʱ��
            waitTimer -= Time.deltaTime;

            // ����ȴ�ʱ���������ʼ�½�
            if (waitTimer <= 0)
            {
                isMovingDown = true;
            }
        }
        else if (isMovingDown)
        {
            // �����½�
            MoveElevator(startPosition);

            // ����ص���ʼλ�ã�ֹͣ�½�
            if (Vector3.Distance(transform.position, startPosition) < 0.01f)
            {
                isMovingDown = false;
            }
        }
    }

    // �ƶ�����
    void MoveElevator(Vector3 target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }

    // ����ҽ��봥������ʱ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMovingUp && !isMovingDown)
        {
            isMovingUp = true; // ��ʼ����
        }
    }
}