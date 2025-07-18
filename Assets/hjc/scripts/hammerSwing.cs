using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerSwing : MonoBehaviour
{
    // �ڶ������Ƕ�
    public float maxAngle = 60f;
    // �ڶ����ٶ�
    public float swingSpeed = 1f;

    // ��ת�Ƕȵĵ�ǰֵ
    private float currentAngle;

    void Update()
    {
        // ���������ԵĽǶȣ�ʹ�� Mathf.Sin ����һ���� -1 �� 1 ��������ֵ
        currentAngle = Mathf.Sin(Time.time * swingSpeed) * maxAngle;

        // �������� Z �������ת�������� XZ ƽ����
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}