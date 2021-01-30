using System;
using System.Collections.Generic;
using Game.Utility;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

namespace Game.Runtime.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        private static DialogueManager _DialogueManager;

        public static DialogueManager UDialogueManager
        {
            get
            {
                if (_DialogueManager == null)
                    _DialogueManager = FindObjectOfType<DialogueManager>();

                return _DialogueManager;
            }
        }

        [SerializeField] protected DialogueDatabase DialogueDB;
        [SerializeField] protected DialogueRunner DialogueRunner;
        [SerializeField] protected DialogueUI DialogueUI;
        
        private OneParamSignature<YarnCommandPacket> OnReceiveSetSpeaker;
        private ITalkable Player;
        private ITalkable Speaker2;
        
        void Awake()
        {
            if (_DialogueManager == null)
                _DialogueManager = this;
            
            DialogueRunner.AddCommandHandler("SetSpeaker", SetSpeaker);
            
            DialogueUI.onDialogueEnd.AddListener(OnDialogueEnd);
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<ITalkable>();
            Player.SendNativeCommand(new []{"JB", "InputModeUI"});
        }

        void Start()
        {
            //StartDialogue("Test");
        }

        void OnDialogueEnd()
        {
            DialogueRunner.Clear();
        }

        void OnDestroy() => OnReceiveSetSpeaker = null;

        public void InitiateDialogue(string YarnAssetName, ITalkable InSpeaker = null,
            string StartNodeName = "Start")
        {
            Speaker2 = InSpeaker;
            
            if (InSpeaker != null)
                JoinConversation(InSpeaker);

            StartDialogue(YarnAssetName, StartNodeName);
        }
        
        void StartDialogue(string YarnAssetName, string StartNodeName = "Start")
        {
            YarnProgram YarnAsset = DialogueDB.GetYarnAssetByKey(YarnAssetName);
            if (YarnAsset != null)
            {
                DialogueRunner.Add(YarnAsset);
                DialogueRunner.StartDialogue(StartNodeName);
            }
            else
                Debug.LogError("YarnAsset not found");
        }

        void JoinConversation(ITalkable Speaker)
        {
            FSpeakerInfo SpeakerInfo = Speaker.GetSpeakerInfo();
            //TODO Add speaker to the UI.
            
            if (SpeakerInfo.Name == "JB")
                Speaker.SendNativeCommand(new []{"JB", "InputModeUI"});
        }

        #region CommandHandlers
        void SetSpeaker(string[] Params)
        {
            string[] Commands = {null, null};

            for (int i = 0; i < Params.Length; i++)
                Commands[i] = Params[i];
            
            OnReceiveSetSpeaker?.Invoke(new YarnCommandPacket(Commands[0], Commands[1]));
        }
        #endregion
    }
}