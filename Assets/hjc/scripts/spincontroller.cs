using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spincontroller : MonoBehaviour
{
    Animator spinAnim;

    // Start is called before the first frame update
    void Start()
    {
        spinAnim = GetComponent<Animator>();
    }

    // 当玩家进入碰撞区域时触发旋转动画
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 检查是否是玩家
        {
            spinAnim.SetTrigger("activateSpin"); // 触发旋转动画
        }
    }

    // 当玩家离开碰撞区域时触发回转动画
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 检查是否是玩家
        {
            spinAnim.SetTrigger("activateSpin"); // 触发回转动画（使用相同的触发器）
        }
    }
}