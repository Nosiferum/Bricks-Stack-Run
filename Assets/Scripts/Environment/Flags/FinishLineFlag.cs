using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;

namespace DogukanKarabiyik.BricksStackRun.Environment.Flags {

    public class FinishLineFlag : MonoBehaviour {

        private void OnTriggerEnter(Collider other) {

            if (other.tag == "Player") {

                var player = other.GetComponent<PlayerController>();

                player.isMoving = false;
                player.isFinished = true;
                player.animator.SetBool("isWon", true);

            }
        }
    }
}


    
