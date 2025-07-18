using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float jumpHeight = 5f; // �����ĸ߶�

    private void OnTriggerEnter(Collider other)
    {
        // �����봥�����Ķ����Ƿ������
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // ֱ��������ҵĴ�ֱ�ٶ�
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, CalculateJumpVelocity(jumpHeight), playerRigidbody.velocity.z);
            }
        }
    }

    // ����ﵽָ���߶�������ٶ�
    private float CalculateJumpVelocity(float height)
    {
        // ʹ�ù�ʽ v = sqrt(2 * g * h)������ g ���������ٶȣ�����Ϊ 9.81��
        return Mathf.Sqrt(2f * 9.81f * height);
    }
}