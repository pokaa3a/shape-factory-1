using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UISelectLevelButton : UIButton
{

}

public partial class UISelectLevelButton : UIButton
{
    public UISelectLevelButton() : base("SelectLevelButton")
    {
        SetImage(SpritePath.UI.selectLevel);
        SetSize(new Vector2(200, 200));
        this.xy = new Vector2(0, -4f);
    }

    public override void Click()
    {
        LevelSelectionMenu.Instance.enabled = true;
    }
}