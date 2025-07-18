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

    // ����ҽ�����ײ����ʱ������ת����
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // ����Ƿ������
        {
            spinAnim.SetTrigger("activateSpin"); // ������ת����
        }
    }

    // ������뿪��ײ����ʱ������ת����
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // ����Ƿ������
        {
            spinAnim.SetTrigger("activateSpin"); // ������ת������ʹ����ͬ�Ĵ�������
        }
    }
}