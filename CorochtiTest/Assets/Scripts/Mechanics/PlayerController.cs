using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using Zenject;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        public Collider2D collider2d;
        public AudioSource audioSource;

        [Inject] private ConfigSystem _configSystem;
        private AnimationSystem _animationSystem;
        private AudioSystem _audioSystem;
        private GfxSystem _gfxSystem;
        bool jump;
        Vector2 move;
        private bool isSpaceBarPressed;
        private float speed;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        public void Init(AnimationSystem animationSystem, AudioSystem audioSystem, GfxSystem gfxSystem)
        {
            //audioSource = GetComponentInChildren<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            _animationSystem = animationSystem;
            _audioSystem = audioSystem;
            _gfxSystem = gfxSystem;
            speed = maxSpeed;
        }

        protected override void Update()
        {
            UpdateJumpState();
            base.Update();
        }

        protected override void FixedUpdate()
        {
            //if already falling, fall faster than the jump speed, otherwise use normal gravity.
            if (jumpState != JumpState.FlyWithBubble)
            {
                if (velocity.y < 0)
                {
                    velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
                }
                else
                {
                    velocity += Physics2D.gravity * Time.deltaTime;
                }
            }
            
            velocity.x = targetVelocity.x;

            IsGrounded = false;

            var deltaPosition = velocity * Time.deltaTime;

            var moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            var move = moveAlongGround * deltaPosition.x;

            PerformMovement(move, false, jumpState);

            move = Vector2.up * deltaPosition.y;

            PerformMovement(move, true, jumpState);
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    _audioSystem.PlayAudio(ConstantDefinitions.JUMP_SFX);
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        //Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.FlyWithBubble:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                        Landing();
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    _gfxSystem.ChangeColor(false);
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jumpState == JumpState.FlyWithBubble)
            {
                if (isSpaceBarPressed)
                {
                    velocity.y = -_configSystem.Config.flySpeedIncreaseStep; // Apply downward velocity
                }
                else
                {
                    velocity.y = _configSystem.Config.flySpeedIncreaseStep; // Apply upward velocity
                }
                //velocity.y += isSpaceBarPressed ? -_configSystem.Config.flySpeedIncreaseStep : _configSystem.Config.flySpeedIncreaseStep;
                // float floatingSpeed = isSpaceBarPressed ? -_configSystem.Config.flySpeedIncreaseStep : _configSystem.Config.flySpeedIncreaseStep;
                // targetVelocity.y += floatingSpeed * Time.deltaTime;
                speed += _configSystem.Config.flySpeedIncreaseStep * Time.deltaTime;
            }
            else
            {
                if (jump && IsGrounded)
                {
                    velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                    jump = false;
                }
                else if (stopJump)
                {
                    stopJump = false;
                    if (velocity.y > 0)
                    {
                        velocity.y = velocity.y * model.jumpDeceleration;
                    }
                }
            }
            
            _gfxSystem.FlipSprite(move.x < -0.01f);
            
            _animationSystem.Jump(IsGrounded);
            _animationSystem.Move(Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * speed;
        }

        public void Movement(Vector2 direction)
        {
            move.x = direction.x;
        }

        public void Jump(bool isPress)
        {
            if (jumpState == JumpState.FlyWithBubble)
            {
                isSpaceBarPressed = isPress;
                return;
            }
            if (jumpState == JumpState.Grounded && isPress)
            {
                jumpState = JumpState.PrepareToJump;
            }
            else if (!isPress)
            {
                stopJump = true;
                Schedule<PlayerStopJump>().player = this;
            }
        }

        public void TakeOff()
        {
            velocity.y = _configSystem.Config.takeOffSpeed * model.jumpModifier;
            jump = false;
            stopJump = false;
        }

        public void Landing()
        {
            speed = maxSpeed;
            isSpaceBarPressed = false;
        }

        public enum JumpState : byte
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed,
            FlyWithBubble,
            None
        }
    }
}