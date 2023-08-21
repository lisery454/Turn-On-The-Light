using System.Collections;
using Moss;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TOTL
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private Text text;
        [SerializeField] private GameObject inFacade;
        [SerializeField] private GameObject failPanel;
        [SerializeField] private GameObject successPanel;
        [SerializeField] private string nextSceneName;


        protected override void Awake()
        {
            base.Awake();
            CloseFailPanel();
            CloseSuccessPanel();
            inFacade.SetActive(true);
            StartCoroutine(InFacadeDisappearCoroutine());

            IEnumerator InFacadeDisappearCoroutine()
            {
                yield return new WaitForSeconds(1.5f);
                inFacade.SetActive(false);
                SelfController.Instance.canMove = true;
            }
        }

        private void Update()
        {
            if (successPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }

        public void UpdateLampNum(int litLampNum, int allLampNum)
        {
            text.text = $"{litLampNum}/{allLampNum}";
        }

        public void CloseFailPanel()
        {
            failPanel.SetActive(false);
        }

        public void OpenFailPanel()
        {
            StartCoroutine(Open());

            IEnumerator Open()
            {
                AudioManager.Instance.Play("fail");
                yield return new WaitForSeconds(1f);
                failPanel.SetActive(true);
            }
        }

        public void CloseSuccessPanel()
        {
            successPanel.SetActive(false);
        }

        public void OpenSuccessPanel()
        {
            StartCoroutine(Open());

            IEnumerator Open()
            {
                AudioManager.Instance.Play("success");
                yield return new WaitForSeconds(1f);
                successPanel.SetActive(true);
            }
        }
    }
}