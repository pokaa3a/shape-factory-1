using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CarrierTodo
{
    Reveal,
    Hide,
    Destroy
}

public partial class ElementRunner
{
    public bool firstFrame = false;
    public LinkedList<ElementCarrier> carriers = new LinkedList<ElementCarrier>();
    public float pos { get => component.pos; }
    private GameObject gameObject;
    private Component component;
}

public partial class ElementRunner
{
    private static ElementRunner _instance;
    public static ElementRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ElementRunner();
            }
            return _instance;
        }
    }
}

public partial class ElementRunner
{
    public class Component : MonoBehaviour
    {
        public float pos = 0f;

        void FixedUpdate()
        {
            pos += Time.deltaTime * Map.tileWH.x;
            if (pos > Map.tileWH.x)
            {
                pos -= Map.tileWH.x;
                ElementRunner.Instance.firstFrame = true;
            }
            else
            {
                ElementRunner.Instance.firstFrame = false;
            }

            ElementRunner.Instance.UpdateCarrierPositions(pos);
        }
    }
}

public partial class ElementRunner
{
    private ElementRunner()
    {
        gameObject = new GameObject("ElementRunner");
        component = gameObject.AddComponent<Component>();
    }

    public void UpdateCarrierPositions(float pos)
    {
        foreach (ElementCarrier carrier in carriers)
        {
            Vector2 xyFloor = new Vector2(
                Mathf.Floor(carrier.xy.x / Map.tileWH.x) * Map.tileWH.x,
                Mathf.Floor(carrier.xy.y / Map.tileWH.y) * Map.tileWH.y);

            if (carrier.direction == Direction.Up)
            {
                float newY = xyFloor.y + pos +
                    (carrier.xy.y > xyFloor.y + pos ? Map.tileWH.y : 0);
                carrier.xy = new Vector2(carrier.xy.x, newY);
            }
            else if (carrier.direction == Direction.Down)
            {
                float newY = xyFloor.y + Map.tileWH.y - pos +
                    (carrier.xy.y < xyFloor.y + Map.tileWH.y - pos ? -Map.tileWH.y : 0);
                carrier.xy = new Vector2(carrier.xy.x, newY);
            }
            else if (carrier.direction == Direction.Left)
            {
                float newX = xyFloor.x + Map.tileWH.x - pos +
                    (carrier.xy.x < xyFloor.x + Map.tileWH.x - pos ? -Map.tileWH.x : 0);
                carrier.xy = new Vector2(newX, carrier.xy.y);
            }
            else if (carrier.direction == Direction.Right)
            {
                float newX = xyFloor.x + pos +
                    (carrier.xy.x > xyFloor.x + pos ? Map.tileWH.x : 0);
                carrier.xy = new Vector2(newX, carrier.xy.y);
            }
        }
    }

    public void AddCarrier(ElementCarrier carrier)
    {
        carriers.AddLast(carrier);
    }

    public void RemoveCarrier(ElementCarrier carrier)
    {
        carriers.Remove(carrier);
    }
}