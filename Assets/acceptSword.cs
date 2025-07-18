using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer; // ��Ӷ�Platformer�����ռ������

public class acceptSword : MonoBehaviour
{
    public GameObject keyInfo; // ��Inspector��ָ������KeyInfo�ı���GameObject
    public GameObject rollingPin; // ��Inspector��ָ������RollingPin��GameObject
    public GameObject sword; // ��������Inspector��ָ������Sword��GameObject
    private bool isTouchingPlayer = false;

    private Weapon rollingPinWeapon; // ���ڱ���rolling pin��Weapon�ű�����
    private Weapon swordWeapon; // ���ڱ���sword��Weapon�ű�����
    private WeaponSwitcher weaponSwitcher; // ���������ڱ���WeaponSwitcher�ű�����

    void Start()
    {
        // ���KeyInfo��Sword�ڿ�ʼʱӦ�ò��ɼ�����������������Ϊfalse
        if (keyInfo != null)
        {
            keyInfo.SetActive(false);
        }
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

        // �ҵ������е�WeaponSwitcherʵ��
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        if (weaponSwitcher == null)
        {
            Debug.LogError("WeaponSwitcher not found in the scene.");
        }
        else
        {
            weaponSwitcher.AllowWeaponSwitching(false); // ��ʼ״̬�²������л�����
        }
    }

    void Update()
    {
        // ����Ƿ�������Ҳ��Ұ�����E��
        if (isTouchingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            HideRollingPinShowSwordAndHideSelf();
            weaponSwitcher.AllowWeaponSwitching(true); // �����л�����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // ���������һ����Ϊ"Player"��tag
        if (other.CompareTag("Player"))
        {
            ShowKeyInfo();
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ������뿪��������Χʱ����KeyInfo
        if (other.CompareTag("Player"))
        {
            HideKeyInfo();
            isTouchingPlayer = false;
        }
    }

    private void ShowKeyInfo()
    {
        if (keyInfo != null)
        {
            keyInfo.SetActive(true);
        }
    }

    private void HideKeyInfo()
    {
        if (keyInfo != null)
        {
            keyInfo.SetActive(false);
        }
    }

    private void HideRollingPinShowSwordAndHideSelf()
    {
        // ����rolling pin��MeshRenderer����ʾ�������������Լ�����KeyInfo
        if (rollingPin != null)
        {
            MeshRenderer[] meshRenderers = rollingPin.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false; // ���������������MeshRenderer
            }
        }
        if (sword != null)
        {
            sword.SetActive(true); // ��ʾ��

            // ���ý����˺�ֵ������200��
            SetWeaponDamage(rollingPinWeapon, 200f);
        }
        HideKeyInfo();

        // ���ؽű����صı���
        gameObject.SetActive(false);
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