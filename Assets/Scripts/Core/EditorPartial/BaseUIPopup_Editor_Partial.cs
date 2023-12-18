using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract partial class BaseUIPopup : BaseUI
    {
#if UNITY_EDITOR
        public void FindContentObject()
        {
            if (ContentObject == null)
            {
                FindContentObject(transform);
            }
        }

        void FindContentObject(Transform parent)
        {
            foreach (Transform tf in parent)
            {
                if (tf.name == Const.ContentObjectName || tf.CompareTag(Const.ContentObjectTag))
                {
                    ContentObject = tf.gameObject;
                    return;
                }
                FindContentObject(tf);
            }
        }
#endif
    }
}
