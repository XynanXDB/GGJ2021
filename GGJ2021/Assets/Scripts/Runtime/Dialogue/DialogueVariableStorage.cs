using System.Collections.Generic;
using Game.Runtime.UI;
using Game.Runtime.Utility;
using UnityEngine;

namespace Game.Runtime.Dialogue
{
    public class DialogueVariableStorage : Yarn.Unity.InMemoryVariableStorage
    {
        void Start()
        {
            int[] Profile = UIManager.UUIManager.GFProfile;
            RetrieveChecklist();

            if (Profile != null && Profile.Length > 0)
            {
                SetValue(StringConstants.Clothes, Profile[0]);
                SetValue(StringConstants.Smell, Profile[1]);
                SetValue(StringConstants.Gift, Profile[2]);
            }

            DialogueManager.UDialogueManager.StartDialogue("Ending");
        }

        public void RetrieveChecklist()
        {
            List<string> Checklist = UIManager.UUIManager.GFChecklist;
                        
            foreach (string Names in Checklist)
            {
                Debug.Log(Names);
                switch (Names)
                {
                    case StringConstants.Phone:
                        SetValue(StringConstants.Phone, 1);
                        break;
                    
                    case StringConstants.HouseKeys:
                        SetValue(StringConstants.HouseKeys, 1);
                        break;
                    
                    case StringConstants.Wallet:
                        SetValue(StringConstants.Wallet, 1);
                        break;
                    
                    case StringConstants.ClothesFormal:
                        SetValue(StringConstants.ClothesFormal, 1);
                        break;
                    
                    case StringConstants.ClothesSmart:
                        SetValue(StringConstants.ClothesSmart, 1);
                        break;
                    
                    case StringConstants.ClothesCasual:
                        SetValue(StringConstants.ClothesCasual, 1);
                        break;
                    
                    case StringConstants.SmellShower:
                        SetValue(StringConstants.SmellShower, 1);
                        break;

                    case StringConstants.SmellDeodorant:
                        SetValue(StringConstants.SmellDeodorant, 1);
                        break;
                    
                    case StringConstants.SmellPerfume:
                        SetValue(StringConstants.SmellPerfume, 1);
                        break;
                    
                    case StringConstants.GiftBear:
                        SetValue(StringConstants.GiftBear, 1);
                        break;
                    
                    case StringConstants.GiftBook:
                        SetValue(StringConstants.GiftBook, 1);
                        break;
                    
                    case StringConstants.GiftChocolate:
                        SetValue(StringConstants.GiftChocolate, 1);
                        break;
                    
                    case StringConstants.ComeEarly:
                        SetValue(StringConstants.ComeEarly, 1);
                        break;
                }
            }
        }
    }
}