using UnityEngine;
using System.Collections.Generic;

namespace Platformer
{
    /// <summary>
    /// ��ʾһ����̣���Խ��봥����Χ�Ľ�ɫ����˺���
    /// </summary>
    public class Spike : MonoBehaviour
    {
        /// <summary>
        /// ÿ��Խ�ɫ��ɵ��˺���
        /// </summary>
        [Tooltip("ÿ��Խ�ɫ��ɵ��˺�")]
        public float DamagePerSecond = 18f;

        /// <summary>
        /// ÿ���˺�֮���ʱ�������룩��
        /// </summary>
        [Tooltip("ÿ���˺�֮���ʱ�������룩")]
        public float DamageInterval = 1f;

        // ���ڼ�¼ÿ����ɫ�ļ�ʱ��
        private readonly Dictionary<GameObject, float> _damageTimers = new Dictionary<GameObject, float>();

        /// <summary>
        /// ����������봥������Χʱ���á�
        /// </summary>
        /// <param name="other">��������ײ��</param>
        private void OnTriggerEnter(Collider other)
        {
            // ����Ƿ��Ǵ��� CharacterHealth ������
            var health = other.GetComponent<CharacterHealth>();
            if (health != null && health.Health > 0)
            {
                // �����ɫ��һ�ν����̷�Χ����ʼ����ʱ��
                if (!_damageTimers.ContainsKey(other.gameObject))
                {
                    _damageTimers[other.gameObject] = 0f; // ��ʼ����ʱ��
                }
            }
        }

        /// <summary>
        /// ��������������ڴ�������Χ��ʱ���á�
        /// </summary>
        /// <param name="other">��������ײ��</param>
        private void OnTriggerStay(Collider other)
        {
            // �����봥�����������Ƿ��� CharacterHealth �ű�
            var health = other.GetComponent<CharacterHealth>();
            if (health != null && health.Health > 0)
            {
                // ��ȡ��ǰʱ��
                float currentTime = Time.time;

                // ����Ƿ��Ѽ�¼������
                if (_damageTimers.ContainsKey(other.gameObject))
                {
                    // ��������ϴ��˺���ʱ�䳬�� DamageInterval��������˺�
                    if (currentTime - _damageTimers[other.gameObject] >= DamageInterval)
                    {
                        health.Deal(DamagePerSecond); // �Խ�ɫ����˺�
                        _damageTimers[other.gameObject] = currentTime; // �������һ���˺�ʱ��
                    }
                }
            }
        }



    }
}
