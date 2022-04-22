using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;

namespace DogukanKarabiyik.BricksStackRun.Environment.Flags {

    public class BridgeFlag : MonoBehaviour {

        [SerializeField]
        private GameObject bridgePartPrefab;

        private float startPosZ;
        private float endPosZ;
        private float quadPerimeter = 0.5f;
        private float lastBuildPosZ;

        private void Start() {

            startPosZ = transform.GetChild(0).transform.position.z;
            endPosZ = transform.GetChild(1).transform.position.z;
            lastBuildPosZ = startPosZ;
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player") 
                StartCoroutine(BuildBridge(other));            
        }

        private IEnumerator BuildBridge(Collider other) {

            var player = other.GetComponent<PlayerController>();
            var deltaPos = endPosZ - startPosZ;
            var totalBridgeParts = Mathf.CeilToInt(deltaPos / (bridgePartPrefab.transform.localScale.z * 10));
                
                for (int i = 0; i < totalBridgeParts; i++) {

                    //Game Over
                    if (player.bricks.Count == 0)
                        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

                    else {

                    var brick = player.bricks[player.bricks.Count - 1];
                    player.bricks.RemoveAt(player.bricks.Count - 1);
                    Destroy(brick);

                    var bridgePart = Instantiate(bridgePartPrefab, new Vector3(bridgePartPrefab.transform.position.x, bridgePartPrefab.transform.position.y, lastBuildPosZ + quadPerimeter), Quaternion.identity);
                    lastBuildPosZ = bridgePart.transform.position.z + quadPerimeter;

                    yield return new WaitForSeconds(.1f);
                }                                              
            }           
        }
    }
}


