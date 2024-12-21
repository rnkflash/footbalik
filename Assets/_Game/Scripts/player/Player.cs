using System;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.player
{
    public class Player : MonoBehaviour
    {
        readonly static int AnimSpeedParameter = Animator.StringToHash("Speed");
        readonly static int AnimKickParameter = Animator.StringToHash("Kick");
        
        [SerializeField] Transform capturedBallTransform;
        [SerializeField] float walkSpeed = 1.0f;
        [SerializeField] float runSpeed = 2.0f;

        Ball ball;
        Animator animator;
        BallChecker ballChecker;
        float captureCooldown;
        KickAnimation kickAnimation;
        bool kickInProcess;
        IPlayerControl playerControl;

        Vector2 move;
        bool running;
        bool kick;
        Vector3 target;

        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            ballChecker = GetComponentInChildren<BallChecker>();
            kickAnimation = GetComponentInChildren<KickAnimation>();
            playerControl = GetComponent<IPlayerControl>();
        }

        void Update()
        {
            move = playerControl.GetMoveDirection();
            running = playerControl.IsRunning();
            kick = playerControl.IsKick();

            if (captureCooldown > 0)
                captureCooldown = Mathf.Max(captureCooldown - Time.deltaTime, 0.0f);

            if (kick)
                Kick(playerControl.GetMousePosition());
        }

        void FixedUpdate()
        {
            var dt = Time.fixedDeltaTime;
            var speed = running ? runSpeed : walkSpeed;
            var positionDelta = new Vector3(move.x * speed * dt, 0.0f, move.y * speed * dt);
            transform.position += positionDelta;

            HandleVisual(positionDelta);
            
            if (captureCooldown <= 0.0f)
                CaptureBall();
        }

        void HandleVisual(Vector3 positionDelta)
        {
            if (positionDelta.x != 0)
            {
                SetFlip(positionDelta.x < 0);
            }
            
            var animSpeed = 0.0f;
            if (positionDelta.x != 0 || positionDelta.z != 0)
            {
                animSpeed = running ? 1.0f : 0.5f;
            }
            animator.SetFloat(AnimSpeedParameter, animSpeed);
        }


        void Kick(Vector3 target)
        {
            if (ball == null || kickInProcess) return;
            this.target = target;
            kickInProcess = true;
            kickAnimation.kickEvent.AddListener(ActuallyKick);
            animator.SetTrigger(AnimKickParameter);
        }

        void ActuallyKick()
        {
            ball.transform.DOKill(true);
            ball.UnCapture();
            ball.transform.SetParent(null);

            ball.Kick(target, 1.0);
            
            ball = null;
            captureCooldown = 0.1f;

            kickInProcess = false;
            kickAnimation.kickEvent.RemoveListener(ActuallyKick);
        }

        void CaptureBall()
        {
            var newBall = ballChecker.GetBall();
            if (newBall == null || newBall.IsCaptured() || ball != null) return;
            
            ball = newBall;
            newBall.Capture(this);
            newBall.transform.SetParent(capturedBallTransform);
            newBall.transform.DOLocalMove(Vector3.zero, 0.1f);
        }

        void SetFlip(Boolean flip)
        {
            var x = flip ? -1.0f : 1.0f;
            transform.localScale = new Vector3(x * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
