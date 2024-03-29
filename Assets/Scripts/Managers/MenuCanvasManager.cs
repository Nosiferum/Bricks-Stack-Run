using DogukanKarabiyik.BricksStackRun.Managers;
using UnityEngine;

namespace DogukanKarabiyik.BricksStackRun.Managers
{
    public class MenuCanvasManager : MonoBehaviour
    {
        private void DeactivateParent()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.GameStart();
                DeactivateParent();
            }
        }
    }
}

