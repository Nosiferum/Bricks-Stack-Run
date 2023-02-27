using System.Collections;
using UnityEngine;
using DogukanKarabiyik.BricksStackRun.Control;

namespace DogukanKarabiyik.BricksStackRun.Environment.Flags
{
    public class BridgeFlag : MonoBehaviour
    {
        [SerializeField] private GameObject bridgePartPrefab;

        private float _startPosZ;
        private float _endPosZ;
        private const float QuadPerimeter = 0.5f;
        private float _lastBuildPosZ;
        private float _playerPosX;
        private bool _isEngaged = false;
        private PlayerController _player;
        
        private void Awake()
        {
            _player = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            _startPosZ = transform.GetChild(0).transform.position.z;
            _endPosZ = transform.GetChild(1).transform.position.z;
            _lastBuildPosZ = _startPosZ;
        }

        private void Update()
        {
            if (_isEngaged)
                _playerPosX = _player.transform.position.x;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isEngaged = true;
                //this code snippet looks like its redundant however, the first update call is not fast enough to get the first X position
                _playerPosX = other.transform.position.x;

                StartCoroutine(BuildBridge(other));
            }
        }

        private IEnumerator BuildBridge(Collider other)
        {
            var deltaPos = _endPosZ - _startPosZ;
            var totalBridgeParts = Mathf.CeilToInt(deltaPos / (bridgePartPrefab.transform.localScale.z * 10));

            for (int i = 0; i < totalBridgeParts; i++)
            {
                //Game Over
                if (_player.bricks.Count == 0)
                    UnityEngine.SceneManagement.SceneManager.LoadScene(0);

                else
                {
                    var brick = _player.bricks[_player.bricks.Count - 1];
                    _player.bricks.RemoveAt(_player.bricks.Count - 1);
                    _player.stackConditionCounter--;

                    Destroy(brick);

                    var bridgePart = Instantiate(bridgePartPrefab,
                        new Vector3(_playerPosX, bridgePartPrefab.transform.position.y, _lastBuildPosZ + QuadPerimeter),
                        Quaternion.identity);
                    _lastBuildPosZ = bridgePart.transform.position.z + QuadPerimeter;

                    yield return new WaitForSeconds(.2f);
                }
            }

            _isEngaged = false;
        }
    }
}