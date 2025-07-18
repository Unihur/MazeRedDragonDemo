using UnityEngine;
using System.Collections.Generic;

namespace Platformer
{
    /// <summary>
    /// 表示一个尖刺，会对进入触发范围的角色造成伤害。
    /// </summary>
    public class Spike : MonoBehaviour
    {
        /// <summary>
        /// 每秒对角色造成的伤害。
        /// </summary>
        [Tooltip("每秒对角色造成的伤害")]
        public float DamagePerSecond = 18f;

        /// <summary>
        /// 每次伤害之间的时间间隔（秒）。
        /// </summary>
        [Tooltip("每次伤害之间的时间间隔（秒）")]
        public float DamageInterval = 1f;

        // 用于记录每个角色的计时器
        private readonly Dictionary<GameObject, float> _damageTimers = new Dictionary<GameObject, float>();

        /// <summary>
        /// 当有物体进入触发器范围时调用。
        /// </summary>
        /// <param name="other">触发的碰撞体</param>
        private void OnTriggerEnter(Collider other)
        {
            // 检查是否是带有 CharacterHealth 的物体
            var health = other.GetComponent<CharacterHealth>();
            if (health != null && health.Health > 0)
            {
                // 如果角色第一次进入尖刺范围，初始化计时器
                if (!_damageTimers.ContainsKey(other.gameObject))
                {
                    _damageTimers[other.gameObject] = 0f; // 初始化计时器
                }
            }
        }

        /// <summary>
        /// 当有物体持续处于触发器范围内时调用。
        /// </summary>
        /// <param name="other">触发的碰撞体</param>
        private void OnTriggerStay(Collider other)
        {
            // 检查进入触发器的物体是否有 CharacterHealth 脚本
            var health = other.GetComponent<CharacterHealth>();
            if (health != null && health.Health > 0)
            {
                // 获取当前时间
                float currentTime = Time.time;

                // 检查是否已记录该物体
                if (_damageTimers.ContainsKey(other.gameObject))
                {
                    // 如果距离上次伤害的时间超过 DamageInterval，则造成伤害
                    if (currentTime - _damageTimers[other.gameObject] >= DamageInterval)
                    {
                        health.Deal(DamagePerSecond); // 对角色造成伤害
                        _damageTimers[other.gameObject] = currentTime; // 更新最近一次伤害时间
                    }
                }
            }
        }



    }
}
