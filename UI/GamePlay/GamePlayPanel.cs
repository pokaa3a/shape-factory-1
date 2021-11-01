using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GamePlayPanel
{
    UISelectLevelButton selectLevelButton;
}

public partial class GamePlayPanel
{
    private static GamePlayPanel _instance;
    public static GamePlayPanel Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GamePlayPanel();
            }
            return _instance;
        }
    }
}

public partial class GamePlayPanel
{
    private GamePlayPanel() { }

    public void ShowPanel()
    {
        selectLevelButton = new UISelectLevelButton();
    }
}