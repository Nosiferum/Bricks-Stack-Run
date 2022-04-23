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
        private float playerPosX;
        private bool isEngaged = false;
        private PlayerController player;


        private void Awake() {

            player = FindObjectOfType<PlayerController>();
        }

        private void Start() {

            startPosZ = transform.GetChild(0).transform.position.z;
            endPosZ = transform.GetChild(1).transform.position.z;
            lastBuildPosZ = startPosZ;
        }

        private void Update() {
            
            if (isEngaged)
            playerPosX = player.transform.position.x;
        }

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player") {

                isEngaged = true;
                //this code snippet looks like its redundant however, the first update call is not fast enough to get the first X position
                playerPosX = other.transform.position.x;

                StartCoroutine(BuildBridge(other));
            }                            
        }

        private IEnumerator BuildBridge(Collider other) {

            var deltaPos = endPosZ - startPosZ;
            var totalBridgeParts = Mathf.CeilToInt(deltaPos / (bridgePartPrefab.transform.localScale.z * 10));
                        
                for (int i = 0; i < totalBridgeParts; i++) {

                    //Game Over
                    if (player.bricks.Count == 0)
                        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

                    else {

                    var brick = player.bricks[player.bricks.Count - 1];
                    player.bricks.RemoveAt(player.bricks.Count - 1);

                    if(player.stackConditionCounter > 0)
                        player.stackConditionCounter--;

                    Destroy(brick);
                    
                    var bridgePart = Instantiate(bridgePartPrefab, new Vector3(playerPosX, bridgePartPrefab.transform.position.y, lastBuildPosZ + quadPerimeter), Quaternion.identity);
                    lastBuildPosZ = bridgePart.transform.position.z + quadPerimeter;

                    yield return new WaitForSeconds(.2f);
                }                                              
            }

            isEngaged = false;
        }
    }
}


