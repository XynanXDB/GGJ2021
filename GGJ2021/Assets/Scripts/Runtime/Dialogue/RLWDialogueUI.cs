using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Game.Runtime.Dialogue
{
    public class RLWDialogueUI : DialogueUI
    {
        [SerializeField] protected TMP_Text SpeechText;
        [SerializeField] protected DialogueSpeakerVisuals SpeakerVisuals;
        [SerializeField] protected GameObject Speaker1;
        
        void Start()
        {
            onLineUpdate.AddListener(UpdateSpeechText);
                
            ITalkable Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ITalkable>();
            Player.SendNativeCommand(new []{Player.GetSpeakerInfo().InternalIdentifier, "InputModeUI"});
            
            SpeakerVisuals.SetupReference(Speaker1.GetComponent<ITalkable>());
        }

        void UpdateSpeechText(string Texts)
        {
            SpeechText.text = Texts;
        }
    }
}