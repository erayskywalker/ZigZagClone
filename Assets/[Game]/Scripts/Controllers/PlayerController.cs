using UnityEngine;
using ZigZagClone.Movements;

namespace ZigZagClone.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        #region Player Movement Variables

        [Header("Speed Variables")] [Range(2f, 5f)] [SerializeField]
        private float movingSpeed = 2f;

        [Range(5f, 10f)] [SerializeField] private float speedUpFactor = 10f;
        public float CurrentSpeed { get; private set; }
        [Space] private PlayerEnums directionEnum = PlayerEnums.Right;
        private PlayerMovement playerMover;

        #endregion
        
        private void Awake()
        {
            playerMover = new PlayerMovement(movingSpeed, transform);
        }

        private void Start()
        {
            CurrentSpeed = movingSpeed;
        }

        private void Update()
        {
            DirectionControl();

            GroundControl();

            SpeedUpControl();
        }


        private void FixedUpdate()
        {
            playerMover.Move();
        }

        private void SpeedUpControl()
        {
            if (!GameManager.Instance.IsGameStarted) return;
            if (GameManager.Instance.IsGameEnded) return;

            CurrentSpeed += Time.deltaTime / speedUpFactor;
            playerMover.SetSpeed(CurrentSpeed);
        }

        private void GroundControl()
        {
            if (!GameManager.Instance.IsGameStarted) return;
            if (GameManager.Instance.IsGameEnded) return;
            
            var isFalling = transform.position.y < 0.6f;
            
            if (isFalling)
                GameManager.Instance.OnLevelEnded(false);
        }


        private void DirectionControl()
        {
            if (!GameManager.Instance.IsGameStarted) return;
            if (GameManager.Instance.IsGameEnded) return;
            if (!PlayerInputs.ChangeDirection) return;

            ScoreManager.Instance.SetScore("click", 1);

            directionEnum = directionEnum == PlayerEnums.Right ? PlayerEnums.Forward : PlayerEnums.Right;

            switch (directionEnum)
            {
                case PlayerEnums.Right:
                    playerMover.SetDirection(transform.right);
                    break;
                case PlayerEnums.Forward:
                    playerMover.SetDirection(transform.forward);
                    break;
            }
        }
    }
}