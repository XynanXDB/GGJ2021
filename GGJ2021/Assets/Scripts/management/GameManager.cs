using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct InteractableObjects
    {
        public string ItemName;
        public GameObject ItemPrefab;
        public GameObject SpawnedObj;
        public bool canHold;
        public bool disableOnUse;
        public string ItemRemove;
        public List<Transform> spawnLocale;
        public Transform HoldItemPos;
        public GameObject UIInteract;

    }
    public List<InteractableObjects> AllItems;

    List<string> itemInventory = new List<string>();
    public static GameManager m_instance;
    InteractableObjects TempItem;


    private void Awake()
    {
        if (m_instance = null)
        {
            m_instance = this;
        }
    }

    private void Start()
    {
        // Temporary need link this to a reset
        StartGame();
    }

    void StartGame()
    {
        StartCoroutine(SpawnObjects());
        
    }

    IEnumerator SpawnObjects()
    {
        for (int i = 0; i < AllItems.Count; i++)
        {
            InteractableObjects item = AllItems[i];
            int randomNumber = Random.Range(0, item.spawnLocale.Count);
            Transform spawnPoint = item.spawnLocale[randomNumber];
            GameObject obj = GameObject.Instantiate(item.ItemPrefab);
            obj.transform.position = spawnPoint.position;
            obj.transform.rotation = spawnPoint.rotation;
            obj.name = item.ItemName;
            item.SpawnedObj = obj;
            yield return null;
        }
    }

    InteractableObjects FindItemInQuestion(string ItemName)
    {
        foreach (InteractableObjects item in AllItems)
        {
            if (item.ItemName == ItemName)
            {
                return item;
            }
        }
        return new InteractableObjects();
    }

    public void AddItem(InteractableObjects HoldingItem) // upon picking up item just add to list
    {
        itemInventory.Add(HoldingItem.ItemName);
        if(HoldingItem.ItemRemove != "")
        {
            DropItem(HoldingItem.ItemRemove);
        }
    }
    public void DropItem(string ItemName) // only used for item that are being Held 
    {
        TempItem = FindItemInQuestion(ItemName);
        itemInventory.Remove(TempItem.ItemName);
    }
    public void InteractWithItem(string ItemName) // use this to see if Item Should be attached to player hand
    {
        TempItem = FindItemInQuestion(ItemName);
        if(TempItem.canHold)
        {
            // attach to arm
            AddItem(TempItem);
            TempItem.SpawnedObj.transform.parent = TempItem.HoldItemPos;
            TempItem.SpawnedObj.transform.localPosition = Vector3.zero;
            TempItem.SpawnedObj.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            // item that either just disappear or interact with ( sprays/ clothing )
            AddItem(TempItem);
            if(TempItem.disableOnUse)
            {
                // use for phone wallet house keys
                TempItem.SpawnedObj.SetActive(false);
            }
            else
            {
                // disable the spray so it wont keep firing
                StartCoroutine(DisableObjectTemporary(TempItem.SpawnedObj));
            }
        }
    }

    IEnumerator DisableObjectTemporary (GameObject obj)
    {
        obj.GetComponent<PickableItem>().enabled = false;
        yield return new WaitForSeconds(1.0f);
        obj.GetComponent<PickableItem>().enabled = true;
        yield return null;
    }


    public void  PopUpUI()
    {

    }

    // used for Story Part will return true or false
    public List<string> ExportListOfName()
    {
        return itemInventory;
    }

    public void ClearList()
    {
        if (itemInventory.Count != 0)
            itemInventory.Clear();
    }
}
