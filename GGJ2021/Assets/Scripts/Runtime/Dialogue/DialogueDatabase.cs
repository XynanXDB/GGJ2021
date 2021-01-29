using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Dialogue
{
    [System.Serializable]
    public struct DialogueDBStruct
    {
        public string Key;
        public YarnProgram YarnAsset;
    }

    [CreateAssetMenu(fileName = "DialogueDB", menuName = "ScriptableObjects/Create Dialogue DB", order = 1)]
    public class DialogueDatabase : ScriptableObject
    {
        [SerializeField] private List<DialogueDBStruct> DialogueDB;

        public YarnProgram GetYarnAssetByKey(string Key) 
            => DialogueDB.Find(Item => Item.Key == Key).YarnAsset;
    }
}