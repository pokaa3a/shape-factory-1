using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class LevelSelection
{
    public partial class LevelButton
    {
        public int num;
        private GameObject gameObject;
        private GameObject textObject;
    }
}

public partial class LevelSelection
{
    public partial class LevelButton
    {
        private GameObject levelId;

        public LevelButton(int num, int r, int c)
        {
            this.num = num;

            gameObject = new GameObject($"LevelButton_{num}");
            Utils.SetParent(gameObject, LevelSelection.Instance.lvSelection);
            gameObject.AddComponent<CanvasRenderer>();
            RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
            rectTransform.localPosition =
                LevelSelection.Instance.topLeft + new Vector2(
                        c * LevelSelection.Instance.buttonStep,
                        -r * LevelSelection.Instance.buttonStep);
            rectTransform.sizeDelta = new Vector2(
                LevelSelection.Instance.buttonStep * 0.6f,
                LevelSelection.Instance.buttonStep * 0.6f);
            gameObject.AddComponent<Image>().sprite =
                Resources.Load<Sprite>(SpritePath.UI.levelFrame);

            ButtonComponent component = gameObject.AddComponent<ButtonComponent>();
            component.levelButton = this;

            // Add number text
            AddTextObject();
        }

        private void AddTextObject()
        {
            textObject = new GameObject($"Number");
            Utils.SetParent(textObject, this.gameObject);
            textObject.AddComponent<CanvasRenderer>();
            RectTransform rectTransform = textObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(
                LevelSelection.Instance.buttonStep * 0.6f,
                LevelSelection.Instance.buttonStep * 0.6f);

            Text text = textObject.AddComponent<Text>();
            text.fontSize = 80;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = $"{this.num}";
            text.alignment = TextAnchor.MiddleCenter;

            Color textColor;
            ColorUtility.TryParseHtmlString("#CCCCCC", out textColor);
            text.color = textColor;
        }

        private void Select()
        {
            Debug.Log($"Select level {this.num}");
        }
    }
}

public partial class LevelSelection
{
    public partial class LevelButton
    {
        public class ButtonComponent : MonoBehaviour, IPointerClickHandler
        {
            public LevelButton levelButton;

            public void OnPointerClick(PointerEventData data)
            {
                levelButton.Select();
            }
        }
    }
}