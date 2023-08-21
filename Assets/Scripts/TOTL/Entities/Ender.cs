using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOTL
{
    public class Ender : MonoBehaviour
    {
        private void LateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Slash))
            {
                // AudioManager.Instance.Play("ding");
                SceneManager.LoadScene("Start");
            }
        }
    }
}