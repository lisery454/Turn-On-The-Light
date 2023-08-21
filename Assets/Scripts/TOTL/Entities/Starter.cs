using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOTL
{
    public class Starter : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // AudioManager.Instance.Play("ding");
                SceneManager.LoadScene("Help");
            }
        }
    }
}