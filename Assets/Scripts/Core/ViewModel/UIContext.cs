using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

namespace Core
{
    public abstract class UIContext
    {
        
    }

    public class UIComponentMapper
    {
        readonly Dictionary<Type, ObjectType> componentTypeMap;
        public UIComponentMapper()
        {
            // Initialize the component type map
            componentTypeMap = new() 
            {
                { typeof(TextMeshProUGUI), ObjectType.TextMeshProUGUI },
                { typeof(Button), ObjectType.Button },
                { typeof(Slider), ObjectType.Slider },
                { typeof(Image), ObjectType.Image },
                { typeof(HorizontalLayoutGroup), ObjectType.HorizontalLayoutGroup},
                { typeof(VerticalLayoutGroup), ObjectType.VerticalLayoutGroup},
                { typeof(Toggle), ObjectType.Toggle},
                { typeof(GridLayoutGroup), ObjectType.GridlayoutGroup}
            };
        }

        public ObjectType GetObjectType(GameObject go)
        {
            ObjectType type = ObjectType.Common;

            foreach (var component in go.GetComponents<Component>())
            {
                if (componentTypeMap.TryGetValue(component.GetType(), out var uiType))
                {
                    type = (int)uiType > (int)type ? uiType : type;
                }
            }

            return type;
        }
    }
}

