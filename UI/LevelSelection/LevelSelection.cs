using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public partial class LevelSelection
{
    // TODO: Load number of levels from file
    GameObject lvSelection;
    const int levelsPerRow = 4;
    const float margin = 0.25f;
    int numLevels = 10;
    float buttonStep;
    Vector2 topLeft;
}

public partial class LevelSelection
{
    private static LevelSelection _instance;
    public static LevelSelection Instance
    {
        get
        {
            if (_instance == null)
                _instance = new LevelSelection();
            return _instance;
        }
    }
}

public partial class LevelSelection
{
    private LevelSelection()
    {
        lvSelection = GameObject.Find(ObjectPath.levelSelection);
        Assert.IsNotNull(lvSelection);

        buttonStep = (Screen.width * (1f - margin)) / (levelsPerRow - 1);
        topLeft = new Vector2(
            -((float)levelsPerRow - 1f) / 2f * buttonStep,
            Screen.height / 2f * (1f - margin));
    }

    public void ShowSelections()
    {
        int rows = numLevels / levelsPerRow + (numLevels % levelsPerRow > 0 ? 1 : 0);
        int id = 1;
        for (int row = 0; row < rows; ++row)
        {
            for (int col = 0; col < levelsPerRow; ++col)
            {
                if (id <= numLevels)
                {
                    LevelButton button = new LevelButton(id++, row, col);
                }
            }
        }
    }
}