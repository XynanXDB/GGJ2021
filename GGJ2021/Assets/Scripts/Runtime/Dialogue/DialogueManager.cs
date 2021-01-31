using Game.Runtime.Utility;
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
        public RLWDialogueUI DialogueUI;
        
        private OneParamSignature<YarnCommandPacket> OnReceiveSetSpeaker;
        
        void Awake()
        {
            if (_DialogueManager == null)
                _DialogueManager = this;
            
            DialogueRunner.onDialogueComplete.AddListener(OnDialogueEnd);
        }

        void OnDialogueEnd()
        {
            DialogueRunner.Clear();
        }
        
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

        public float GetFinalScore() 
            => DialogueRunner.variableStorage.GetValue(StringConstants.LovePoint).AsNumber / 7.0f;
    }
}