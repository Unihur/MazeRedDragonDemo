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

    // 当 Player 进入碰撞区域时
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

    // 当 Player 离开碰撞区域时
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 如果门是开启的，我们关闭它
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
