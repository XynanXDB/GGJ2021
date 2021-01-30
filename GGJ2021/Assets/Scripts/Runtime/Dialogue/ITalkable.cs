using System;

namespace Game.Runtime.Dialogue
{
    [Serializable]
    public struct FSpeakerInfo
    {
        public string Name;
        public string InternalIdentifier;
    }
    
    public interface ITalkable
    {
        FSpeakerInfo GetSpeakerInfo();
        void SendNativeCommand(string[] Data);
    }
}