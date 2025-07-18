using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer; // 添加对Platformer命名空间的引用

public class sw2 : MonoBehaviour
{
    public GameObject rollingPin; // 在Inspector中指定包含RollingPin的GameObject
    public GameObject sword; // 在Inspector中指定包含Sword的GameObject

    private Weapon rollingPinWeapon; // 用于保存rolling pin的Weapon脚本引用
    private Weapon swordWeapon; // 用于保存sword的Weapon脚本引用

    void Start()
    {
        // 确保开始时只显示rolling pin，不显示sword
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

        // 设置初始伤害值
        SetWeaponDamage(rollingPinWeapon, 40f);
    }

    void Update()
    {
        // 检测按键输入来切换武器和设置伤害
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 按下1键
        {
            ShowRollingPinAndHideSword();
            SetWeaponDamage(rollingPinWeapon, 40f);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) // 按下2键
        {
            ShowSwordAndHideRollingPin();
            SetWeaponDamage(rollingPinWeapon, 200f);
        }
    }

    private void ShowRollingPinAndHideSword()
    {
        // 显示rolling pin，隐藏sword
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
        // 显示sword，隐藏rolling pin的MeshRenderer
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
            renderer.enabled = enable; // 根据参数启用或禁用所有子物体的MeshRenderer
        }
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