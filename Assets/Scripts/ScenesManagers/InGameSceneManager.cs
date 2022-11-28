using System.Collections;
using DG.Tweening;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace ScenesManagers
{
    public class InGameSceneManager : EntryPointSystemBase
    {
        public static InGameSceneManager Instance;
        
        [SerializeField] private RectTransform background;
        [SerializeField] private RectTransform startGameMessage;
        [SerializeField] private Text collectedSunsText;
        private WaitForSeconds _waitForSeconds;
        public bool startGame;
        public int collectedSuns ;

        public override void Begin()
        {
            if(Instance!=null) return;
            Instance = this;
            StartCoroutine(StartSceneAnimation());
            _waitForSeconds = new WaitForSeconds(2.5f);
            collectedSuns = 50;
            collectedSunsText.text = collectedSuns.ToString();
        }
        
        protected override void ReleaseReferences()
        {
            collectedSunsText = null;
            background = null;
            Instance = null;
            _waitForSeconds = null;
            startGameMessage = null;
        }

        private IEnumerator StartSceneAnimation()
        {
            yield return _waitForSeconds;
            var position = background.position;
            background.DOMoveX(position.x - 600, 2.8f).Play().SetEase(Ease.InExpo).OnComplete(() =>
            {
                position = background.position;
                background.DOMoveX(position.x+600 , 3.2f).Play().SetEase(Ease.Linear).OnComplete(() =>
                    {
                        startGameMessage.gameObject.SetActive(true);
                        startGameMessage.DOAnchorPos(new Vector2(startGameMessage.anchoredPosition.x, 0), 0.8f)
                            .SetEase(Ease.InOutElastic).Play().OnComplete(() =>
                            {
                                startGame = true;
                                Invoke(nameof(DisableStartPlantingMessage), 1.75f);
                            });
                    });
                });
        }

        private void DisableStartPlantingMessage()
        {
            startGameMessage.DOAnchorPos(new Vector2(startGameMessage.anchoredPosition.x, +800), 0.65f)
                .SetEase(Ease.Linear).Play().OnComplete(() => startGameMessage.gameObject.SetActive(false));
        }

        public void UpdateCollectedSunsText() =>  collectedSunsText.text = collectedSuns.ToString();
       
    }
}
