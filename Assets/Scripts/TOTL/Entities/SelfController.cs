using Moss;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace TOTL
{
    public class SelfController : MonoSingleton<SelfController>
    {
        [FormerlySerializedAs("CanMove")] public bool canMove;

        private void Update()
        {
            _Restart();
            _Move();
        }

        private void _Move()
        {
            if (!canMove) return;
            

            var nextDir = _GetNextDir();

            if (nextDir != Vector3.zero)
            {
                AudioManager.Instance.Play("dong");
                _ProcessMove(nextDir);
            }
        }

        private void _ProcessMove(Vector3 nextDir)
        {
            var localPosition = transform.localPosition;
            var isInLight1 = BoxManager.Instance.IsInLight(localPosition + nextDir);
            var isInLight2 = BoxManager.Instance.IsInLight(localPosition + 2 * nextDir);
            var isBox1 = BoxManager.Instance.TryGetBox(localPosition + nextDir, out var box1);
            var isBox2 = BoxManager.Instance.TryGetBox(localPosition + 2 * nextDir, out _);
            if (isInLight1)
            {
                if (isInLight2)
                {
                    if (isBox1)
                    {
                        if (!isBox2)
                        {
                            if (!BoxManager.Instance.IsLock(box1))
                            {
                                transform.localPosition += nextDir;
                                box1.transform.localPosition += nextDir;
                                BoxManager.Instance.UpdateBox();
                            }
                        }
                    }
                    else
                    {
                        transform.localPosition += nextDir;
                    }
                }
                else
                {
                    if (!isBox1)
                    {
                        transform.localPosition += nextDir;
                    }
                }
            }

            JudgeIfFail();
            JudgeIfSuccess();
        }

        private void JudgeIfFail()
        {
            if (BoxManager.Instance.IsInLight(transform.localPosition)) return;
            canMove = false;
            UIManager.Instance.OpenFailPanel();
        }

        private void JudgeIfSuccess()
        {
            if (!BoxManager.Instance.IsAllLight()) return;
            canMove = false;
            UIManager.Instance.OpenSuccessPanel();
        }

        private void _Restart()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                AudioManager.Instance.Play("ding");
                var currSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currSceneName);
            }
        }

        private Vector3 _GetNextDir()
        {
            Vector3 nextDir;

            if (Input.GetKeyDown(KeyCode.W))
                nextDir = Vector3.up;
            else if (Input.GetKeyDown(KeyCode.A))
                nextDir = Vector3.left;
            else if (Input.GetKeyDown(KeyCode.S))
                nextDir = Vector3.down;
            else if (Input.GetKeyDown(KeyCode.D))
                nextDir = Vector3.right;
            else nextDir = Vector3.zero;

            return nextDir;
        }
    }
}