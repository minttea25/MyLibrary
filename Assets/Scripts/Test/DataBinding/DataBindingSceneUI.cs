using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
class DataBindingSceneUIContext : UIContext
{
    public UIObject<TextMeshProUGUI> TestText = new();
    public UIObject<TextMeshProUGUI> TestText2 = new();
    public UIObject<Button> TestButton = new();
    public UIObject<Button> ChangeButton = new();
    public UIObject<Slider> TestSlider = new();
    public UIObject<Toggle> PopupToggle = new();
    public UIObject<Image> TestImage = new();
    public UIObject<RawImage> TestRawImage = new();
    public UIObject<DataBindingComponent, TestData> TestObject = new();
}

public class DataBindingSceneUI : BaseUIScene
{
    [SerializeField]
    DataBindingSceneUIContext Context = new();

    const string PopupKey = "DBPopup";
    const string TestTexture2DKey = "DBTestTexture";

    DataBindingPopup popup;

    readonly Property<string, TextMeshProUGUI> playerName = new();
    readonly Property<Texture2D, RawImage> testRawImage = new(null);
    readonly Property<float, Slider> testSliderValue = new(0f);
    readonly CustomProperty<TestData, DataBindingComponent> testData = new(new TestData() { id = 0 });

    public override void Init()
    {
        base.Init();

        // add uis at property
        playerName.AddUI(Context.TestText, Context.TestText2);
        testRawImage.AddUI(Context.TestRawImage);
        testSliderValue.AddUI(Context.TestSlider);
        testData.AddUI(Context.TestObject);
    }

    private void Start()
    {
        // get component
        Context.TestButton.Component.onClick.AddListener(() => { Debug.Log("TestButtonClicked"); });
        Context.ChangeButton.Component.onClick.AddListener(OnChangeButtonClicked);
        Context.PopupToggle.Component.onValueChanged.AddListener(OpenPopup);
    }

    void OnChangeButtonClicked()
    {
        // chagne properties => update ui
        ManagerCore.Resource.LoadTexture2DAsync(TestTexture2DKey, (t) => testRawImage.Data = t);
        playerName.Data = "New Player Name";
        testData.Data = new() { id = UnityEngine.Random.Range(0, 10) };
        testSliderValue.Data = UnityEngine.Random.value;

        Context.TestImage.Component.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    void OpenPopup(bool open)
    {
        if (popup == null)
        {
            ManagerCore.UI.ShowPopupUIAsync<DataBindingPopup>(PopupKey, false,
                p =>
                {
                    popup = p;
                    popup.SetText(UnityEngine.Random.Range(0, 1000).ToString("000"));
                    popup.Show();
                });
            return;
        }

        if (open)
        {
            popup.SetText(UnityEngine.Random.Range(0, 1000).ToString("000"));
            popup.Show();
        }
        else
        {
            popup.Hide();
        }
    }


#if UNITY_EDITOR
    public override Type GetContextType()
    {
        return typeof(DataBindingSceneUIContext);
    }

    protected override object GetContext()
    {
        return Context;
    }
#endif
}
