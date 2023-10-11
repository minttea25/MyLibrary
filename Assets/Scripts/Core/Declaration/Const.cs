using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Const
    {
        public const int INVALID = -1;

        public const int DefaultPoolObjectCount = 3;

        #region For DontDestoyedOnLoad Object Names

        public const string ManagerName = "!Managers";
        public const string AudioName = "!Sound";
        public const string AudioSourceName = "!SoundSource";

        #endregion

        public const string RootUIName = "@RootUI";
        public const string AddressableEventSystemKey = "EventSystems";
        public const string EventSystemsName = "@EventSystems";

        public const string BindObjectFieldName = "BindObject";
        public const string BindObjectTypeFieldName = "BindObjectType";
        public const string UIContextFieldName = "Context";

        public const string ContentObjectTag = "UIContent";
        public const string ContentObjectName = "Content";
        public const string ContentObjectFieldName = "ContentObject";


        readonly static public int DefaultAudioSourceCount = Enum.GetValues(typeof(AudioType)).Length;
    }
}
