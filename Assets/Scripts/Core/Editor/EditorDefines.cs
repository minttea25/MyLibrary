using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorDefines
{
    #region Common
    public const int SpacePreference = 10;
    public const int InfoFontSize = 15;
    public const int HighlightFontSize = 13;

    public readonly static Color OkColor = new Color(0.1f, 0.9f, 0.2f);
    public readonly static Color WarningColor = new Color(1f, 0.8f, 0.1f);
    public readonly static Color ErrorColor = new Color(0.8f, 0.2f, 0.2f);

    #endregion

    #region BaseUIEditor
    public readonly static string FindAndAssignText = "Find And Assign UI Objects";


    #endregion



    public readonly static string NotFoundObjectsHeader = "Not Found Objects (UIObject Names)";
    public readonly static string DBRefInfoTitle_NotYet = "Push The Assign Button";
    public readonly static string DBRefInfoTitle_AllFound = "All objects are found.";
    public readonly static string DBRefInfoTitle_NotFound = "Some objects are not found.";
    public readonly static string DBRefInfoTitle_NeedCheck = "Properties have been added or changed";

}
