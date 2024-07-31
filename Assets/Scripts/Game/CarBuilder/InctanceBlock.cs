using System.Linq;
using UnityEngine;

public class InctanceBlock : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    private CellSettingHolder[] cellSettings = new CellSettingHolder[25];
    private void Start()
    {
        for (int i = 0; i < 25; i++)
        {
            object data = SaveData.Instance.GetData("BlockInGrid" + i);
            if (data != null)
            {
                cellSettings[i] = (CellSettingHolder)data;
                inctanceBlock(i);
            }
        }
        EventManager.Instance.UpdateBlocksInGrid();
        CountBlockOnStart countBlockStart = GameObject.FindObjectOfType<CountBlockOnStart>();
        countBlockStart.countBlock();
        EventManager.Instance.FreezBLock();
    }

    private void inctanceBlock(int index)
    {
        if (!cellSettings[index].IsBlockHere)
        {
            return;
        }

        GameObject go = null;

        string[] excludedPrefabNames = { "DraggingImage", "IconBlock(Button)" };
        string folderPath = string.Empty;
        if (cellSettings[index].BlockID == 0)
        {
            folderPath = "Blocks/0";
        }
        else if(cellSettings[index].BlockID == 123123123)
        {
            folderPath = "Blocks/deliveryBlock";
        }
        else
        {
            BlockRarity ourRarity = (BlockRarity)cellSettings[index].BlocRarity;
            folderPath = $"Blocks/{cellSettings[index].BlockID}/{ourRarity}";
        }
        GameObject[] allPrefabs = Resources.LoadAll<GameObject>(folderPath);
        
        go = allPrefabs.FirstOrDefault(prefab => !excludedPrefabNames.Contains(prefab.name));
        if (go != null)
        {
            GameObject insGO;
            if (go.name == "block_character")
            {
                insGO = Instantiate(go, points[index].position, Quaternion.Euler(90f, 0, cellSettings[index].Rotate));
                GameObject character = Instantiate(ItemHolder.Instance.GetCurrentSkin(), insGO.transform.position, Quaternion.Euler(0, -50, 0));
                character.transform.parent = insGO.transform;
            }
            else
            {
                insGO = Instantiate(go, points[index].position, Quaternion.Euler(0,0, cellSettings[index].Rotate));
            }
            insGO.layer = LayerMask.NameToLayer("CarBlock");
            if(!insGO.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                insGO.AddComponent<Rigidbody>();
            }
            insGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            CarBlocksSettings blockSettings = insGO.AddComponent<CarBlocksSettings>();
            blockSettings.blockSettings.id = cellSettings[index].BlockID;
            blockSettings.blockSettings.ourRarity = (BlockRarity)cellSettings[index].BlocRarity;
            blockSettings.blockSettings.weConnectUp = cellSettings[index].ConnectUp;
            blockSettings.blockSettings.weConnectDown = cellSettings[index].ConnectDown;
            blockSettings.blockSettings.weConnectLeft = cellSettings[index].ConnectLeft;
            blockSettings.blockSettings.weConnectRight = cellSettings[index].ConnectRight;
            HPblock hpBlock = insGO.AddComponent<HPblock>();
            insGO.AddComponent<CheckGameOver>();
            insGO.AddComponent<CollectCoin>();
            insGO.AddComponent<CarFinish>();
            insGO.AddComponent<Freez>();
            if (cellSettings[index].BlockID == 123123123)
            {
                hpBlock.maxHp = ItemHolder.Instance.blockDelivery.Blocks[0].BlockHP;
                hpBlock.minSpeedToGetDMG = ItemHolder.Instance.blockDelivery.Blocks[0].MinSpeedToGetDMG;
            }
            else
            {
                hpBlock.maxHp = ItemHolder.Instance.GetBlockInAllBlocks(cellSettings[index].BlockID).Blocks[cellSettings[index].BlocRarity].BlockHP;
                hpBlock.minSpeedToGetDMG = ItemHolder.Instance.GetBlockInAllBlocks(cellSettings[index].BlockID).Blocks[cellSettings[index].BlocRarity].MinSpeedToGetDMG;
            }
        }
        else
        {
            Debug.Log("Нет объекта");
        }
    }
}
