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
        [SerializeField] protected GameObject WinScene;
        [SerializeField] protected GameObject LoseScene;
        
        void Start()
        {
            onLineUpdate.AddListener(UpdateSpeechText);
            onDialogueEnd.AddListener(OnDialogueEnd);
                
            ITalkable Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ITalkable>();
            Player.SendNativeCommand(new []{Player.GetSpeakerInfo().InternalIdentifier, "InputModeUI"});
            
            SpeakerVisuals.SetupReference(Speaker1.GetComponent<ITalkable>());
        }

        void OnDialogueEnd()
        {
            float FinalScore = DialogueManager.UDialogueManager.GetFinalScore();
            
            if (FinalScore >= 0.35f)
                WinScene.SetActive(true);
            else
                LoseScene.SetActive(true);
            
            gameObject.SetActive(false);
        }

        void UpdateSpeechText(string Texts)
        {
            SpeechText.text = Texts;
        }
    }
}