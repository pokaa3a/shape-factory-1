using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GamePlayPanel
{
    UISelectLevelButton selectLevelButton;
    UISaveLevelButton saveLevelButton;
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
        // Level Selection Button
        selectLevelButton = new UISelectLevelButton();

        // Save Level Button
        saveLevelButton = new UISaveLevelButton();

        // Module selection panel
        InitializeModuleSelectionPanel();

        // Show bullpen
        UIImage bullpen = new UIImage("Bullpen");
        bullpen.xy = new Vector2(0, -2f);
        bullpen.SetImage(SpritePath.UI.bullpen);
        bullpen.SetSize(new Vector2(500, 100));
    }

    private void InitializeModuleSelectionPanel()
    {
        int rows = 2;
        int cols = 4;
        Vector2 center = new Vector2(0, -3f);

        float len = 0.6f;
        Vector2 topLeft = center + new Vector2(-(cols - 1f) / 2f, (rows - 1f) / 2f) * len;

        // (0, 0): Source
        InitializeSingleModuleButton(Source.name, topLeft + new Vector2(0, 0) * len);

        // (0, 1): Turn
        InitializeSingleModuleButton(Turn.name, topLeft + new Vector2(1, 0) * len);

        // (0, 2): Merge
        InitializeSingleModuleButton(Merge.name, topLeft + new Vector2(2, 0) * len);

        // (0, 3): Split
        InitializeSingleModuleButton(Split.name, topLeft + new Vector2(3, 0) * len);

        // (1, 0): Paint
        InitializeSingleModuleButton(Paint.name, topLeft + new Vector2(0, -1) * len);

        // (1, 1): Rotate
        InitializeSingleModuleButton(Rotate.name, topLeft + new Vector2(1, -1) * len);

        // (1, 2): Grow
        InitializeSingleModuleButton(Grow.name, topLeft + new Vector2(2, -1) * len);

        // (1, 3): Overlay
        InitializeSingleModuleButton(Overlay.name, topLeft + new Vector2(3, -1) * len);
    }

    private void InitializeSingleModuleButton(string name, Vector2 xy)
    {
        UIButton moduleButton = new UIButton($"Module_{name}");
        Action clickCallback = null;
        switch (name)
        {
            case Source.name:
                moduleButton.SetImage(SpritePath.Module.source);
                GameObject element = new GameObject("Element");
                Utils.SetParent(element, moduleButton.gameObject);
                Utils.SetImage(element, SpritePath.Element.Circle.white);
                element.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);

                clickCallback = () => { BullpenManager.Instance.SelectModule(Source.name); };
                break;
            case Turn.name:
                moduleButton.SetImage(SpritePath.Module.turnLeft);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Turn.name); };
                break;
            case Merge.name:
                moduleButton.SetImage(SpritePath.Module.merge);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Merge.name); };
                break;
            case Split.name:
                moduleButton.SetImage(SpritePath.Module.split);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Split.name); };
                break;
            case Paint.name:
                moduleButton.SetImage(SpritePath.Module.paintRed);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Paint.name); };
                break;
            case Rotate.name:
                moduleButton.SetImage(SpritePath.Module.rotateClockwise);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Rotate.name); };
                break;
            case Grow.name:
                moduleButton.SetImage(SpritePath.Module.grow);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Grow.name); };
                break;
            case Overlay.name:
                moduleButton.SetImage(SpritePath.Module.overlay);
                clickCallback = () => { BullpenManager.Instance.SelectModule(Overlay.name); };
                break;
            default:
                clickCallback = null;
                break;
        }
        moduleButton.clickCallBack = clickCallback;
        moduleButton.SetSize(new Vector2(135, 135));    // TODO: Screen size adjustable
        moduleButton.xy = xy;
    }
}