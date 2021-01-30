using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Game.Runtime.Dialogue
{
    public class RLWDialogueUI : DialogueUI
    {
        [SerializeField] protected TMP_Text SpeechText;
        void Start()
        {
            onLineUpdate.AddListener(UpdateSpeechText);
        }

        void UpdateSpeechText(string Texts)
        {
            SpeechText.text = Texts;
        }
    }
}