using UnityEngine;

public class SpringPad : MonoBehaviour
{
    public float springForce = 10f; // ���ϵĵ���
    public float forwardForce = 5f; // ��ǰ�ĵ���
    public Transform launchDirection; // ���䷽��
    public float gravity = -9.8f; // �ֶ�ģ����������ٶ�
    public float airDrag = 0.1f; // ��������

    private bool isFlying = false; // �ж��Ƿ��ڿ���
    private Vector3 velocity; // ��ǰ�ٶ�
    private Rigidbody playerRb;

    private void OnTriggerEnter(Collider other)
    {
        // ����Ƿ��ǽ�ɫ���봥������
        if (other.CompareTag("Player"))
        {
            // ��ȡ��ɫ�� Rigidbody
            playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // ����ٶ�
                velocity = Vector3.zero;

                // ���㵯���ʼ�ٶ�
                Vector3 launchForce = launchDirection.up * springForce + launchDirection.forward * forwardForce;
                velocity = launchForce;

                // ���� Rigidbody �������������
                playerRb.isKinematic = true;

                // ���Ϊ����״̬
                isFlying = true;
            }
        }
    }

    private void Update()
    {
        if (isFlying)
        {
            // ģ������
            velocity.y += gravity * Time.deltaTime;

            // ģ���������
            velocity *= (1f - airDrag * Time.deltaTime);

            // ���½�ɫλ��
            playerRb.transform.position += velocity * Time.deltaTime;

            // ����Ƿ�Ӵ����棨ֹͣ���У�
            if (playerRb.transform.position.y <= 4f) 
            {
                isFlying = false;
                playerRb.isKinematic = false; // ����������������
            }
        }
    }
}
