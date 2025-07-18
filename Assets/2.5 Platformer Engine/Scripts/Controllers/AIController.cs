using UnityEngine;

namespace Platformer
{
    public enum AIState
    {
        Patrol,
        Attack
    }

    /// <summary>
    /// Manages a character using AI.
    /// </summary>
    [RequireComponent(typeof(CharacterMotor))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class AIController : MonoBehaviour
    {
        /// <summary>
        /// Leftmost position of patrol.
        /// </summary>
        public Vector3 LeftPoint
        {
            get
            {
                if (_motor == null)
                    return transform.position;

                var right = _motor.CameraRight;

                if (_hasCenter)
                    return _center - right * PatrolLeft;
                else
                    return transform.position - right * PatrolLeft;
            }
        }

        /// <summary>
        /// Rightmost position of patrol.
        /// </summary>
        public Vector3 RightPoint
        {
            get
            {
                if (_motor == null)
                    return transform.position;

                var right = _motor.CameraRight;

                if (_hasCenter)
                    return _center + right * PatrolRight;
                else
                    return transform.position + right * PatrolRight;
            }
        }

        /// <summary>
        /// AI can be either patrolling or attacking.
        /// </summary>
        [Tooltip("AI can be either patrolling or attacking.")]
        public AIState State;

        /// <summary>
        /// Target for the AI to attack.
        /// </summary>
        [Tooltip("Target for the AI to attack.")]
        public GameObject Target;

        /// <summary>
        /// Distance left of the starting position for patrol.
        /// </summary>
        [Tooltip("Distance left of the starting position for patrol.")]
        public float PatrolLeft = 1;

        /// <summary>
        /// Distance right of the starting position for patrol.
        /// </summary>
        [Tooltip("Distance right of the starting position for patrol.")]
        public float PatrolRight = 1;

        /// <summary>
        /// Minimum distance to maintain against an enemy.
        /// </summary>
        [Tooltip("Minimum distance to maintain against an enemy.")]
        public float MinEnemyDistance = 0.1f;

        /// <summary>
        /// Maximum distance at which the AI will attack. Measured
        /// </summary>
        [Tooltip("Maximum distance at which the AI will attack.")]
        public float EnemyAttackDistance = 0.3f;

        /// <summary>
        /// Duration between attack attempts.
        /// </summary>
        [Tooltip("Duration between attack attempts.")]
        public float AttackWait = 1;

        /// <summary>
        /// Should the AI stop its attack when hit by an enemy. Will start counting the time from zero if that's the case.
        /// </summary>
        [Tooltip("Should the AI stop its attack when hit by an enemy. Will start counting the time from zero if that's the case.")]
        public bool ResetAttackCounterOnHit = true;

        /// <summary>
        /// Should the AI attack when an enemy approaches.
        /// </summary>
        [Tooltip("Should the AI attack when an enemy approaches.")]
        public bool AutoNotice = true;

        /// <summary>
        /// Distance at which the AI will notice enemies.
        /// </summary>
        [Tooltip("Distance at which the AI will notice enemies.")]
        public float NoticeDistance = 2;

        /// <summary>
        /// Should the AI try to stand up when the character has fallen.
        /// </summary>
        [Tooltip("Should the AI try to stand up when the character has fallen.")]
        public bool AutoStandUp = false;

        /// <summary>
        /// Enemies further away than this distance will be forgotten.
        /// </summary>
        [Tooltip("Enemies further away than this distance will be forgotten.")]
        public float ForgetDistance = 15;

        private CharacterMotor _motor;
        private CapsuleCollider _capsule;

        private Vector3 _center;
        private bool _hasCenter;
        private int _direction = 1;
        private float _attack;

        private void Awake()
        {
            _motor = GetComponent<CharacterMotor>();
            _capsule = GetComponent<CapsuleCollider>();
        }

        /// <summary>
        /// Sets the AI to attack a target. If the value given is null it enters patrol mode.
        /// </summary>
        public void Attack(GameObject target)
        {
            if (target == null)
                CalmDown();
            else
            {
                State = AIState.Attack;
                Target = target;
            }
        }

        /// <summary>
        /// Sets the AI to enter patrol mode.
        /// </summary>
        public void CalmDown()
        {
            State = AIState.Patrol;
            Target = null;
        }

        /// <summary>
        /// Remembers the center position for patrolling.
        /// </summary>
        private void OnEnable()
        {
            _center = transform.position;
            _hasCenter = true;
        }

        /// <summary>
        /// Performs AI logic.
        /// </summary>
        private void Update()
        {
            if (_motor.IsFalling)
            {
                if (AutoStandUp)
                    _motor.StandUp();
                return;
            }

            CharacterMotor targetMotor = null;

            if (Target != null)
                targetMotor = Target.GetComponent<CharacterMotor>();

            var hasAFriendInBetween = false;
            var isFacingTheEnemy = false;

            if (targetMotor != null)
            {
                var enemyDot = Vector3.Dot(_motor.Forward, Target.transform.position - transform.position);
                isFacingTheEnemy = enemyDot > 0;

                if (isFacingTheEnemy)
                {
                    var me = Characters.Get(gameObject);

                    foreach (var character in Characters.All)
                        if (character.Object != me.Object)
                            if (character.IsSameSide(me))
                            {
                                var dot = Vector3.Dot(_motor.Forward, character.Object.transform.position - transform.position);

                                if (dot > 0 && dot < enemyDot)
                                {
                                    hasAFriendInBetween = true;
                                    break;
                                }
                            }
                }
            }

            if (_attack > float.Epsilon && targetMotor != null && targetMotor.IsAlive)
            {
                if (ResetAttackCounterOnHit && _motor.IsGettingHit)
                    _attack = AttackWait;

                _attack -= Time.deltaTime;

                if (_attack < float.Epsilon && isFacingTheEnemy && !hasAFriendInBetween)
                {
                    _motor.InputAttack();
                    _attack = AttackWait;
                }
            }
            else
                _attack = 0;

            var cameraForward = Quaternion.AngleAxis(_motor.Angle, Vector3.up) * Vector3.forward;
            var current = Vector3.Dot(cameraForward, transform.position);

            switch (State)
            {
                case AIState.Patrol:
                    if (AutoNotice)
                    {
                        var me = Characters.Get(gameObject);

                        foreach (var character in Characters.All)
                            if (character.Object != null && character.Object != gameObject && !character.IsSameSide(me))
                                if (Vector3.Distance(character.Object.transform.position, transform.position) < NoticeDistance)
                                    Attack(character.Object);
                    }

                    var right = Vector3.Dot(cameraForward, _center + cameraForward * PatrolRight);
                    var left = Vector3.Dot(cameraForward, _center - cameraForward * PatrolLeft);

                    if (_direction > 0)
                    {
                        _motor.Direction = CharacterDirection.Right;
                        
                        if (current < right)
                        {
                            if (!_motor.IsBlocked || !_motor.IsFacingWalkDirection)
                                _motor.InputMovement(1);
                            else
                                _motor.InputJump();
                        }
                        else
                            _direction = -1;
                    }
                    else
                    {
                        _motor.Direction = CharacterDirection.Left;

                        if (current > left)
                        {
                            if (!_motor.IsBlocked || !_motor.IsFacingWalkDirection)
                                _motor.InputMovement(-1);
                            else
                                _motor.InputJump();
                        }
                        else
                            _direction = 1;
                    }
                    break;

                case AIState.Attack:
                    if (Target == null)
                        State = AIState.Patrol;
                    else
                    {
                        var enemyAttackDistance = EnemyAttackDistance;
                        var minEnemyDistance = MinEnemyDistance;

                        var radius = transform.TransformVector(Vector3.forward * _capsule.radius).magnitude;
                        enemyAttackDistance += radius;
                        minEnemyDistance += radius;

                        var enemyCapsule = Target.GetComponent<CapsuleCollider>();
                        if (enemyCapsule != null)
                        {
                            var enemyRadius = Target.transform.TransformVector(Vector3.forward * enemyCapsule.radius).magnitude;
                            enemyAttackDistance += enemyRadius;
                            minEnemyDistance += enemyRadius;
                        }

                        if (Vector3.Distance(transform.position, Target.transform.position) < enemyAttackDistance)
                            if (_attack < float.Epsilon)
                                _attack = AttackWait;

                        var enemy = Vector3.Dot(cameraForward, Target.transform.position);
                        var enemyDistance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                                                             new Vector2(Target.transform.position.x, Target.transform.position.z));

                        if (enemyDistance > ForgetDistance)
                        {
                            Target = null;
                            State = AIState.Patrol;
                        }
                        else
                        {
                            if (enemyDistance > radius)
                            {
                                if (current < enemy)
                                    _motor.Direction = CharacterDirection.Right;
                                else
                                    _motor.Direction = CharacterDirection.Left;
                            }

                            if (enemyDistance > minEnemyDistance && !hasAFriendInBetween)
                            {
                                if (current < enemy + float.Epsilon)
                                {
                                    if (!_motor.IsBlocked || !_motor.IsFacingWalkDirection)
                                        _motor.InputMovement(1);
                                    else if (_motor.Obstacle != Target)
                                        _motor.InputJump();
                                }
                                else if (current > enemy - float.Epsilon)
                                {
                                    if (!_motor.IsBlocked || !_motor.IsFacingWalkDirection)
                                        _motor.InputMovement(-1);
                                    else if (_motor.Obstacle != Target)
                                        _motor.InputJump();
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }
}
