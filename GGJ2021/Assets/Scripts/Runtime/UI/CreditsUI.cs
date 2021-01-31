using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Runtime.UI
{
    public class CreditsUI : MonoBehaviour
    {
        [SerializeField] protected MainMenuButtonVisuals BackButton;
        [SerializeField] protected MainMenuUI ParentUI;

        private EventSystem E;
        
        void Awake()
        {
            E = EventSystem.current;
            BackButton.onClick.AddListener(ParentUI.OnClickCreditsBack);
        }

        void OnEnable() => E.SetSelectedGameObject(BackButton.gameObject);
        void OnDisable() => E.SetSelectedGameObject(E.firstSelectedGameObject);
    }
}