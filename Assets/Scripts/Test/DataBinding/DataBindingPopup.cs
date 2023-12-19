using System.Collections;
using UnityEngine;
using Core;
using System;
using UnityEngine.UI;
using TMPro;


[Serializable]
public class DataBindingPopupContext : UIContext
{
    public UIObject<TextMeshProUGUI> PopupText = new();
}

public class DataBindingPopup : BaseReusableUIPopup
{
    [SerializeField]
    DataBindingPopupContext Context = new();

    readonly Property<string, TextMeshProUGUI> popupText = new();

    public override void Init()
    {
        base.Init();

        popupText.AddUI(Context.PopupText);
    }

    public void SetText(string text)
    {
        popupText.Data = text;
    }


#if UNITY_EDITOR
    public override Type GetContextType()
    {
        return typeof(DataBindingPopupContext);
    }

    protected override object GetContext()
    {
        return Context;
    }
#endif
}
