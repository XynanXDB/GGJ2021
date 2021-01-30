using System;
using System.Collections.Generic;
using Game.Utility;
using UnityEngine;
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
        private List<ITalkable> JoinedSpeakers;
        
        void Awake()
        {
            if (_DialogueManager == null)
                _DialogueManager = this;

            JoinedSpeakers = new List<ITalkable>();
            
            DialogueRunner.AddCommandHandler("SetSpeaker", SetSpeaker);
            
            
            DialogueUI.onDialogueEnd.AddListener(OnDialogueEnd);
        }

        void OnDialogueEnd()
        {
            DialogueRunner.Clear();
            JoinedSpeakers?.Clear();
        }

        void OnDestroy() => OnReceiveSetSpeaker = null;

        public void InitiateDialogue(string YarnAssetName, List<ITalkable> InSpeakers = null,
            string StartNodeName = "Start")
        {
            JoinedSpeakers = InSpeakers;

            if (InSpeakers != null)
            {
                foreach (ITalkable I in InSpeakers)
                    JoinConversation(I);
            }
            
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