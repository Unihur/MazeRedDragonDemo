using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorcontroller : MonoBehaviour
{
    public bool resetble;
    public GameObject door;
    public GameObject Valve;
    public bool startOpen;

    bool firstTrigger = false;
    bool open = true;
    Animator doorAnim;
    Animator ValveAnim;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        ValveAnim = Valve.GetComponent<Animator>();
        if (!startOpen)
        {
            open = false;
            doorAnim.SetTrigger("doorTrigger");
        }
    }

    // �� Player ������ײ����ʱ
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !firstTrigger)
        {
            if (!resetble) firstTrigger = true;
            doorAnim.SetTrigger("doorTrigger");
            open = !open;
            ValveAnim.SetTrigger("valveRoatating");
        }
    }

    // �� Player �뿪��ײ����ʱ
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ������ǿ����ģ����ǹر���
            if (open)
            {
                doorAnim.SetTrigger("doorTrigger");
                open = false;
                ValveAnim.SetTrigger("valveRoatating");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
