using Core;
using System;
using TMPro;
using UnityEngine;

public struct TestData
{
    public int id;

    public override string ToString()
    {
        return $"id: {id}";
    }
}

[Serializable]
class TestComponentContext : UIContext
{
    public UIObject<TextMeshProUGUI> IdText = new();
}

public class TestComponent : BaseUI, IDBCustomComponent<TestData>
{
    [SerializeField]
    TestComponentContext Context = new();


    public void UpdateUI(TestData data)
    {
        Context.IdText.Component.text = $"{data}";
        Debug.Log("Updated UI in TestComponent");

    }

#if UNITY_EDITOR
    public override Type GetContextType()
    {
        return typeof(TestComponentContext);
    }

    protected override object GetContext()
    {
        return Context;
    }
#endif
}
