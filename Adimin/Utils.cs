using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static void SetSprite(GameObject obj, string spritePath)
    {
        SpriteRenderer sprRend = obj.GetComponent<SpriteRenderer>();
        if (sprRend == null)
            sprRend = obj.AddComponent<SpriteRenderer>();
        sprRend.sprite = Resources.Load<Sprite>(spritePath);
    }

    public static void SetSpriteSortingOrder(GameObject obj, int idx)
    {
        SpriteRenderer sprRend = obj.GetComponent<SpriteRenderer>();
        if (sprRend == null)
            sprRend = obj.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sprRend.sortingOrder = idx;
    }

    public static void SetScale(GameObject obj, Vector2 sizeWH)
    {
        SpriteRenderer sprRend = obj.GetComponent<SpriteRenderer>();
        if (sprRend == null)
            sprRend = obj.AddComponent<SpriteRenderer>();
        obj.transform.localScale = new Vector2(
            sizeWH.x / sprRend.size.x,
            sizeWH.y / sprRend.size.y);
    }

    public static void SetParent(GameObject child, GameObject parent)
    {
        child.transform.SetParent(parent.transform);
        child.transform.localPosition = Vector2.zero;
        child.transform.localScale = new Vector3(1, 1, 1);
    }
}
