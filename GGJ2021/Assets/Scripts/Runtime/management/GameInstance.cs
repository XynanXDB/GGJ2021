using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime.management
{
    public class GameInstance : MonoBehaviour
    {
        public static GameInstance UGameInstance;

        void Awake()
        {
            if (UGameInstance == null)
            {
                UGameInstance = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(this);
            
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene CurrentScene, LoadSceneMode Mode)
        {
            
        }

        public void LoadScene(string SceneName) 
            => SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}