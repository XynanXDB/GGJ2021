using Game.Runtime.management;
using Game.Runtime.Utility;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.Runtime.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] protected MainMenuButtonVisuals PlayButton;
        [SerializeField] protected MainMenuButtonVisuals CreditsButton;
        [SerializeField] protected MainMenuButtonVisuals ExitButton;

        [Header("Group Management")] 
        [SerializeField] protected GameObject ButtonGroup;
        [SerializeField] protected GameObject CreditsGroup;
        
        void Awake()
        {
            PlayButton.onClick.AddListener(OnClickPlay);
            CreditsButton.onClick.AddListener(OnClickCredits);
            ExitButton.onClick.AddListener(OnClickExit);
            
            ButtonGroup.SetActive(true);
            CreditsGroup.SetActive(false);
        }

        void OnClickExit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();    
            #endif
        }

        void OnClickCredits()
        {
            ButtonGroup.SetActive(false);
            CreditsGroup.SetActive(true);
        }

        void OnClickPlay()
        {
            GameInstance.UGameInstance.LoadScene(StringConstants.LevelMain);
        }

        public void OnClickCreditsBack()
        {
            ButtonGroup.SetActive(true);
            CreditsGroup.SetActive(false);
        }
    }
}