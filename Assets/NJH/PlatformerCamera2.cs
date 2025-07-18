using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Camera))]
    public class TopDownCamera : MonoBehaviour
    {
        /// <summary>
        /// ���Ҫ�����Ŀ�����
        /// </summary>
        [Tooltip("���Ҫ�����Ŀ�����")]
        public GameObject Target;

        /// <summary>
        /// ��������Ŀ���ƫ������
        /// </summary>
        [Tooltip("��������Ŀ���ƫ������")]
        public Vector3 TargetOffset = new Vector3(0, 10, -10);

        /// <summary>
        /// ����Ŀ��ľ��롣
        /// </summary>
        [Tooltip("����Ŀ��ľ��롣")]
        public float Distance = 10;

        /// <summary>
        /// �����ٶȡ�
        /// </summary>
        [Tooltip("�������Ŀ����ٶȡ�")]
        public float FollowSpeed = 5.0f;

        /// <summary>
        /// ���ӽǶȡ�
        /// </summary>
        [Tooltip("���ӵĹ̶��Ƕȡ�")]
        public float PitchAngle = 60.0f;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        /// <summary>
        /// ִ����������߼���
        /// </summary>
        private void LateUpdate()
        {
            if (Target == null)
                return;

            // ����Ŀ��λ�ã�����ƫ����
            Vector3 targetPosition = Target.transform.position + TargetOffset;

            // ƽ���ƶ����
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * FollowSpeed);

            // ��������ĸ��ӽǶ�
            transform.rotation = Quaternion.Euler(PitchAngle, 0, 0);

            // ʹ���ʼ�ճ����ɫ
            transform.LookAt(Target.transform.position);
        }
    }
}
