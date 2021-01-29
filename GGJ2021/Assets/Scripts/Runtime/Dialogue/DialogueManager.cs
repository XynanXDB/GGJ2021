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

        void Awake()
        {
            DontDestroyOnLoad(this);

            if (_DialogueManager == null)
                _DialogueManager = this;
            else
                Destroy(_DialogueManager);
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
            {
                Debug.LogError("YarnAsset not found");
            }
        }
    }
}