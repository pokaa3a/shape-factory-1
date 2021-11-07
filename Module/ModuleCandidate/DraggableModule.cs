#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class DraggableModule
{
    public GameObject gameObject;
}

public partial class DraggableModule
{
    private Vector2 _xy;
    public Vector2 xy
    {
        get => _xy;
        set
        {
            _xy = value;
            gameObject.transform.position = _xy;
        }
    }
}

public partial class DraggableModule
{
    public class Component : MonoBehaviour, IPointerUpHandler, IDragHandler
    {
        public DraggableModule module;

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            Debug.Log($"DraggleModule Up: {Map.UVtoXY(pointerEventData.position)}");
            Vector2 xy = Map.UVtoXY(pointerEventData.position);
            if (Map.InsideMap(xy))
            {
                ModuleConfig config = ModuleConfig.MakeMergeConfig(
                    Map.XYtoRC(xy), Direction.Up);
                Merge newModule = new Merge(config);
            }
            Destroy(gameObject);
        }

        public void OnDrag(PointerEventData data)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(data.position);
            module.xy = worldPosition;
        }
    }
}

public partial class DraggableModule
{
    public DraggableModule(ModuleConfig config)
    {
        gameObject = new GameObject("DraggableModule");
        Component component = gameObject.AddComponent<Component>();
        component.module = this;

        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = Map.tileWH;

        // Set properties according to config
        switch (config.name)
        {
            case Source.name:

            case Merge.name:

            case Turn.name:

            case Rotate.name:

            case Split.name:

            case Paint.name:

            case Grow.name:

            case Overlay.name:

            default:
        }


        Utils.SetSpriteSortingOrder(gameObject, 5);
    }
}
#endif