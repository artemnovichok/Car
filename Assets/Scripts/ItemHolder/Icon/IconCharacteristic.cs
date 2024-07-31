using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconCharacteristic : MonoBehaviour
{
    [HideInInspector]
    public bool IsDonate = false;
    [HideInInspector]
    public bool WasBuy = true;
    [HideInInspector]
    public int costToUnlock;
    //[HideInInspector]
    public int ourId;
    [HideInInspector]
    public BlockRarity ourRarity;
    [HideInInspector]
    public bool NotInfinityBlock = false;
    //[HideInInspector]
    //public int count;

    [Header("Иконка замка(не купленна деталь)")]
    [SerializeField] private GameObject _lockImage;
    [Header("Текст с ценой для покупки")]
    [SerializeField] private TMP_Text _costToBuy;

    private GameObject dragImageBlock;

    private void OnDisable() => gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
    private void OnDestroy() => gameObject.GetComponent<Button>().onClick.RemoveAllListeners();

    public void StartCustom()
    {

        Sprite newLockIcon = Resources.Load<Sprite>("Sprites/IconBlock/LockIcon");
        if (newLockIcon != null)
        {
            Image lockImageComponent = _lockImage.GetComponent<Image>();
            if (lockImageComponent != null)
            {
                lockImageComponent.sprite = newLockIcon;
            }
        }



        if (WasBuy)
        {
            gameObject.GetComponent<Button>().onClick.AddListener(interact);
            _lockImage.SetActive(false);
            _costToBuy.gameObject.SetActive(false);

            if (ourId == 0)
            {
                dragImageBlock = Resources.Load<GameObject>("Blocks/0/DraggingImage");
            }
            else if (ourId == 123123123)
            {
                dragImageBlock = Resources.Load<GameObject>("Blocks/deliveryBlock/DraggingImage");
            }
            else
            {
                dragImageBlock = Resources.Load<GameObject>("Blocks/" + ourId + "/" + ourRarity.ToString() + "/DraggingImage");
            }
        }
        else
        {
            gameObject.GetComponent<Button>().onClick.AddListener(buy);
            _lockImage.SetActive(true);
            _costToBuy.gameObject.SetActive(true);
            if (IsDonate)
            {
                //цена в валюте

            }
            else
            {
                _costToBuy.text = costToUnlock.ToString();
            }
        }
    }

    public void StartCustomNotAvaliableBlock(int lvlToUnlock)
    {
        _costToBuy.text = lvlToUnlock.ToString() + (GameLanguage.Instance.GetLanguage() == "ru" ? " лвл" : GameLanguage.Instance.GetLanguage() == "en" ? " lvl" : " düzey");
    }

    private void interact()
    {
        int currentPlayerLevel = CurrentLevelManager.Instance.GetCurrentLevel();
        int maxDetails = 0;
        switch (currentPlayerLevel)
        {
            case 0:
                maxDetails = 5;
                break;
            case 1:
                maxDetails = 8;
                break;
            case 2:
                maxDetails = 11;
                break;
            case 3:
                maxDetails = 14;
                break;
            case 4:
                maxDetails = 16;
                break;
            case 5:
                maxDetails = 18;
                break;
            case 6:
                maxDetails = 20;
                break;
            case 7:
                maxDetails = 22;
                break;
            case 8:
                maxDetails = 22;
                break;
            case 9:
                maxDetails = 25;
                break;
            default:
                break;
        }
        DragImage[] dragImages = GameObject.FindObjectsOfType<DragImage>();
        List<DragImage> dragImageList = new List<DragImage>(dragImages);
        if (NotInfinityBlock == true && ourId != 123123123 && ItemHolder.Instance.GetBlockInAllBlocks(ourId).Blocks[(int)ourRarity].CountBlocks > 0 && dragImages.Length + 1 <= maxDetails)
        {
            if (ourId == 4)//на сцене может быть только 1 двигатель, все id двигателей = 4
            {
                bool wasDetectedEngine = false;

                foreach (DragImage dragImage in dragImageList)
                {
                    if (dragImage.ourId == 4)
                    {
                        wasDetectedEngine = true;
                    }
                }
                if (!wasDetectedEngine)
                {
                    ItemHolder.Instance.GetBlockInAllBlocks(ourId).Blocks[(int)ourRarity].CountBlocks--;
                    GameObject go = Instantiate(dragImageBlock, GameObject.Find("WorkArea(Image)").gameObject.transform);
                    go.GetComponent<DragImage>().ourId = ourId;
                    go.GetComponent<DragImage>().ourRarity = ourRarity;
                    go.GetComponent<DragImage>().NotInfinityBlock = NotInfinityBlock;
                }
            }
            else
            {
                ItemHolder.Instance.GetBlockInAllBlocks(ourId).Blocks[(int)ourRarity].CountBlocks--;
                GameObject go = Instantiate(dragImageBlock, GameObject.Find("WorkArea(Image)").gameObject.transform);
                go.GetComponent<DragImage>().ourId = ourId;
                go.GetComponent<DragImage>().ourRarity = ourRarity;
                go.GetComponent<DragImage>().NotInfinityBlock = NotInfinityBlock;
            }
        }
        else if (ourId == 123123123 && ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocks > 0 && dragImages.Length + 1 <= maxDetails)
        {
            print("ставлю");
            ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocks--;
            GameObject go = Instantiate(dragImageBlock, GameObject.Find("WorkArea(Image)").gameObject.transform);
            go.GetComponent<DragImage>().ourId = ourId;
            go.GetComponent<DragImage>().ourRarity = ourRarity;
            go.GetComponent<DragImage>().NotInfinityBlock = NotInfinityBlock;
        }
        if (NotInfinityBlock == false && dragImages.Length + 1 <= maxDetails)
        {
            GameObject go = Instantiate(dragImageBlock, GameObject.Find("WorkArea(Image)").gameObject.transform);
            go.GetComponent<DragImage>().ourId = ourId;
            go.GetComponent<DragImage>().ourRarity = ourRarity;
            go.GetComponent<DragImage>().NotInfinityBlock = NotInfinityBlock;
        }
    }




    private void buy()
    {
        Debug.Log("покупка...");
        object moneyObj = SaveData.Instance.GetData("Money");
        int money = moneyObj == null ? 0 : Convert.ToInt32(moneyObj);
        if (money >= costToUnlock)
        {
            Debug.Log("покупка совершена");
            SaveData.Instance.Save("Money", money - costToUnlock);
            ItemHolder.Instance.allBloks[ourId].Blocks[((int)ourRarity)].IsWasBuy = true;
            ItemHolder.Instance.Save();
            EventManager.Instance.UpdateMoney();
            EventManager.Instance.UpdateBlocks();
        }
        else
        {
            Debug.Log("Не достаточно денег");
        }
    }
}
