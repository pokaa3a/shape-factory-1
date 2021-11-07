#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class ModuleCandidate
{
    public GameObject gameObject;
    public Component component;
    public Vector2 initPos = Vector2.zero;
}

public partial class ModuleCandidate
{
    private Vector2 _xy;
    public Vector2 xy
    {
        get => Map.UVtoXYCentered(_uv);
        set { uv = Map.XYtoUVCentered(value); }
    }

    private Vector2 _uv;
    public Vector2 uv
    {
        get => _uv;
        set
        {
            _uv = value;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            if (rectTransform == null)
            {
                rectTransform = gameObject.AddComponent<RectTransform>();
            }
            rectTransform.localPosition = _uv;
        }
    }

    private bool _enabled = false;
    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;
            gameObject.SetActive(_enabled);
        }
    }
}

public partial class ModuleCandidate
{
    public class Component : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        public ModuleCandidate moduleCandidate;

        public void OnBeginDrag(PointerEventData data)
        {
            // Debug.Log("OnBeginDrag");
            // DraggableModule draggableModule = new DraggableModule();

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(data.position);
            draggableModule.xy = worldPosition;

            data.pointerDrag = draggableModule.gameObject;
            data.pointerPress = draggableModule.gameObject;
        }

        // According to Unity's manual, IDragHandler needs to be implemented
        // in addition to IBeginDragHandler.
        public void OnDrag(PointerEventData data) { }
    }
}

public partial class ModuleCandidate
{
    public ModuleCandidate(ModuleConfig config)
    {
        gameObject = new GameObject(config.name);
        component = gameObject.AddComponent<Component>();
        component.moduleCandidate = this;

        GameObject canvas = GameObject.Find(ObjectPath.canvas);
        Assert.IsNotNull(canvas);
        Utils.SetParent(gameObject, canvas);

        gameObject.AddComponent<RectTransform>();
        gameObject.AddComponent<Image>();

        // Set properties according to config
        switch (config.name)
        {
            case Source.name:
                this.SetImage(SpritePath.Module.source);
                GameObject element = new GameObject("Element");
                switch (config.elementType)
                {
                    case ElementType.Circle:
                        Utils.SetImage(element, SpritePath.Element.Circle.white); break;
                    case ElementType.Cross:
                        Utils.SetImage(element, SpritePath.Element.Cross.white); break;
                    case ElementType.Square:
                        Utils.SetImage(element, SpritePath.Element.Square.white); break;
                    case ElementType.Triangle:
                        Utils.SetImage(element, SpritePath.Element.Triangle.white); break;
                    default:
                        break;
                }
                Utils.SetParent(element, this.gameObject);
                element.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
                break;
            case Merge.name:
                this.SetImage(SpritePath.Module.merge);
                break;
            case Turn.name:
                if (config.turnPose == TurnPose.TurnLeft)
                    this.SetImage(SpritePath.Module.turnLeft);
                else
                    this.SetImage(SpritePath.Module.turnRight);
                break;
            case Rotate.name:
                if (config.rotatePose == RotatePose.ClockWise)
                    this.SetImage(SpritePath.Module.rotateClockwise);
                else
                    this.SetImage(SpritePath.Module.rotateCounterClockwise);
                break;
            case Split.name:
                this.SetImage(SpritePath.Module.split);
                break;
            case Paint.name:
                switch (config.paintColor)
                {
                    case PaintColor.Blue:
                        this.SetImage(SpritePath.Module.paintBlue); break;
                    case PaintColor.Yellow:
                        this.SetImage(SpritePath.Module.paintYellow); break;
                    case PaintColor.Red:
                        this.SetImage(SpritePath.Module.paintRed); break;
                    case PaintColor.White:
                        this.SetImage(SpritePath.Module.paintWhite); break;
                    default:
                        break;
                }
                break;
            case Grow.name:
                this.SetImage(SpritePath.Module.grow);
                break;
            case Overlay.name:
                this.SetImage(SpritePath.Module.overlay);
                break;
            default:
                break;
        }
    }

    public void SetImage(string path)
    {
        Image image = gameObject.GetComponent<Image>();
        if (image == null)
        {
            image = gameObject.AddComponent<Image>();
        }
        image.sprite = Resources.Load<Sprite>(path);
    }

    public void SetSize(Vector2 wh)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            rectTransform = gameObject.AddComponent<RectTransform>();
        }
        rectTransform.sizeDelta = wh;
    }
}

#endif