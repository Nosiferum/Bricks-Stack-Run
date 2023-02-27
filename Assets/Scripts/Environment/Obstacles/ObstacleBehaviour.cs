using System.Collections;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;

namespace DogukanKarabiyik.BricksStackRun.Environment.Obstacles {

    public class ObstacleBehaviour : MonoBehaviour {

        private bool _isEngaged = false;

        private void OnTriggerStay(Collider other) {

            if (other.CompareTag("Player") && !_isEngaged) {

                StartCoroutine(DestroyBricks(other));
                _isEngaged = true;
            }                                                    
        }

        private IEnumerator DestroyBricks(Collider other) {

            var player = other.GetComponent<PlayerController>();

            if (player.bricks.Count > 0) {
                                                  
                var brick = player.bricks[player.bricks.Count - 1];
                player.bricks.RemoveAt(player.bricks.Count - 1);
                player.stackConditionCounter--;
            
                Destroy(brick);

                yield return new WaitForSeconds(1f);

                _isEngaged = false;
            }
        }
    }
}


