﻿using System.Collections.Generic;
using Game.Runtime.Utility;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Runtime.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager UUIManager;
        private GameObject FocusedGO;

        public int[] GFProfile = new int[3];
        public List<string> GFChecklist;
        
        void Awake()
        {
            if (UUIManager == null)
            {
                UUIManager = this;
                DontDestroyOnLoad(this);
            }
            else
                Destroy(this);
        }

        void Update()
        {
            if (FocusedGO != null && EventSystem.current.currentSelectedGameObject != FocusedGO)
                EventSystem.current.SetSelectedGameObject(FocusedGO);
        }

        public void ForceFocusGameObject(GameObject GO)
        {
            FocusedGO = GO;
            
            if (FocusedGO != null)
                EventSystem.current.SetSelectedGameObject(FocusedGO);
        }

        public void PostInteractNotification(string ItemName)
        {
            
        }

    }
}