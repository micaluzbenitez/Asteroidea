using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Toolbox;

namespace Entities.Platforms
{
    public class Platform : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [Header("Lateral Limits")]

        [Header("Obstacle Spawn")]
        [SerializeField] private GameObject obstacle;
        [SerializeField] private GameObject model = null;

        [Header("Horizontal movement")]
        [SerializeField] private bool horizontalMovement = false;
        [SerializeField] private float horizontalSpeed = 0;
        [SerializeField] private float hSpawnRate = 0.07f;

        [Header("Breakable platform")]
        [SerializeField] private bool breakablePlatform = false;
        [SerializeField] private float breakableSpeed = 0;
        [SerializeField] private GameObject breakableModel = null;
        [SerializeField] private Animator[] breakableAnimator = null;
        [SerializeField] private float bSpawnRate = 0.07f;

        [Header("Platform Respawn")]
        [SerializeField] private Transform platformRespawnPos;
        [SerializeField] private bool isVisible;
        #endregion

        #region STATIC VARIABLES
        #endregion

        #region PUBLIC VARIABLES
        #endregion

        #region PRIVATE VARIABLES
        private Rigidbody2D rigidBody;

        private float distanceToObstacle = 0.0f;

        private BoxCollider2D boxCollider;

        private bool startsOff;

        private float minX = 0;
        private float maxX = 0;

        private float maxXLimit = 0;
        private float minXLimit = 0;

        private bool movingRight = false;
        private float moveTime = 0;
        private float speed = 0;

        private ObjectShake objectShake = null;
        private Timer breakablePlatformTimer = new Timer();
        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS

        #endregion

        #region STATIC METHODS

        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            boxCollider = GetComponent<BoxCollider2D>();
            objectShake = GetComponent<ObjectShake>();  
            distanceToObstacle = Vector3.Distance(obstacle.transform.position, transform.position);
            breakablePlatformTimer.SetTimer(breakableSpeed, Timer.TIMER_MODE.DECREASE);

            objectShake.OnFinishShake += BreakPlatform;
        }

        private void Start()
        {
            speed = horizontalMovement? horizontalSpeed : 0;
            startsOff = !isVisible;

            minX = PlatformController.HorizontalLimits.Left + boxCollider.bounds.size.x / 2;
            maxX = PlatformController.HorizontalLimits.Right - boxCollider.bounds.size.x / 2;

            minXLimit = PlatformController.HorizontalLimits.Left;
            maxXLimit = PlatformController.HorizontalLimits.Right;
        }

        private void Update()
        {
            if(horizontalMovement) HorizontalMove();
            UpdateBreakPlatform();
        }

        private void OnDestroy()
        {
            objectShake.OnFinishShake += BreakPlatform;
        }

        private void SetRandomX()
        {
            float newX = minX + UnityEngine.Random.Range(0, (Mathf.Abs(minX - maxX)));
            transform.position = new Vector3(newX, transform.position.y);

            float A = minX;
            float B = maxX;
            float C = newX;

            moveTime = (Mathf.Abs(C - A) / Mathf.Abs(B - A));

        }

        private void ResetPosition()
        {
            if (startsOff) Enable();
            obstacle.SetActive(false);
            SetRandomX();
            CheckHorizontalMovement();
            transform.position = new Vector3(transform.position.x, platformRespawnPos.position.y);
            if (!CheckBreakablePlatform()) SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            float rand = UnityEngine.Random.Range(0.0f, 1.0f);
            Debug.Log("Random: " + rand + "/ SpawnRate: " + PlatformController.ObstacleSpawnRate);
            if (PlatformController.ObstacleSpawnRate > 1.0f || rand < PlatformController.ObstacleSpawnRate)
            {
                obstacle.transform.position = new Vector3(transform.position.x, platformRespawnPos.position.y + distanceToObstacle);
                obstacle.SetActive(true);
            }
        }

        private void HorizontalMove()
        {
            float delTime = Time.deltaTime * speed;

            moveTime += movingRight ? delTime : -delTime;

            float newX = Mathf.Lerp(
                PlatformController.HorizontalLimits.Left + boxCollider.bounds.size.x / 2,
                PlatformController.HorizontalLimits.Right - boxCollider.bounds.size.x / 2,
                moveTime);
            
            rigidBody?.MovePosition(new Vector2(newX,rigidBody.position.y));
                
               
            if (moveTime < 0)
            {
                movingRight = true;
                moveTime = 0;
            }
            else if (moveTime > 1) //si completa el lerp
            {
                movingRight = false;
                moveTime = 1;
            }
        }

        private void UpdateBreakPlatform()
        {
            if (breakablePlatformTimer.Active) breakablePlatformTimer.UpdateTimer();

            if (breakablePlatformTimer.ReachedTimer())
            {
                boxCollider.enabled = false;
                model.SetActive(false);
                breakableModel.SetActive(false);
            }
        }

        private void BreakPlatform()
        {
            for (int i = 0; i < breakableAnimator.Length; i++) breakableAnimator[i].SetBool("Break", true);
            breakablePlatformTimer.ActiveTimer();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("ResetZone"))
            {
                gameObject.SetActive(false);
                //ResetPosition();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && breakablePlatform) objectShake.StartShake();
        }

        private void Enable()
        {
            boxCollider.isTrigger = false;
            startsOff = false;
        }

        private void CheckHorizontalMovement()
        {
            horizontalMovement = Random.Range(0.0f, 1.0f) < hSpawnRate;
            speed = horizontalMovement ? horizontalSpeed : 0;
        }

        private bool CheckBreakablePlatform()
        {
            bool breakable = Random.Range(0.0f, 1.0f) < hSpawnRate;
            if (breakable) breakablePlatform = true;
            else breakablePlatform = false;

            boxCollider.enabled = true;

            if (breakablePlatform)
            {
                model.SetActive(false);
                breakableModel.SetActive(true);
                for (int i = 0; i < breakableAnimator.Length; i++) breakableAnimator[i].SetBool("Break", false);
            }
            else
            {
                model.SetActive(true);
                breakableModel.SetActive(false);
            }

            return breakablePlatform;
        }

        public void ResetPlatform()
        {
            ResetPosition();
        }


        #endregion
        #endregion
    }
}