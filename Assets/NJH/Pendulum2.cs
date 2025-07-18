using UnityEngine;

public class Pendulum2 : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Pendulum Settings")]
    public float swingForce = 10f; // �ڶ����ƶ���
    public bool reverseDirection = false; // �Ƿ���ڶ�

    private void Start()
    {
        // ��ȡ�������
        rb = GetComponent<Rigidbody>();

        // ��ʼ��ʩ���ƶ���
        AddSwingForce();
    }

    private void FixedUpdate()
    {
        // �����������˶�
        if (rb != null)
        {
            AddSwingForce();
        }
    }

    private void AddSwingForce()
    {
        // ���ݰڴ��Ƕ�ʩ���������ƶ���
        float angle = Vector3.SignedAngle(transform.up, Vector3.down, transform.forward); // ��ǰ����ֱ����ļн�
        float torque = Mathf.Sin(angle * Mathf.Deg2Rad) * swingForce; // �������ش�С

        // �����Ҫ����ڶ����ı����ط���
        if (reverseDirection)
        {
            torque = -torque;
        }

        // ʩ��Ť��
        rb.AddTorque(Vector3.forward * torque);
    }
}
