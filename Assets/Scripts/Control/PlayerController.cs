using System;
using System.Collections;
using System.Collections.Generic;
using DogukanKarabiyik.BricksStackRun.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace DogukanKarabiyik.BricksStackRun.Control
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float forwardSpeed = 5f;
        [SerializeField] private float horizontalSpeed = 5f;

        public Rigidbody rb { get; private set; }
        public Animator animator { get; private set; }
        public List<GameObject> bricks { get; private set; } = new List<GameObject>();
        public int stackCondition { get; set; } = 1;
        public int stackConditionCounter { get; set; } = 0;

        private Touch touch;
        private float deadZone = 0.8f;
        private float dragBoundary = 1.5f;

        private Action playerState;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            playerState?.Invoke();
        }

        private void GamePlayState()
        {
            Move();
        }

        private void Move()
        {
            rb.MovePosition(transform.position + (Vector3.forward * (forwardSpeed * Time.fixedDeltaTime)));

            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (touch.deltaPosition.x > deadZone)
                    {
                        Vector3 rightVector = new Vector3(touch.deltaPosition.x - deadZone, 0, 0);

                        if (touch.deltaPosition.x > dragBoundary)
                            rightVector = new Vector3(dragBoundary, 0, 0);

                        rb.MovePosition(transform.position +
                                        (Vector3.forward * (forwardSpeed * Time.fixedDeltaTime)) +
                                        (rightVector * (horizontalSpeed * Time.fixedDeltaTime)));
                    }

                    else if (touch.deltaPosition.x < -deadZone)
                    {
                        Vector3 leftVector = new Vector3(touch.deltaPosition.x + deadZone, 0, 0);

                        if (touch.deltaPosition.x < -dragBoundary)
                            leftVector = new Vector3(-dragBoundary, 0, 0);

                        rb.MovePosition(transform.position +
                                        (Vector3.forward * (forwardSpeed * Time.fixedDeltaTime)) +
                                        (leftVector * (horizontalSpeed * Time.fixedDeltaTime)));
                    }
                }
            }

            else if (Input.GetMouseButton(0))
                rb.MovePosition(transform.position + (Vector3.forward * (forwardSpeed * Time.fixedDeltaTime)) +
                                (new Vector3(InputManager.Delta.x, 0, 0) * (horizontalSpeed * Time.fixedDeltaTime)));
        }

        private void SetStartingAnimation()
        {
            animator.SetBool("isRunning", true);
        }

        private void SetEndingAnimation()
        {
            animator.SetBool("isWon", true);
        }

        private void StartBroadcast()
        {
            SetStartingAnimation();
            playerState = GamePlayState;
        }

        private void EndBroadcast()
        {
            playerState = null;
            SetEndingAnimation();
            StartCoroutine(ReStartGame());
        }

        private IEnumerator ReStartGame()
        {
            yield return new WaitForSeconds(3f);
            
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }

        private void OnEnable()
        {
            GameManager.OnLevelStart += StartBroadcast;
            GameManager.OnLevelOver += EndBroadcast;
        }

        private void OnDisable()
        {
            GameManager.OnLevelStart -= StartBroadcast;
            GameManager.OnLevelOver -= EndBroadcast;
        }
    }
}