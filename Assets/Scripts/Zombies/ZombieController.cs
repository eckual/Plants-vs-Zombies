using DG.Tweening;
using ScenesManagers;
using Tools;
using UnityEngine;

namespace Zombies
{
    public class ZombieController : BaseMonoBehaviour
    {
        [SerializeField] private RectTransform rect;
        private Animator _animator;

        private Animator Animator
        {
            get
            {
                if (_animator == null) _animator = GetComponent<Animator>();
                return _animator;
            }
        }
        protected override void ReleaseReferences()
        {
            rect = null;
            _animator = null;
        }

        private void Awake()
        {
            Animator.enabled = false;
        }

        private void Update()
        {
            if(!InGameSceneManager.Instance.startGame) return;
            Animator.enabled = true;
            rect.DOAnchorPos(rect.anchoredPosition + new Vector2(-35, 0), 15f)
                .SetEase(Ease.OutBack)
                .Play();
        }
        
    }
}
