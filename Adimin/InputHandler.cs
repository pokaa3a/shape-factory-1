#if false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public partial class InputHandler
{
    public GameObject gameObject;
}

public partial class InputHandler
{
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

public partial class InputHandler
{
    private static InputHandler _instance;
    public static InputHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }
}

public partial class InputHandler
{
    public class Component : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        //Detect current clicks on the GameObject (the one with the script attached)
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            //Output the name of the GameObject that is being clicked
            Debug.Log("Game Object Click in Progress");
        }

        //Detect if clicks are no longer registering
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            Debug.Log("No longer being clicked");
        }
    }
}

public partial class InputHandler
{
    private InputHandler()
    {
        gameObject = new GameObject("InputHandler");
        gameObject.AddComponent<Component>();

        GameObject canvas = GameObject.Find(ObjectPath.canvas);
        Assert.IsNotNull(canvas);
        Utils.SetParent(gameObject, canvas);
        RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
        // Stretch
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = new Vector2(1, 1);
    }
}

#endif