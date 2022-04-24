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

                if (player.stackConditionCounter % 10 == 1 || player.stackConditionCounter % 10 == 2)
                    player.stackCondition = 1;
                else if (player.stackConditionCounter % 10 == 3 )
                    player.stackCondition = 2;
                else if (player.stackConditionCounter % 10 == 4)
                    player.stackCondition = 3;
                else if (player.stackConditionCounter % 10 == 5)
                    player.stackCondition = 4;
                else if (player.stackConditionCounter % 10 == 6 || player.stackConditionCounter % 10 == 7)
                    player.stackCondition = 5;
                else if (player.stackConditionCounter % 10 == 8)
                    player.stackCondition = 6;
                else if (player.stackConditionCounter % 10 == 9)
                    player.stackCondition = 7;
                else if (player.stackConditionCounter % 10 == 0)
                    player.stackCondition = 8;

                if (player.bricks.Count == 0) {

                    player.bricks.Add(brick);
                    player.stackConditionCounter = 1;

                    yield return new WaitForSeconds(.1f);
                }

                else {

                    if (player.stackCondition == 1) {
                 
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x - 0.3f, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z);
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;                      
                    }

                    else if (player.stackCondition == 2) {
                     
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x - 0.1f, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z + 0.2f);
                            brick.transform.rotation = Quaternion.Euler(0, 90, 0);
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;                   

                    }

                    else if (player.stackCondition == 3) {
                     
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z + 0.3f);
                            brick.transform.rotation = Quaternion.Euler(0, 90, 0);
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;                     
                    }

                    else if (player.stackCondition == 4) {
                     
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x + 0.1f, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z + 0.2f);
                            brick.transform.rotation = Quaternion.Euler(0, 0, 0); //redundant, added for readability
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;                    
                    }

                    else if (player.stackCondition == 5) {
                       
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x + 0.3f, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z);
                            brick.transform.rotation = Quaternion.Euler(0, 0, 0); //redundant, added for readability
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;
                  
                    }

                    else if (player.stackCondition == 6) {
                     
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x + 0.1f, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z - 0.2f);
                            brick.transform.rotation = Quaternion.Euler(0, 90, 0);
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;                    
                    }

                    else if (player.stackCondition == 7) {
                     
                            brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x, player.bricks[player.bricks.Count - 1].transform.localPosition.y, player.bricks[player.bricks.Count - 1].transform.localPosition.z - 0.3f);
                            brick.transform.rotation = Quaternion.Euler(0, 90, 0);
                            player.bricks.Add(brick);
                            player.stackConditionCounter++;                    
                    }

                    else if (player.stackCondition == 8) {

                        brick.transform.localPosition = new Vector3(player.bricks[player.bricks.Count - 1].transform.localPosition.x - 0.1f, player.bricks[player.bricks.Count - 1].transform.localPosition.y + 0.1f, player.bricks[player.bricks.Count - 1].transform.localPosition.z - 0.2f);
                        brick.transform.rotation = Quaternion.Euler(0, 0, 0); //redundant, added for readability
                        player.bricks.Add(brick);
                        player.stackConditionCounter++;                      
                    }
                }

                yield return new WaitForSeconds(.1f);
            }
        }
    }
}



    
