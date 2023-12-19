using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneManagerCore : IManager
    {
        public SceneTypes _currentSceneType = SceneTypes.INVALID;

        public SceneTypes CurrentSceneType
        {
            private set
            {
                _currentSceneType = value;
            }
            get
            {
                return _currentSceneType;
            }
        }

        public static BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

        public void LoadScene(SceneTypes sceneType)
        {
            CurrentScene.Clear();

            _currentSceneType = sceneType;
            SceneManager.LoadScene(GetSceneName(sceneType));
        }

        public void LoadScene(SceneTypes sceneType, LoadSceneMode mode)
        {
            CurrentScene.Clear();

            _currentSceneType = sceneType;
            SceneManager.LoadScene(GetSceneName(sceneType), mode);
        }

        public virtual string GetSceneName(SceneTypes sceneType)
        {
            return Enum.GetName(typeof(SceneTypes), sceneType);
        }

        public void AddSceneChangeAction(UnityAction<Scene, LoadSceneMode> action)
        {
            SceneManager.sceneLoaded -= action;
            SceneManager.sceneLoaded += action;
        }

        public void RemoveSceneChangeAction(UnityAction<Scene, LoadSceneMode> action)
        {
            SceneManager.sceneLoaded -= action;
        }

        public void UnloadSceneAsync(SceneTypes sceneType, UnloadSceneOptions options = UnloadSceneOptions.None)
        {
            SceneManager.UnloadSceneAsync(GetSceneName(sceneType), options);
        }

        void IManager.ClearManager()
        {
            CurrentScene.Clear();
        }

        void IManager.InitManager()
        {
        }
    }
}
