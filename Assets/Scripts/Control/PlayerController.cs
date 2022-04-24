using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DogukanKarabiyik.BricksStackRun.Control {

    public class PlayerController : MonoBehaviour {

        [SerializeField]
        private float runnigSpeed = 5f;

        [SerializeField]
        private float movingSpeed = 5f;

        private Touch touch;
        private float deadZone = 0.8f;
        private float dragBoundary = 1.5f;

        public Rigidbody rb { get; private set; }
        public Animator animator { get; private set; }
        public bool isMoving { get; set; } = false;
        public bool isFinished { get; set; } = false;
        public List<GameObject> bricks { get; private set; } = new List<GameObject>();
        public int stackCondition { get; set; } = 1;
        public int stackConditionCounter { get; set; } = 0;
      
        private void Awake() {

            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() {

            if (isMoving && !isFinished) {

                rb.MovePosition(transform.position + (Vector3.forward * runnigSpeed * Time.fixedDeltaTime));

                if (Input.touchCount > 0) {

                    touch = Input.GetTouch(0);

                    if (Input.GetTouch(0).phase == TouchPhase.Moved) {

                        if (touch.deltaPosition.x > deadZone) {

                            Vector3 rightVector = new Vector3(touch.deltaPosition.x - deadZone, 0, 0);

                            if (touch.deltaPosition.x > dragBoundary)
                                rightVector = new Vector3(dragBoundary, 0, 0);

                            rb.MovePosition(transform.position + (Vector3.forward * runnigSpeed * Time.fixedDeltaTime) + (rightVector * movingSpeed * Time.fixedDeltaTime));
                        }

                        else if (touch.deltaPosition.x < -deadZone) {

                            Vector3 leftVector = new Vector3(touch.deltaPosition.x + deadZone, 0, 0);

                            if (touch.deltaPosition.x < -dragBoundary)
                                leftVector = new Vector3(-dragBoundary, 0, 0);

                            rb.MovePosition(transform.position + (Vector3.forward * runnigSpeed * Time.fixedDeltaTime) + (leftVector * movingSpeed * Time.fixedDeltaTime));
                        }
                    }
                }

                else if (Input.GetMouseButton(1))
                    rb.MovePosition(transform.position + (Vector3.forward * runnigSpeed * Time.fixedDeltaTime) + (Vector3.right * movingSpeed * Time.fixedDeltaTime));

                else if (Input.GetMouseButton(0))
                    rb.MovePosition(transform.position + (Vector3.forward * runnigSpeed * Time.fixedDeltaTime) + (Vector3.left * movingSpeed * Time.fixedDeltaTime));
            }         
        }

        private void Update() {

            if (Input.GetKey(KeyCode.Mouse0)) {

                animator.SetBool("isRunning", true);
                isMoving = true;
            }
        }       
    }
}

   
