using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Runtime.UI
{
    public class MainMenuButtonVisuals : Button
    {
        public Image Selector;
        public TMP_Text Text;

        private Color Selected;
        private Color Deselected;
        
        protected override void Awake()
        {
            base.Awake();
            
            #if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying)
                return;
            #endif

            Selected = new Color(0.17647f, 0.22352f, 0.14901f, 1.0f);
            Deselected = new Color(0.91372f, 0.22352f, 0.2549f, 1.0f);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            Selector.enabled = false;
            Text.color = Deselected;
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            Selector.enabled = true;
            Text.color = Selected;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);

            Selector.enabled = false;
            Text.color = Deselected;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            
            Selector.enabled = true;
            Text.color = Selected;
            
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            if (EventSystem.current.currentSelectedGameObject == gameObject) 
                return;
            
            Selector.enabled = false;
            Text.color = Deselected;
        }
    }
}
