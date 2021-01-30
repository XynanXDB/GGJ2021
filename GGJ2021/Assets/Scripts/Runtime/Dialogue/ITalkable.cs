using System;

namespace Game.Runtime.Dialogue
{
    [Serializable]
    public struct FSpeakerInfo
    {
        public string Name;
    }
    
    public interface ITalkable
    {
        FSpeakerInfo GetSpeakerInfo();
    }
}