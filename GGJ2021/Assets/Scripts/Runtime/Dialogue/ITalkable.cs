using System;
using UnityEngine;

namespace Game.Runtime.Dialogue
{
    [Serializable]
    public struct FSpeakerInfo
    {
        public string Name;
        
        [HideInInspector] public string InternalIdentifier;
    }
    
    public interface ITalkable
    {
        FSpeakerInfo GetSpeakerInfo();
        void SendNativeCommand(string[] Data);
    }
}