using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playrope : MonoBehaviour
{
    private Rigidbody playerRigidbody; // ��Ҹ���
    private FixedJoint ropeJoint;      // ��������������ӵ�
    private bool isAttachedToRope = false; // ����Ƿ���ץס����

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ���ո����������
        if (isAttachedToRope && Input.GetKeyDown(KeyCode.Space))
        {
            DetachFromRope();
        }
    }

    // ����ҽӴ�����ʱ
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("rape")) // ȷ���������� "Rope" ��ǩ
        {
            AttachToRope(collision.rigidbody); // �󶨵�����
        }
    }

    // �󶨵�����
    private void AttachToRope(Rigidbody ropeRigidbody)
    {
        if (ropeJoint == null) // �����ظ��������
        {
            ropeJoint = gameObject.AddComponent<FixedJoint>();
            ropeJoint.connectedBody = ropeRigidbody; // �󶨵������ĸ���
            isAttachedToRope = true;

            Debug.Log("Player attached to rope!");
        }
    }

    // ����������
    private void DetachFromRope()
    {
        if (ropeJoint != null)
        {
            Destroy(ropeJoint); // �Ƴ��̶��ؽ�
            isAttachedToRope = false;

            // ���һ������������ѡ��
            Vector3 separationForce = new Vector3(0, 5f, 0); // ���ϵ���
            playerRigidbody.AddForce(separationForce, ForceMode.Impulse);

            Debug.Log("Player detached from rope!");
        }
    }
}