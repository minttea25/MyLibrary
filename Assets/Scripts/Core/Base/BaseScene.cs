using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    public abstract class BaseScene : MonoBehaviour
    {
        public SceneTypes Type { get; protected set; } = SceneTypes.INVALID;
        protected bool _init = false;



        private void Awake()
        {
            Init();
            _init = true;
        }

        /// <summary>
        /// It is called at Awake.
        /// </summary>
        protected virtual void Init()
        {
            ManagerCore.Init();

            UnityEngine.Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
            if (obj == null)
            {
                ManagerCore.Resource.InstantiateOnceAsync(Const.AddressableEventSystemKey, callback: obj => 

                {
                    obj.name = Const.EventSystemsName;
                });
            }
        }

        public virtual void Clear() { }
    }
}
