using UnityEngine;

public class Pendulum : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Pendulum Settings")]
    public float swingForce = 10f; // �ڶ����ƶ���

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
        float torque = -Mathf.Sin(angle * Mathf.Deg2Rad) * swingForce; // �������ش�С
        rb.AddTorque(Vector3.forward * torque); // ʩ��Ť��
    }
}
