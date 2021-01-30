using System.Collections.Generic;

namespace Game.Runtime.Dialogue
{
    public class DialogueVariableStorage : Yarn.Unity.InMemoryVariableStorage
    {
        private const string Phone = "Phone";
        private const string HouseKeys = "HouseKeys";
        private const string Wallet = "Wallet";
        
        private const string ClothesFormal = "ClothesFormal";
        private const string ClothesSmart = "ClothesSmart";
        private const string ClothesCasual = "ClothesCasual";
        
        private const string SmellShower = "SmellShower";
        private const string SmellDeodorant = "SmellDeodorant";
        private const string SmellPerfume = "SmellPerfume";
        
        private const string GiftBear = "GiftBear";
        private const string GiftBook = "GiftBook";
        private const string GiftChocolate = "GiftChocolate";

        public void RetrieveChecklist()
        {
            List<string> Checklist = GameManager.m_instance.ExportListOfName();

            foreach (string Names in Checklist)
            {
                switch (Names)
                {
                    case Phone:
                        SetValue(Phone, true);
                        break;
                    
                    case HouseKeys:
                        SetValue(HouseKeys, true);
                        break;
                    
                    case Wallet:
                        SetValue(Wallet, true);
                        break;
                    
                    case ClothesFormal:
                        SetValue(ClothesFormal, true);
                        break;
                    
                    case ClothesSmart:
                        SetValue(ClothesSmart, true);
                        break;
                    
                    case ClothesCasual:
                        SetValue(ClothesCasual, true);
                        break;
                    
                    case SmellShower:
                        SetValue(SmellShower, true);
                        break;

                    case SmellDeodorant:
                        SetValue(SmellDeodorant, true);
                        break;
                    
                    case SmellPerfume:
                        SetValue(SmellPerfume, true);
                        break;
                    
                    case GiftBear:
                        SetValue(GiftBear, true);
                        break;
                    
                    case GiftBook:
                        SetValue(GiftBook, true);
                        break;
                    
                    case GiftChocolate:
                        SetValue(GiftChocolate, true);
                        break;
                }
            }
        }
    }
}