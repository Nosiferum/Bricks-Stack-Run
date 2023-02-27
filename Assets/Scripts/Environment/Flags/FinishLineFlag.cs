using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Managers;

namespace DogukanKarabiyik.BricksStackRun.Environment.Flags 
{
    public class FinishLineFlag : MonoBehaviour 
    {
        private void OnTriggerEnter(Collider other) 
        {
            if (other.CompareTag("Player"))
                GameManager.GameSuccess();
        }
    }
}


    
