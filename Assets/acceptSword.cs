using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer; // 添加对Platformer命名空间的引用

public class acceptSword : MonoBehaviour
{
    public GameObject keyInfo; // 在Inspector中指定包含KeyInfo文本的GameObject
    public GameObject rollingPin; // 在Inspector中指定包含RollingPin的GameObject
    public GameObject sword; // 新增：在Inspector中指定包含Sword的GameObject
    private bool isTouchingPlayer = false;

    private Weapon rollingPinWeapon; // 用于保存rolling pin的Weapon脚本引用
    private Weapon swordWeapon; // 用于保存sword的Weapon脚本引用
    private WeaponSwitcher weaponSwitcher; // 新增：用于保存WeaponSwitcher脚本引用

    void Start()
    {
        // 如果KeyInfo和Sword在开始时应该不可见，可以在这里设置为false
        if (keyInfo != null)
        {
            keyInfo.SetActive(false);
        }
        if (sword != null)
        {
            sword.SetActive(false); // 假设开始时剑是不可见的
        }
        if (rollingPin != null)
        {
            rollingPin.SetActive(true); // 假设开始时rolling pin是可见的
        }

        // 获取武器上的Weapon脚本引用
        rollingPinWeapon = rollingPin.GetComponent<Weapon>();
        swordWeapon = sword.GetComponent<Weapon>();

        // 找到场景中的WeaponSwitcher实例
        weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        if (weaponSwitcher == null)
        {
            Debug.LogError("WeaponSwitcher not found in the scene.");
        }
        else
        {
            weaponSwitcher.AllowWeaponSwitching(false); // 初始状态下不允许切换武器
        }
    }

    void Update()
    {
        // 检查是否触碰了玩家并且按下了E键
        if (isTouchingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            HideRollingPinShowSwordAndHideSelf();
            weaponSwitcher.AllowWeaponSwitching(true); // 允许切换武器
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 假设玩家有一个名为"Player"的tag
        if (other.CompareTag("Player"))
        {
            ShowKeyInfo();
            isTouchingPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 当玩家离开触发器范围时隐藏KeyInfo
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
        // 隐藏rolling pin的MeshRenderer并显示剑，隐藏自身以及隐藏KeyInfo
        if (rollingPin != null)
        {
            MeshRenderer[] meshRenderers = rollingPin.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer renderer in meshRenderers)
            {
                renderer.enabled = false; // 禁用所有子物体的MeshRenderer
            }
        }
        if (sword != null)
        {
            sword.SetActive(true); // 显示剑

            // 设置剑的伤害值（例如200）
            SetWeaponDamage(rollingPinWeapon, 200f);
        }
        HideKeyInfo();

        // 隐藏脚本挂载的本体
        gameObject.SetActive(false);
    }

    private void SetWeaponDamage(Weapon weapon, float damage)
    {
        if (weapon != null)
        {
            // 更新默认攻击的伤害值
            Attack newDefaultAttack = weapon.Attack;
            newDefaultAttack.Damage = damage;
            weapon.Attack = newDefaultAttack;

            // 如果有连击，也更新连击中每个攻击的伤害值
            if (weapon.Combo != null && weapon.Combo.Length > 0)
            {
                for (int i = 0; i < weapon.Combo.Length; i++)
                {
                    Attack comboAttack = weapon.Combo[i];
                    comboAttack.Damage = damage;
                    weapon.Combo[i] = comboAttack;

                    // 可选：调试输出，以确认伤害值已更改
                    Debug.Log($"Set combo attack {i} damage for {weapon.name} to: {comboAttack.Damage}");
                }
            }

            // 添加调试信息，以确认默认攻击的伤害值已更改
            Debug.Log($"Set default attack damage for {weapon.name} to: {newDefaultAttack.Damage}");
        }
    }
}