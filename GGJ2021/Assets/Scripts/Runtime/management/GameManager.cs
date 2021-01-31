using System;
using Game.Runtime.Player;
using System.Collections;
using System.Collections.Generic;
using Game.Runtime.Input;
using Game.Runtime.UI;
using Game.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [System.Serializable]
    public struct InteractableObjects
    {
        public string ItemName;
        public GameObject ItemPrefab;
        public bool canHold;
        public bool disableOnUse;
        public string ItemRemove;
        public string ItemRemove2;
        public List<Transform> spawnLocale;
        public Transform HoldItemPos;
    }    

    public List<InteractableObjects> AllItems;

    [Header("UI Pop UP Curve")]
    public float popSpeed = 4f;
    public AnimationCurve PopUpAnim;

    [Header("GF Bio UI")]
    public string[] ClothingBIo = new string[3];
    public string[] SmellBio = new string[3];
    public string[] GiftBio = new string[3];

    [Header("Game Timer Settings")]
    public float CountDownTime;
    public double TotalTime;
    public GameObject TimerUI;
    public TMP_Text TimerText;
    public Player playerObject;
    
    [Header("UI")]
    [SerializeField] protected TMP_Text CueCardText;
    [SerializeField] protected GameObject InGameHUD;
    [SerializeField] protected GameObject InventoryEntryPrefab;
    [SerializeField] protected GameObject ProfilePrompt;
    [SerializeField] protected TMP_Text ProfileText;
    [SerializeField] protected Button ProfileOKButton;
    [SerializeField] protected Transform InventoryList;
    
    
    int[] GFBehaviour = new int[3];
    public List<string> itemInventory = new List<string>();

    Dictionary<string, GameObject> SpawnedObjectDic = new Dictionary<string, GameObject>();

    public static GameManager m_instance;
    InteractableObjects TempItem;
    InteractableObjects CurrentlyHolding;
    
    public OneParamSignature<string> PostInteractNotification;

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        
        playerObject.SetInputMode(InputMode.Disable);

        ProfileOKButton.onClick.AddListener(OnClickProfileOK);
        ProfilePrompt.SetActive(true);
        
        CueCardText.gameObject.SetActive(false);
        InGameHUD.SetActive(false);
        GeneratorGFProfile();
    }

    void OnClickProfileOK()
    {
        ProfilePrompt.SetActive(false);
        CueCardText.gameObject.SetActive(true);
        StartCoroutine(GameTimerCoroutine());
    }

    private void Start()
    {
        PostInteractNotification += UIManager.UUIManager.PostInteractNotification;
        
        // Temporary need link this to a reset
        StartGame();
    }

    void OnDestroy()
    {
        PostInteractNotification = null;
    }

    void StartGame()
    {
        StartCoroutine(SpawnObjects());
        // Supposely Coroutine Goes Here
        
        ProfilePrompt.SetActive(true);
    }

    IEnumerator GameTimerCoroutine()
    {
        float timeStart = Time.time;

        while((Time.time - timeStart) < (CountDownTime/3) )
        {
            CueCardText.text = "Ready";
            yield return null;
        }
        timeStart = Time.time;
        while ((Time.time - timeStart) < (CountDownTime / 3))
        {
            CueCardText.text = "Ready";
            yield return null;
        }
        timeStart = Time.time;
        while ((Time.time - timeStart) < (CountDownTime / 3))
        {
            CueCardText.text = "Steady";
            yield return null;
        }

        // Player can move already and start running around

        playerObject.SetInputMode(InputMode.Game);
        //Count down begins and play game

        CueCardText.text = "Rush";
        yield return new WaitForSeconds(1.0f);
        
        CueCardText.gameObject.SetActive(false);
        InGameHUD.gameObject.SetActive(true);
        
        timeStart = Time.time;
        float Lifetime = Time.time - timeStart;
        byte RemainingTime = 0;
        while (Lifetime < TotalTime - Lifetime)
        {
            RemainingTime = Convert.ToByte(TotalTime - Time.time + timeStart);
            TimerText.text = RemainingTime.ToString();
            
            yield return null;
        }

        // the main countdown is here using the   float of ~~~ CountDownTime  ~~~~ to set how long the game to run for

        // will do
        // TimerText.text = TotalTime     as ~~~~ Total Time ~~~~~ is a double just 
        //                                      need convert to string here or something more faster and light weight

        // then end here after duration over  ~~~ CountDownTime ~~~~~
        

        // if early end run this will add the run early in
        CameEarly();
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
            obj.transform.GetChild(0).gameObject.SetActive(false);
            SpawnedObjectDic.Add(obj.name, obj);
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
        PostInteractNotification?.Invoke(HoldingItem.ItemName);
        
        if(HoldingItem.ItemRemove != "")
        {
            RemoveItem(HoldingItem.ItemRemove);
        }

        if(HoldingItem.ItemRemove2 != "")
        {
            RemoveItem(HoldingItem.ItemRemove2);
        }

        RefreshInventoryUI();
    }

    void RefreshInventoryUI()
    {
        //Performance is bad, but no time to optimise
        foreach (Transform GO in InventoryList)
            Destroy(GO.gameObject);

        foreach (string Item in itemInventory)
        {
            GameObject GO = GameObject.Instantiate(InventoryEntryPrefab, Vector3.zero, Quaternion.identity, InventoryList);
            GO.GetComponent<TMP_Text>().text = Item;
        }
    }

    public void RemoveItem(string ItemName) // remove from List
    {
        TempItem = FindItemInQuestion(ItemName);

        if (TempItem.ItemName != null)
            itemInventory.Remove(TempItem.ItemName);

        if(TempItem.canHold)
        {
            // Drop Item
            DropItem(TempItem);
        }
        
        RefreshInventoryUI();
    }

    public void DropItem(InteractableObjects Item) // only used for item that are being Held 
    {
        GameObject obj = SpawnedObjectDic[Item.ItemName];
        obj.GetComponent<Collider>().enabled = true;
        obj.GetComponent<Rigidbody>().detectCollisions = true;
        obj.GetComponent<Rigidbody>().useGravity = true;
        obj.GetComponent<Rigidbody>().isKinematic = false;

        obj.transform.parent = null;
    }
    public void InteractWithItem(string ItemName) // use this to see if Item Should be attached to player hand 
    {
        // system will always call this for interaction ===========================

        if (itemInventory.Contains(ItemName))
        {
            return;
        }

        TempItem = FindItemInQuestion(ItemName);
        if (TempItem.ItemName == CurrentlyHolding.ItemName)
        {
            // CAll UI Hands are full; PLS drop item with Left Shit
            return;
        }
        if (TempItem.canHold)
        {
            CurrentlyHolding = TempItem;
            GameObject obj = SpawnedObjectDic[TempItem.ItemName];
            obj.transform.parent = TempItem.HoldItemPos;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
            obj.GetComponent<Collider>().enabled = false;
            obj.GetComponent<Rigidbody>().detectCollisions = false;
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.GetComponent<Rigidbody>().isKinematic = true;

            // attach to arm
        }
        else if(TempItem.disableOnUse)
        {
            // item that either just disappear or interact with ( sprays/ clothing )
            GameObject obj = SpawnedObjectDic[TempItem.ItemName];
            // use for phone wallet house keys
            obj.SetActive(false);
        }
        AddItem(TempItem);
    }

    public void PopUpUI(string ItemName)
    {
        TempItem = FindItemInQuestion(ItemName);
        GameObject uipopObj = SpawnedObjectDic[TempItem.ItemName].transform.GetChild(0).gameObject;
        StartCoroutine(PopUpUICoroutine(uipopObj));
    }

    IEnumerator PopUpUICoroutine (GameObject obj)
    {
        obj.transform.rotation = Quaternion.identity;
        float elapsedTime = 0;
        Vector3 OriSize = obj.transform.localScale;
        obj.SetActive(true);
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * popSpeed;
            obj.transform.localScale = PopUpAnim.Evaluate(elapsedTime) * OriSize;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    public void PopDownUI(string ItemName)
    {
        TempItem = FindItemInQuestion(ItemName);
        GameObject uipopObj = SpawnedObjectDic[TempItem.ItemName].transform.GetChild(0).gameObject;
        StartCoroutine(PopDownUICoroutine(uipopObj));
    }

    IEnumerator PopDownUICoroutine (GameObject obj)
    {
        float elapsedTime = 0;
        Vector3 OriSize = obj.transform.localScale;
        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime * popSpeed;
            obj.transform.localScale = PopUpAnim.Evaluate(1-elapsedTime) * OriSize;
            yield return new WaitForEndOfFrame();
        }
        obj.SetActive(false);
        obj.transform.localScale = OriSize;
        yield return null;
    }

    // used for Story Part will return true or false
    public List<string> ExportListOfName()
    {
        return itemInventory;
    }

    public void CameEarly()
    {
        itemInventory.Add("ComeEarly");
    }

    public void ClearList()
    {
        if (itemInventory.Count != 0)
            itemInventory.Clear();
    }

    public void GeneratorGFProfile()
    {
        for (int i = 0; i < 3; i++)
        {
            GFBehaviour[i] = Random.Range(1, 4);
        }

        int ClothingIndex = GFBehaviour[0] - 1;
        int SmellIndex = GFBehaviour[1] - 1;
        int GiftIndex = GFBehaviour[2] - 1;
        
        string Output = string.Format($"<color=#222b1c>Oh no!! I'm running late! \n" +
                                      $"I gotta get ready quick! What did she say she liked again? \n\n" +
                                      "{0} \n" +
                                      "{1} \n" +
                                      "{2} \n\n" +
                                      $"ARGH!!! I gotta be quick!", ClothingBIo[ClothingIndex], SmellBio[SmellIndex], GiftBio[GiftIndex]);
        ProfileText.text = Output;

        UIManager.UUIManager.GFProfile = GFBehaviour;
    }

    public int[] GetGFProfile()
    {
        return GFBehaviour;
    }
}
