using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBindingScene : BaseScene
{
    DataBindingSceneUI sceneUI;

    const string SceneKey = "DBSceneUI";

    protected override void Init()
    {
        base.Init();

        // SceneType = SceneTypes.[];

        ManagerCore.UI.ShowSceneUIAsync<DataBindingSceneUI>(SceneKey);
    }
}
