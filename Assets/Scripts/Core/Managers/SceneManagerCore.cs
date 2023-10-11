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
        public SceneType _currentSceneType = SceneType.INVALID;

        public SceneType CurrentSceneType
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

        public void LoadScene(SceneType sceneType)
        {
            CurrentScene.Clear();

            _currentSceneType = sceneType;
            SceneManager.LoadScene(GetSceneName(sceneType));
        }

        public void LoadScene(SceneType sceneType, LoadSceneMode mode)
        {
            CurrentScene.Clear();

            _currentSceneType = sceneType;
            SceneManager.LoadScene(GetSceneName(sceneType), mode);
        }

        public virtual string GetSceneName(SceneType sceneType)
        {
            return Enum.GetName(typeof(SceneType), sceneType);
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

        public void UnloadSceneAsync(SceneType sceneType, UnloadSceneOptions options = UnloadSceneOptions.None)
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
