using TMPro;
using UnityEngine;

namespace Game.Runtime.Dialogue
{
    public class DialogueSpeakerVisuals : MonoBehaviour
    {
        [SerializeField] protected TMP_Text NameText;

        private ITalkable Speaker;
        
        public void SetupReference(ITalkable InSpeaker)
        {
            Speaker = InSpeaker;
            NameText.text = InSpeaker.GetSpeakerInfo().Name;
        }
    }
}