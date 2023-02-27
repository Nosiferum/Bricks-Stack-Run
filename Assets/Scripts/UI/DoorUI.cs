using DogukanKarabiyik.BricksStackRun.Environment.Flags;
using TMPro;
using UnityEngine;

namespace DogukanKarabiyik.BricksStackRun.UI
{
    public class DoorUI : MonoBehaviour
    {
        private StackFlag _stackFlag;
        private TextMeshPro _textUI;

        private void Awake()
        {
            _stackFlag = GetComponent<StackFlag>();
            _textUI = GetComponent<TextMeshPro>();
        }
        
        private void Start()
        {
            _textUI.text = "--" + _stackFlag.GetBrickSpawnCount + "--";
        }
    }
}

