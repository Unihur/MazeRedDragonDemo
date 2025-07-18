using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer; // ��Ӷ�Platformer�����ռ������

public class sw2 : MonoBehaviour
{
    public GameObject rollingPin; // ��Inspector��ָ������RollingPin��GameObject
    public GameObject sword; // ��Inspector��ָ������Sword��GameObject

    private Weapon rollingPinWeapon; // ���ڱ���rolling pin��Weapon�ű�����
    private Weapon swordWeapon; // ���ڱ���sword��Weapon�ű�����

    void Start()
    {
        // ȷ����ʼʱֻ��ʾrolling pin������ʾsword
        if (sword != null)
        {
            sword.SetActive(false); // ���迪ʼʱ���ǲ��ɼ���
        }
        if (rollingPin != null)
        {
            rollingPin.SetActive(true); // ���迪ʼʱrolling pin�ǿɼ���
        }

        // ��ȡ�����ϵ�Weapon�ű�����
        rollingPinWeapon = rollingPin.GetComponent<Weapon>();
        swordWeapon = sword.GetComponent<Weapon>();

        // ���ó�ʼ�˺�ֵ
        SetWeaponDamage(rollingPinWeapon, 40f);
    }

    void Update()
    {
        // ��ⰴ���������л������������˺�
        if (Input.GetKeyDown(KeyCode.Alpha1)) // ����1��
        {
            ShowRollingPinAndHideSword();
            SetWeaponDamage(rollingPinWeapon, 40f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // ����2��
        {
            ShowSwordAndHideRollingPin();
            SetWeaponDamage(rollingPinWeapon, 200f);
        }
    }

    private void ShowRollingPinAndHideSword()
    {
        // ��ʾrolling pin������sword
        if (rollingPin != null)
        {
            rollingPin.SetActive(true);
            EnableMeshRenderers(rollingPin, true);
        }
        if (sword != null)
        {
            sword.SetActive(false);
        }
    }

    private void ShowSwordAndHideRollingPin()
    {
        // ��ʾsword������rolling pin��MeshRenderer
        if (sword != null)
        {
            sword.SetActive(true);
        }
        if (rollingPin != null)
        {
            EnableMeshRenderers(rollingPin, false);
        }
    }

    private void EnableMeshRenderers(GameObject obj, bool enable)
    {
        MeshRenderer[] meshRenderers = obj.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer renderer in meshRenderers)
        {
            renderer.enabled = enable; // ���ݲ������û���������������MeshRenderer
        }
    }

    private void SetWeaponDamage(Weapon weapon, float damage)
    {
        if (weapon != null)
        {
            // ����Ĭ�Ϲ������˺�ֵ
            Attack newDefaultAttack = weapon.Attack;
            newDefaultAttack.Damage = damage;
            weapon.Attack = newDefaultAttack;

            // �����������Ҳ����������ÿ���������˺�ֵ
            if (weapon.Combo != null && weapon.Combo.Length > 0)
            {
                for (int i = 0; i < weapon.Combo.Length; i++)
                {
                    Attack comboAttack = weapon.Combo[i];
                    comboAttack.Damage = damage;
                    weapon.Combo[i] = comboAttack;

                    // ��ѡ�������������ȷ���˺�ֵ�Ѹ���
                    Debug.Log($"Set combo attack {i} damage for {weapon.name} to: {comboAttack.Damage}");
                }
            }

            // ��ӵ�����Ϣ����ȷ��Ĭ�Ϲ������˺�ֵ�Ѹ���
            Debug.Log($"Set default attack damage for {weapon.name} to: {newDefaultAttack.Damage}");
        }
    }
}