using System.Collections;
using UnityEngine;
using Core;
using System;
using UnityEngine.UI;
using TMPro;


[Serializable]
public class SamplePopupContext : UIContext
{
    public UIObject<TextMeshProUGUI> TestText = new();
    public UIObject<TextMeshProUGUI> TestText2 = new();
    public UIObject<Button> TestButton = new();
    public UIObject<Slider> TestSlider = new();
    public UIObject<Toggle> TestToggle = new();
    public UIObject<Image> TestImage = new();
    public UIObject<RawImage> TestRawImage = new();
    public UIObject<TestComponent, TestData> TestObject = new();

}

public class SamplePopup : BaseUIPopup
{
    [SerializeField]
    SamplePopupContext Context = new();

    Property<string, TextMeshProUGUI> playerName = new();
    Property<Texture2D, RawImage> testRawImage = new(null);
    Property<float, Slider> testSliderValue = new(0f);
    CustomProperty<TestData, TestComponent> testData = new(new TestData() { id = 0 });

    private void Awake()
    {
        // add uis at property
        playerName.AddUI(Context.TestText, Context.TestText2);
        testRawImage.AddUI(Context.TestRawImage);
        testSliderValue.AddUI(Context.TestSlider);
        testData.AddUI(Context.TestObject);
    }

    private void Start()
    {
        // get component
        Context.TestButton.Component.onClick.AddListener(() => { Debug.Log("TestButton"); });
        Context.TestToggle.Component.onValueChanged.AddListener((v) => { Debug.Log($"TestToggle: {v}"); });
        Context.TestImage.Component.color = Color.blue;

        StartCoroutine(nameof(Change));
    }

    IEnumerator Change()
    {
        yield return new WaitForSeconds(2);

        // chagne properties => update ui
        testRawImage.Data = Resources.Load<Texture2D>("TestImage");
        playerName.Data = "New Player Name";
        testData.Data = new() { id = 10 };
        testSliderValue.Data = 0.5f;
    }

#if UNITY_EDITOR
    public override Type GetContextType()
    {
        return typeof(SamplePopupContext);
    }

    protected override object GetContext()
    {
        return Context;
    }
#endif
}
