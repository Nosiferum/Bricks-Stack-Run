using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;

namespace DogukanKarabiyik.BricksStackRun.Environment.Obstacles {

    public class ObstacleBehaviour : MonoBehaviour {

        private bool isEngaged = false;

        private void OnTriggerStay(Collider other) {

            if (other.tag == "Player" && !isEngaged) {

                StartCoroutine(DestoryBricks(other));
                isEngaged = true;
            }                                                    
        }

        private IEnumerator DestoryBricks(Collider other) {

            var player = other.GetComponent<PlayerController>();

            if (player.bricks.Count > 0) {

                var brick = player.bricks[player.bricks.Count - 1];
                player.bricks.RemoveAt(player.bricks.Count - 1);

                if (player.stackConditionCounter > 0)
                    player.stackConditionCounter--;

                Destroy(brick);

                yield return new WaitForSeconds(1f);

                isEngaged = false;
            }
        }
    }
}


