using Game.Runtime.UI;
using UnityEngine;

namespace Game.Runtime.management
{
    public static class SystemInit
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void OnStartUp()
        {
            Application.targetFrameRate = 60;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void PostSceneLoad()
        {
            GameObject obj = GameObject.FindGameObjectWithTag("UIManager");
            if (obj == null)
                UIManager.UUIManager = CreateManager("UIManager").GetComponent<UIManager>();
        }

        static GameObject CreateManager(string ObjectName)
        {
            GameObject Prefab = Resources.Load("Managers/" + ObjectName) as GameObject;
            GameObject Instance = GameObject.Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            Instance.name = ObjectName;

            return (Prefab == null) ? null : Instance;
        }
    }
}