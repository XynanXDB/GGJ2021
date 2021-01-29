using UnityEngine;

namespace Game.Runtime.Dialogue
{
    [System.Serializable]
    public struct FSpeakerInfo
    {
        public string Name;
        public Transform SpeechTransform;
    }
    
    public interface ITalkable
    {
        FSpeakerInfo GetSpeakerInfo();
    }
}