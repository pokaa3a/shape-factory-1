using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public partial class Module
{
    public Vector2Int rc;
    public GameObject gameObject;
    public ModuleConfig config;
    public bool inMap = false;
    private ModuleComponent component;
}

public partial class Module
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

    private bool _enabled;
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

public partial class Module
{
    public class ModuleComponent : MonoBehaviour,
        IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
    {
        public Module module;
        private bool onDragging = false;

        //Detect current clicks on the GameObject (the one with the script attached)
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            //Output the name of the GameObject that is being clicked
            // Debug.Log("Game Object Click in Progress");
        }

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            if (!onDragging)
            {
                // Simply click down and up without dragging
                module.ClockwiseRotate();
                return;
            }

            // Debug.Log("No longer being clicked");
            if (Map.InsideMap(module.xy))
            {
                module.config.inMap = true;
                Vector2Int rc = Map.XYtoRC(module.xy);
                Map.Instance.GetTile(rc).AddModuleToTile(module);
            }
            else
            {
                Destroy(gameObject);
            }
            BullpenManager.Instance.ReleaseModule(module);
            onDragging = false;
        }

        public void OnBeginDrag(PointerEventData data)
        {
            // Debug.Log("OnBeginDrag");
            // DraggableModule draggableModule = new DraggableModule();

            // Vector3 worldPosition = Camera.main.ScreenToWorldPoint(data.position);
            // draggableModule.xy = worldPosition;

            // data.pointerDrag = draggableModule.gameObject;
            // data.pointerPress = draggableModule.gameObject;
            onDragging = true;
            module.BeginDragging();
            Vector2Int rc = Map.XYtoRC(module.xy);
            Map.Instance.GetTile(rc).RemoveModuleFromTile();
        }

        // According to Unity's manual, IDragHandler needs to be implemented
        // in addition to IBeginDragHandler.
        public void OnDrag(PointerEventData data)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(data.position);
            module.xy = worldPosition;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}

public partial class Module
{
    public Module(ModuleConfig config)
    {
        gameObject = new GameObject("Module");
        this.inMap = config.inMap;
        if (this.inMap)
        {
            this.rc = config.rc;
            Map.Instance.GetTile(rc).AddModuleToTile(this);
        }

        component = gameObject.AddComponent<ModuleComponent>();
        component.module = this;
        BoxCollider2D boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = Map.tileWH;
    }

    public void Destroy()
    {
        this.component.Destroy();
    }

    public virtual CarrierTodo AcknowledgeModule(ElementCarrier carrier)
    {
        return CarrierTodo.Destroy;
    }

    public virtual void BeginDragging() { }
    public virtual void LandInMap()
    {
        this.inMap = true;
    }
    public virtual void ClockwiseRotate() { }
}