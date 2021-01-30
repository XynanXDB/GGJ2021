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
        public RLWDialogueUI DialogueUI;
        
        private OneParamSignature<YarnCommandPacket> OnReceiveSetSpeaker;
        
        void Awake()
        {
            if (_DialogueManager == null)
                _DialogueManager = this;
            
            DialogueRunner.AddCommandHandler("SetSpeaker", SetSpeaker);
            DialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
            OnReceiveSetSpeaker += DialogueUI.OnSetSpeaker;
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

        public void StartDialogue(string YarnAssetName, string StartNodeName = "Start")
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