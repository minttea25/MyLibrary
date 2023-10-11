using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public abstract class BaseUIItem : BaseUI
    {
        public override void Init()
        {
            base.Init();

            _ = gameObject.GetOrAddComponent<UIEventHandlerDynamic>();
        }

        public void RemoveDynamicEventHandler()
        {
            Destroy(gameObject.GetComponent<UIEventHandlerDynamic>());
        }
    }
}
