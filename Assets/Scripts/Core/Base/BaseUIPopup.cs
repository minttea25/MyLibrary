using UnityEngine;
using UnityEditor;

namespace Core
{
    public abstract class BaseUIPopup : BaseUI
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

#if UNITY_EDITOR
        internal void FindContentObject()
        {
            if (ContentObject == null)
            {
                FindContentObject(transform);
            }
            Utils.Assert(ContentObject != null, "Can not find ContentObject");
        }

        void FindContentObject(Transform parent)
        {
            foreach(Transform tf in parent)
            {
                if (tf.CompareTag(Const.ContentObjectTag) || tf.name == Const.ContentObjectName)
                {
                    ContentObject = tf.gameObject;
                    return;
                }
                FindContentObject(tf);
            }
        }
#endif
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(BaseUIPopup), true)]
    public class BaseUIPopupEditor : BaseUIEditor
    {
        SerializedProperty content;

        private void OnEnable()
        {
            context = serializedObject.FindProperty(Const.UIContextFieldName);
            content = serializedObject.FindProperty(Const.ContentObjectFieldName);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (content != null)
            {
                EditorGUILayout.PropertyField(content, true);
                if (GUILayout.Button("Assign Content Object"))
                {
                    (target as BaseUIPopup).FindContentObject();
                    EditorUtility.SetDirty(target);
                }
            }
            else
            {
                Debug.LogWarning($"Can not find: {Const.ContentObjectFieldName}");
            }

            if (context != null)
            {
                EditorGUILayout.PropertyField(context, true);

                EditorGUILayout.Space(10);

                if (GUILayout.Button("Auto - Find&Assign UI Object"))
                {
                    (target as BaseUI).AutoAssignUIs();
                    EditorUtility.SetDirty(target);
                }
            }
            else
            {
                Debug.LogWarning($"Can not find: {Const.UIContextFieldName}");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
