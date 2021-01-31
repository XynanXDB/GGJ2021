﻿using System.Collections.Generic;
using Game.Runtime.UI;
using Game.Runtime.Utility;
using Game.Utility;
using UnityEngine;

namespace Game.Runtime.Dialogue
{
    public class DialogueVariableStorage : Yarn.Unity.InMemoryVariableStorage
    {
        void Start()
        {
            int[] Profile = UIManager.UUIManager.GFProfile;
            
            SetValue(StringConstants.Clothes, Profile[0]);
            SetValue(StringConstants.Smell, Profile[1]);
            SetValue(StringConstants.Gift, Profile[2]);
            
            Debug.Log(Profile[0]);
            Debug.Log(Profile[1]);
            Debug.Log(Profile[2]);
        }

        public void RetrieveChecklist()
        {
            List<string> Checklist = GameManager.m_instance.ExportListOfName();

            foreach (string Names in Checklist)
            {
                switch (Names)
                {
                    case StringConstants.Phone:
                        SetValue(StringConstants.Phone, true);
                        break;
                    
                    case StringConstants.HouseKeys:
                        SetValue(StringConstants.HouseKeys, true);
                        break;
                    
                    case StringConstants.Wallet:
                        SetValue(StringConstants.Wallet, true);
                        break;
                    
                    case StringConstants.ClothesFormal:
                        SetValue(StringConstants.ClothesFormal, true);
                        break;
                    
                    case StringConstants.ClothesSmart:
                        SetValue(StringConstants.ClothesSmart, true);
                        break;
                    
                    case StringConstants.ClothesCasual:
                        SetValue(StringConstants.ClothesCasual, true);
                        break;
                    
                    case StringConstants.SmellShower:
                        SetValue(StringConstants.SmellShower, true);
                        break;

                    case StringConstants.SmellDeodorant:
                        SetValue(StringConstants.SmellDeodorant, true);
                        break;
                    
                    case StringConstants.SmellPerfume:
                        SetValue(StringConstants.SmellPerfume, true);
                        break;
                    
                    case StringConstants.GiftBear:
                        SetValue(StringConstants.GiftBear, true);
                        break;
                    
                    case StringConstants.GiftBook:
                        SetValue(StringConstants.GiftBook, true);
                        break;
                    
                    case StringConstants.GiftChocolate:
                        SetValue(StringConstants.GiftChocolate, true);
                        break;
                    
                    case StringConstants.ComeEarly:
                        SetValue(StringConstants.ComeEarly, true);
                        break;
                }
            }
        }
    }
}