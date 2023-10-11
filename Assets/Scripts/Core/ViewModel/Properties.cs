using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

public enum ObjectType
{
    // 우선 순위 주의
    Common = 0,
    CUSTOM = 1,
    TextMeshProUGUI,
    Slider,
    Image,
    Button,
    Toggle,
    HorizontalLayoutGroup,
    VerticalLayoutGroup,
    GridlayoutGroup
}

[System.Serializable]
public class Property
{
    public GameObject BindObject = null;
    public ObjectType BindObjectType;
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">data type for data-binding (ex - string, int, UnityAction ...)</typeparam>
[System.Serializable]
public class Property<T>
{
    T _value;
    public T Value
    {
        get => _value;
        set { _value = value; Notify(); }
    }

    /// <summary>
    /// DO NOT CHANGE THE NAME; It is relevant  from the System.Reflection. See: Const.BindObjectFieldName.
    /// </summary>
    public GameObject BindObject = null;
    public ObjectType BindObjectType;

    public void Notify()
    {
        if (BindObject == null) return;

        // TODO - Map 이용

        if (BindObjectType == ObjectType.TextMeshProUGUI)
        {
            if (BindObject.TryGetComponent<TextMeshProUGUI>(out var com))
            {
                com.text = _value.ToString();
            }
            else
            {
                Debug.LogWarning($"{BindObject.name} has no component=TextMeshProUGUI. The changes of value was not applied.");
            }
        }
        else if (BindObjectType == ObjectType.Image)
        {
            if (BindObject.TryGetComponent<Image>(out var img))
            {
                img.sprite = _value as Sprite;
            }
            else
            {
                Debug.LogWarning($"{BindObject.name} has no component=Image. The changes of value was not applied.");
            }
        }
        else if (BindObjectType == ObjectType.Button)
        {
            if (BindObject.TryGetComponent<Button>(out var btn))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(_value as UnityAction);
            }
            else
            {
                Debug.LogWarning($"{BindObject.name} has no component=Button. The changes of value was not applied.");
            }
        }
        else if (BindObjectType == ObjectType.Toggle)
        {
            if (BindObject.TryGetComponent<Toggle>(out var toggle))
            {
                toggle.onValueChanged.AddListener(_value as UnityAction<bool>);
            }
            else
            {
                Debug.LogWarning($"{BindObject.name} has no component=Toggle. The changes of value was not applied.");
            }
        }
        else if (BindObjectType == ObjectType.Slider)
        {
            if (BindObject.TryGetComponent<Slider>(out var slider))
            {
                slider.value = (_value as Wrapper<float>).value;
            }
            else
            {
                Debug.LogWarning($"{BindObject.name} has no component=Slider. The changes of value was not applied.");
            }
        }

        // TODO
    }
}


/// <summary>
/// It is for CUSTOM type with the field name.
/// </summary>
/// <typeparam name="T">A type for data-binding</typeparam>
/// <typeparam name="FieldType">The field type of the value in T</typeparam>
[System.Serializable]
public class Property<T, CustomType> where CustomType : Component, ICustomProperty<T>
{
    public T Value
    {
        get => BindObject.GetComponent<CustomType>().GetData();
        set => BindObject.GetComponent<CustomType>().SetData(value);
    }

    /// <summary>
    /// DO NOT CHANGE THE NAME; It is relevant  from the System.Reflection. See: Const.BindObjectFieldName.
    /// </summary>
    public GameObject BindObject = null;
    public ObjectType BindObjectType = ObjectType.CUSTOM;
    public CustomType Component => BindObject.GetComponent<CustomType>();
}

public class Wrapper<T>
{
    public Wrapper(T value) { this.value = value; }

    public T value;
}

public interface ICustomProperty<T>
{
    public void SetData(T value);
    public T GetData();
}