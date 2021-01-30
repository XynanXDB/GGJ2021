using Game.Runtime.Dialogue;
using UnityEngine;

namespace Game.Runtime.Girl
{
    public class NPC : MonoBehaviour, ITalkable
    {
        [SerializeField] protected FSpeakerInfo SpeakerInfo = new FSpeakerInfo()
        {
            Name = "Jane",
            InternalIdentifier = "Girl"
        };
        
        public FSpeakerInfo GetSpeakerInfo() => SpeakerInfo;

        public void SendNativeCommand(string[] Data)
        { }
    }
}