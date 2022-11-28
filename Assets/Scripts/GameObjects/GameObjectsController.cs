using Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameObjects
{
    public class GameObjectsController : BaseMonoBehaviour, IBeginDragHandler ,IDragHandler ,IEndDragHandler
    {
        [SerializeField] protected Button button;
        [SerializeField] protected int value;
        protected override void ReleaseReferences() => button = null;
        public virtual void OnBeginDrag(PointerEventData eventData){}

        public virtual void OnDrag(PointerEventData eventData){}

        public virtual void OnEndDrag(PointerEventData eventData){}

       
    }
}
