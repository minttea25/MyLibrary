using UnityEngine;
using UnityEditor;

namespace Core
{
    public abstract partial class BaseUIPopup : BaseUI
    {
        [Tooltip("A panel to apply show/hide animation; It must not be a canvas.\nIf not assigned in Editor, it would find with a tag or name.")]
        public GameObject ContentObject; // Do not change the name.

        VisibleStates _visibleState = VisibleStates.Disappeared;
        public VisibleStates VisibleState
        {
            get => _visibleState;
            set => _visibleState = value;
        }

        public enum VisibleStates
        {
            Appearing,
            Appeared,
            Disappearing,
            Disappeared,
        }

        public override void Init()
        {
            base.Init();

            ManagerCore.UI.SetCanvas(gameObject, true);

            BindContent();
            ContentObject.SetActive(false);
        }

        public virtual void ClosePopupUI()
        {
            VisibleState = VisibleStates.Disappearing;
            DOTweenAnimations.HideUI(ContentObject, callback: () =>
            {
                VisibleState = VisibleStates.Disappeared;
                ManagerCore.UI.ClosePopupUI(this);
            });
        }

        public virtual void Show()
        {
            VisibleState = VisibleStates.Appearing;
            DOTweenAnimations.ShowUI(ContentObject, callback: () =>
            {
                VisibleState = VisibleStates.Appeared;
            });
        }

        public virtual void Hide()
        {
            VisibleState = VisibleStates.Disappearing;
            DOTweenAnimations.HideUI(gameObject, callback: () => 
            {
                VisibleState = VisibleStates.Disappeared;
            });
        }

        internal void BindContent()
        {
            if (ContentObject == null)
            {
                ContentObject = GameObject.FindGameObjectWithTag(Const.ContentObjectTag);

                if (ContentObject == null)
                {
                    ContentObject = GameObject.Find(Const.ContentObjectName);
                }
            }
            Utils.Assert(ContentObject != null, "Can not find ContentObject");
        }
    }
}
