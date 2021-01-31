using Game.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Game.Runtime.Dialogue
{
    public class DialogueSkipper : MonoBehaviour
    {
        [SerializeField] protected DialogueUI DialogueUI;
        [SerializeField] protected Image Visuals;
        [SerializeField] protected Button Button;
        
        void Awake()
        {
            DialogueUI.onLineStart.AddListener(OnLineStart);
            DialogueUI.onLineFinishDisplaying.AddListener(OnLineFinishDisplaying);
            DialogueUI.onDialogueEnd.AddListener(OnDialogueEnd);
            Button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            if (Visuals.enabled)
                DialogueUI.MarkLineComplete();
            else
                DialogueUI.textSpeed = 0.0001f;
        }

        void OnDialogueEnd()
        {
            UIManager.UUIManager.ForceFocusGameObject(null);
            Visuals.enabled = false;
        }

        void OnLineFinishDisplaying() => Visuals.enabled = true;

        void OnLineStart()
        {
            UIManager.UUIManager.ForceFocusGameObject(gameObject);
            Visuals.enabled = false;
            DialogueUI.textSpeed = 0.025f;
        }
    }
}