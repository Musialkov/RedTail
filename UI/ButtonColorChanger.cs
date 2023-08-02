using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace FoxRevenge.UI
{
    public class ButtonColorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] Color onHoverColor = Color.white;
        [SerializeField] Color onClickColor = Color.yellow;
        [SerializeField] Color inActiveColor;
        
        [Range(1.0f, 2.0f)]
        [SerializeField] float onHoverScale = 1.2f;
        [SerializeField] TextMeshProUGUI text;
        [SerializeField] Button button;

        private Color originalColor;

        private void Awake() 
        {
            if(!text)
            {
                text = GetComponentInChildren<TextMeshProUGUI>();
            }

            if(!button)
            {
                button = GetComponent<Button>();
            }

            originalColor = text.color;
        }

        private void OnEnable() 
        {
            ChangeColor(originalColor);
            ChangeScale(1.0f);
        }
        private void Start() 
        {
            button.onClick.AddListener(() => ChangeColor(onClickColor));
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeColor(onHoverColor);
            ChangeScale(onHoverScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeColor(originalColor);
            ChangeScale(1.0f);
        }

        public void ChangeColor(Color newColor)
        {
            if(!this.isActiveAndEnabled) text.color = inActiveColor;
            else text.color = newColor;
        }

        public void SetInactiveColor()
        {
            text.color = inActiveColor;
        }

        private void ChangeScale(float newScale)
        {
            Vector3 newScaleVector = new Vector3(newScale, newScale, newScale);
            text.rectTransform.localScale = newScaleVector;
        }
    }
}
