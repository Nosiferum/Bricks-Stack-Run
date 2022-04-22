using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;
using TMPro;

namespace DogukanKarabiyik.BricksStackRun.Environment.Flags {

    public class StackFlag : MonoBehaviour {

        [SerializeField]
        private GameObject brickPrefab;
        
        [SerializeField]
        private int brickSpawnCount = 5;

        private bool isEngaged = false;

        private void Start() {

            GetComponent<TextMeshPro>().text = "--" + brickSpawnCount + "--";
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player" && !isEngaged) {

                StartCoroutine(StackBricks(other));
                isEngaged = true;
            }
        }

        private IEnumerator StackBricks(Collider other) {

            var player = other.GetComponent<PlayerController>();
                    
            for (int i = 0; i < brickSpawnCount; i++) {

                var brick = Instantiate(brickPrefab, other.transform);

                if (player.bricks.Count == 0) {

                    player.bricks.Add(brick);
                    
                    yield return new WaitForSeconds(.1f);
                }

                else {

                    brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x, player.bricks[player.bricks.Count - 1].transform.localPosition.y + 0.1f, player.bricks[player.bricks.Count - 1].transform.localPosition.z);
                    player.bricks.Add(brick);
                
                    yield return new WaitForSeconds(.1f);

                }
            }
        }
    }
}

    
