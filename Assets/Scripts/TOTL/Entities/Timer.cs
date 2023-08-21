using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TOTL
{
    public class Timer : MonoBehaviour
    {
        private bool _canLoad;

        private void Awake()
        {
            StartCoroutine(StartTime());

            IEnumerator StartTime()
            {
                yield return new WaitForSeconds(1.5f);
                _canLoad = true;
            }
        }

        private void Update()
        {
            if (_canLoad && Input.anyKeyDown)
            {
                // AudioManager.Instance.Play("ding");
                SceneManager.LoadScene("Level1");
            }
        }
    }
}