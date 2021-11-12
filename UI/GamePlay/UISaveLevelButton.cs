using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class UISaveLevelButton : UIButton
{

}

public partial class UISaveLevelButton : UIButton
{
    public UISaveLevelButton() : base("SaveLevel")
    {
        SetImage(SpritePath.UI.saveLevel);
        SetSize(new Vector2(200, 200));
        this.xy = new Vector2(1f, -4.3f);
    }

    public override void Click()
    {

    }
}
