
using Game.Runtime.management;
using UnityEngine;

public class LoadNewGame : MonoBehaviour
{
    public void GoToMainMenu()
    {
        GameInstance.UGameInstance.LoadScene("MainMenu");
    }
}
