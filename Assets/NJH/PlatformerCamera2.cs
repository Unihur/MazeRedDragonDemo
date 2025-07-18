using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(Camera))]
    public class TopDownCamera : MonoBehaviour
    {
        /// <summary>
        /// 相机要跟随的目标对象。
        /// </summary>
        [Tooltip("相机要跟随的目标对象。")]
        public GameObject Target;

        /// <summary>
        /// 相机相对于目标的偏移量。
        /// </summary>
        [Tooltip("相机相对于目标的偏移量。")]
        public Vector3 TargetOffset = new Vector3(0, 10, -10);

        /// <summary>
        /// 距离目标的距离。
        /// </summary>
        [Tooltip("距离目标的距离。")]
        public float Distance = 10;

        /// <summary>
        /// 跟随速度。
        /// </summary>
        [Tooltip("相机跟随目标的速度。")]
        public float FollowSpeed = 5.0f;

        /// <summary>
        /// 俯视角度。
        /// </summary>
        [Tooltip("俯视的固定角度。")]
        public float PitchAngle = 60.0f;

        private Camera _camera;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
        }

        /// <summary>
        /// 执行相机跟随逻辑。
        /// </summary>
        private void LateUpdate()
        {
            if (Target == null)
                return;

            // 设置目标位置，增加偏移量
            Vector3 targetPosition = Target.transform.position + TargetOffset;

            // 平滑移动相机
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * FollowSpeed);

            // 设置相机的俯视角度
            transform.rotation = Quaternion.Euler(PitchAngle, 0, 0);

            // 使相机始终朝向角色
            transform.LookAt(Target.transform.position);
        }
    }
}
