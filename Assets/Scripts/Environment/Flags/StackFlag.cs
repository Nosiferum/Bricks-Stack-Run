using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;

namespace DogukanKarabiyik.BricksStackRun.Environment.Flags {

    public class StackFlag : MonoBehaviour {

        [SerializeField]
        private GameObject brickPrefab;

        [SerializeField]
        private int brickSpawnCount = 5;

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player") {

                StartCoroutine(StackBricks(other));  
            }
        }

        private IEnumerator StackBricks(Collider other) {


            var player = other.GetComponent<PlayerController>();
            var brickCount = player.bricks.Count;
            var bricks = player.bricks;
            var a = 0;

            for (int i = 0; i < brickSpawnCount; i++) {

                var brick = Instantiate(brickPrefab, other.transform);

                if (a == 0) {

                    bricks.Add(brick);
                    a++;
                    yield return new WaitForSeconds(1f);
                }
                   

                else {

                    brick.transform.localPosition = new Vector3(bricks[brickCount - 1].transform.localPosition.x, bricks[brickCount - 1].transform.localPosition.y + 0.1f, bricks[brickCount - 1].transform.localPosition.z);
                    bricks.Add(brick);
                    yield return new WaitForSeconds(1f);

                }
            }         
        }
    }
}

    
