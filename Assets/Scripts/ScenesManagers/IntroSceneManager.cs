using System.Collections;
using DG.Tweening;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using Sequence = DG.Tweening.Sequence;

namespace ScenesManagers
{
    public class IntroSceneManager : BaseMonoBehaviour
    {
        private static IntroSceneManager instance;
        
        private delegate void ProgressDelegate(float progress);
        
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button startButton;
        [SerializeField] private Button tutoButton;
        [SerializeField] private Text loadingText;
        
        private Sequence _startBtnAnimationSequence;
        private Sequence _tutoBtnAnimationSequence;
        
        private Vector2 _desiredScale;
        private float _currentTime;
        private float _desiredTime = 5;
        
        protected override void ReleaseReferences()
        {
            instance = null;
            startButton = null;
            tutoButton = null;
            loadingText = null;
            canvasGroup = null;
        }
        private void Awake()
        {
            // Restricts the instantiation of the manager to a singular instance.
            if(instance!=null) return;
            instance = this;
            
            canvasGroup.alpha = 0;
            _startBtnAnimationSequence = DOTween.Sequence();
            _tutoBtnAnimationSequence = DOTween.Sequence();
            startButton.onClick.AddListener(()=> StartCoroutine(LoadSceneAsyncByName("inGame", OnLoadLevelProgressUpdate)));
            tutoButton.onClick.AddListener(OpenTuto);
            _desiredScale = new Vector2(1.04f, 1.02f);
            canvasGroup.alpha = 1;
            AnimateMainButtons();
        }

        private IEnumerator LoadSceneAsyncByName(string nextLevel, ProgressDelegate progressDelegate)
        {
            _currentTime = 0;
            UpdateButtonsStatus();
            var async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextLevel);

            while (_currentTime < _desiredTime)
            {
                progressDelegate(_currentTime/_desiredTime*100);
                yield return null;
                _currentTime += Time.deltaTime;
                async.allowSceneActivation = _currentTime > _desiredTime;
            }
        }

        private void UpdateButtonsStatus()
        {
            tutoButton.enabled = false;
            startButton.enabled = false;
            _startBtnAnimationSequence.Kill();
            _tutoBtnAnimationSequence.Kill();
        }

        private void OnLoadLevelProgressUpdate(float progress) => loadingText.text = progress.ToString("0.0") + " %";
        private void OpenTuto() {}
        private void AnimateMainButtons()
        {
            _startBtnAnimationSequence.Append(startButton.transform.DOScale(_desiredScale, 0.25f)
                .Play()
                .SetLoops(-1, LoopType.Yoyo)
                .OnComplete(() => { startButton.transform.DOScale(Vector3.one, 0.35f).Play(); }));

            _tutoBtnAnimationSequence.Append(tutoButton.transform.DOScale(_desiredScale, 0.25f)
                .Play()
                .SetLoops(-1, LoopType.Yoyo)
                .OnComplete(() => { tutoButton.transform.DOScale(Vector3.one, 0.35f).Play(); }));
            
        }
    }
}
