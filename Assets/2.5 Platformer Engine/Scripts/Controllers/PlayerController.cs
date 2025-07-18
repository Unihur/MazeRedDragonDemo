using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Manages a character. Allows keyboard input.
    /// </summary>
    [RequireComponent(typeof(CharacterMotor))]
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// Object to activate and deactivate depending if there is an action to perform by the motor.
        /// </summary>
        [Tooltip("Object to activate and deactivate depending if there is an action to perform by the motor.")]
        public GameObject ActionUI;

        internal bool HasMovement;
        internal Vector2 Movement;

        private void Update()
        {
            Characters.MainPlayer = Characters.Get(gameObject);

            var motor = GetComponent<CharacterMotor>();
            if (motor == null)
                return;

            if (ActionUI != null)
                if (ActionUI.activeSelf != (motor.Action != null))
                    ActionUI.SetActive(motor.Action != null);

            if (motor.IsFalling)
                motor.StandUp();
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1) && !motor.IsHangingOnEdge || Input.GetKey(KeyCode.J))
                    motor.InputAttack();

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || (HasMovement && Movement.y > 0.05f))
                    motor.InputClimb(1);

                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || (HasMovement && Movement.y < -0.05f))
                    motor.InputClimb(-1);

                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || (HasMovement && Movement.x < -0.05f))
                {
                    motor.InputMovement(-1);

                    if (!motor.IsHangingOnRope)
                        if (motor.IsOnWalkableSurface || !motor.IsGrounded)
                            motor.Direction = CharacterDirection.Left;
                }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || (HasMovement && Movement.x > 0.05f))
                {
                    motor.InputMovement(1);

                    if (!motor.IsHangingOnRope)
                        if (motor.IsOnWalkableSurface || !motor.IsGrounded)
                            motor.Direction = CharacterDirection.Right;
                }

                if (Input.GetKeyDown(KeyCode.Space))
                    motor.InputJump();
            }
        }
    }
}
