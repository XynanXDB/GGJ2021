using Game.Runtime.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject GFBioObj;
    public Text BioText1;
    public Text BioText2;
    public Text BioText3;
    public string[] ClothingBIo = new string[3];
    public string[] SmellBio = new string[3];
    public string[] GiftBio = new string[3];

    [Header("Game Timer Settings")]
    public float CountDownTime;
    public double TotalTime;
    public GameObject TimerUI;
    public Text TimerText;
    public Player playerObject;

    int[] GFBehaviour = new int[3];
    List<string> itemInventory = new List<string>();

    Dictionary<string, GameObject> SpawnedObjectDic = new Dictionary<string, GameObject>();
    public static GameManager m_instance;
    InteractableObjects TempItem;


    private void Awake()
    {
        if (m_instance == null)
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

    IEnumerator GameTimerCoroutine()
    {
        yield return null;
        RandomGF(); 
        yield return null;
        int ProfileInt = GFBehaviour[0];
        BioText1.text = ClothingBIo[ProfileInt-1];
        yield return null;
        ProfileInt = GFBehaviour[1];
        BioText2.text = SmellBio[ProfileInt-1];
        yield return null;
        ProfileInt = GFBehaviour[2];
        BioText3.text = GiftBio[ProfileInt-1];
        yield return null;
        playerObject.DisableMovement();

        float timeStart = Time.time;

        while((Time.time - timeStart) < (CountDownTime/3) )
        {
            TimerText.text = "Ready";
            yield return null;
        }
        timeStart = Time.time;
        while ((Time.time - timeStart) < (CountDownTime / 3))
        {
            TimerText.text = "Ready";
            yield return null;
        }
        timeStart = Time.time;
        while ((Time.time - timeStart) < (CountDownTime / 3))
        {
            TimerText.text = "Steady";
            yield return null;
        }

        playerObject.EnableMovement();
        //Count down begins and play game

        TimerText.text = "Rush";
        yield return new WaitForSeconds(1.0f);


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
            yield return null;
            SpawnedObjectDic.Add(item.ItemName, obj);
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

        if(HoldingItem.ItemRemove2 != "")
        {
            DropItem(HoldingItem.ItemRemove2);
        }
    }
    public void DropItem(string ItemName) // only used for item that are being Held 
    {
        TempItem = FindItemInQuestion(ItemName);
        if(TempItem.ItemName != null)
            itemInventory.Remove(TempItem.ItemName);
    }
    public void InteractWithItem(string ItemName) // use this to see if Item Should be attached to player hand
    {
        TempItem = FindItemInQuestion(ItemName);
        if(TempItem.canHold)
        {
            // attach to arm
            AddItem(TempItem);
            GameObject obj = SpawnedObjectDic[TempItem.ItemName];
            obj.transform.parent = TempItem.HoldItemPos;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
        }
        else
        {
            // item that either just disappear or interact with ( sprays/ clothing )
            AddItem(TempItem);
            GameObject obj = SpawnedObjectDic[TempItem.ItemName];
            if (TempItem.disableOnUse)
            {
                // use for phone wallet house keys
                obj.SetActive(false);
            }
            else
            {
                // disable the spray so it wont keep firing
                StartCoroutine(DisableObjectTemporary(obj));
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

    public void RandomGF()
    {
        for (int i = 0; i < 3; i++)
        {
            GFBehaviour[i] = Random.Range(1, 4);
        }
    }

    public int[] GetGFProfile()
    {
        return GFBehaviour;
    }
}
