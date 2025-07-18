
using UnityEngine;

namespace Platformer
{
    /// <summary>
    /// Handles character death animation when health drops to 0.
    /// </summary>
    public class CharacterDeathAnimation : MonoBehaviour
    {
        [Tooltip("Reference to the Animator component.")]
        public Animator animator;

        [Tooltip("Reference to the CharacterHealth component.")]
        public CharacterHealth characterHealth;

        private void Update()
        {
            // Check if health is less than or equal to 0
            if (characterHealth.Health <= 0)
            {
                // Output a debug message
                Debug.Log("Character has died! Health is now 0 or below.");

                // Set the "dead" parameter in the Animator to true
                animator.SetBool("dead", true);

                // Optionally disable other components (e.g., CharacterController, Collider)
                DisableComponents();
            }
        }

        /// <summary>
        /// Disables additional components when the character dies.
        /// </summary>
        private void DisableComponents()
        {
            // Disable CharacterController if it exists
            if (TryGetComponent<CharacterController>(out var controller))
            {
                controller.enabled = false;
            }

            // Disable Collider if it exists
            if (TryGetComponent<Collider>(out var collider))
            {
                collider.enabled = false;
            }
        }
    }
}