using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playrope : MonoBehaviour
{
    private Rigidbody playerRigidbody; // 玩家刚体
    private FixedJoint ropeJoint;      // 玩家与绳索的连接点
    private bool isAttachedToRope = false; // 玩家是否已抓住绳索

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // 按空格键分离绳索
        if (isAttachedToRope && Input.GetKeyDown(KeyCode.Space))
        {
            DetachFromRope();
        }
    }

    // 当玩家接触绳索时
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("rape")) // 确保绳索带有 "Rope" 标签
        {
            AttachToRope(collision.rigidbody); // 绑定到绳索
        }
    }

    // 绑定到绳索
    private void AttachToRope(Rigidbody ropeRigidbody)
    {
        if (ropeJoint == null) // 避免重复添加连接
        {
            ropeJoint = gameObject.AddComponent<FixedJoint>();
            ropeJoint.connectedBody = ropeRigidbody; // 绑定到绳索的刚体
            isAttachedToRope = true;

            Debug.Log("Player attached to rope!");
        }
    }

    // 从绳索分离
    private void DetachFromRope()
    {
        if (ropeJoint != null)
        {
            Destroy(ropeJoint); // 移除固定关节
            isAttachedToRope = false;

            // 添加一个反弹力（可选）
            Vector3 separationForce = new Vector3(0, 5f, 0); // 向上的力
            playerRigidbody.AddForce(separationForce, ForceMode.Impulse);

            Debug.Log("Player detached from rope!");
        }
    }
}