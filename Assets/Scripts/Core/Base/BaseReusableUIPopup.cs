using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class BaseReusableUIPopup : BaseUIPopup
    {
        public virtual void OpenPopup()
        {
            gameObject.SetActive(true);
            Show();
        }

        public virtual void ClosePopup()
        {
            VisibleState = VisibleStates.Disappearing;
            DOTweenAnimations.HideUI(ContentObject, callback: () =>
            {
                VisibleState = VisibleStates.Disappeared;
                gameObject.SetActive(false);
            });
        }
    }
}
