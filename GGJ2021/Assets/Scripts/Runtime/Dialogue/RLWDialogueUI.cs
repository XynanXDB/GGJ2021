using TMPro;
using UnityEngine;
using Yarn.Unity;

namespace Game.Runtime.Dialogue
{
    public class RLWDialogueUI : DialogueUI
    {
        [SerializeField] protected TMP_Text SpeechText;
        [SerializeField] protected GameObject Speaker1Visuals;
        [SerializeField] protected GameObject Speaker2Visuals;
        
        private ITalkable Speaker1;
        private ITalkable Speaker2;

        private FSpeakerInfo Speaker1Info;
        private FSpeakerInfo Speaker2Info;
        
        void Start()
        {
            onLineUpdate.AddListener(UpdateSpeechText);
            onDialogueEnd.AddListener(OnDialogueEnd);
                
            Speaker1 = GameObject.FindGameObjectWithTag("Player").GetComponent<ITalkable>();
            Speaker1Info = Speaker1.GetSpeakerInfo();
            
            Speaker1.SendNativeCommand(new []{Speaker1Info.InternalIdentifier, "InputModeUI"});
        }

        void OnDialogueEnd()
        {
            ToggleSpeaker1Visuals(false);
            ToggleSpeaker2Visuals(false);
        }

        void UpdateSpeechText(string Texts)
        {
            SpeechText.text = Texts;
        }

        public void JoinConversation(ITalkable InSpeaker2)
        {
            Speaker2 = InSpeaker2;
            Speaker2Info = Speaker2.GetSpeakerInfo();
        }

        public void OnSetSpeaker(YarnCommandPacket Packet)
        {
            if (Packet.Name == Speaker1Info.InternalIdentifier)
            {
                ToggleSpeaker1Visuals(true);
                ToggleSpeaker2Visuals(false);
            }
            else
            {
                ToggleSpeaker2Visuals(true);
                ToggleSpeaker1Visuals(false);
            }
        }

        void ToggleSpeaker1Visuals(bool State) => Speaker1Visuals.SetActive(State);

        void ToggleSpeaker2Visuals(bool State) => Speaker2Visuals.SetActive(State);
    }
}