using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    public static ItemHolder Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Load();
    }

    private List<Block> boughtBloks = new List<Block>();
    private List<Block> availableBloks = new List<Block>();
    [HideInInspector]
    public List<Block> NotAvaliableBlock = new List<Block>();
    [Header("ћассив всех блоков в игре")]
    public IdBlock[] allBloks;
    [Header("Ѕлок доставки")]
    public IdBlock blockDelivery;
    [Header("ћассив всех скинов в игре")]
    public Skins skins;


    public void SetBlockOnLevel()
    {
        availableBloks.Clear();
        boughtBloks.Clear();
        NotAvaliableBlock.Clear();
        //присваивание id каждому блоку(техническа€ штука)
        for (int i = 0; i < allBloks.Length; i++)
        {
            for (int j = 0; j < allBloks[i].Blocks.Length; j++)
            {
                allBloks[i].Blocks[j].ourID = allBloks[i].id;
            }
        }

        //распределение блоков по доступным и нет(при открытии уровн€)
        foreach (IdBlock idBlock in allBloks)
        {
            if (idBlock.id == 0)
            {
                boughtBloks.Add(idBlock.Blocks[0]);
            }
            else
            {
                foreach (Block block in idBlock.Blocks)
                {
                    if (block.LevelToUnlock <= CurrentLevelManager.Instance.GetCurrentLevel())
                    {
                        if (block.IsWasBuy)
                        {
                            boughtBloks.Add(block);
                        }
                        else
                        {
                            availableBloks.Add(block);
                        }
                    }
                    else
                    {
                        NotAvaliableBlock.Add(block);
                    }
                }
            }
        }
    }




    #region publicMetods

    public int GetListBoughtBloksLength()
    {
        return boughtBloks.Count;
    }

    public int GetListAvailableBloksLength()
    {
        return availableBloks.Count;
    }

    public Block GetBoughtBlok(int index)
    {
        return boughtBloks[index];
    }

    public Block GetAvailableBlok(int index)
    {
        return availableBloks[index];
    }

    public int GetAllBlocksLength()
    {
        return allBloks.Length;
    }

    public IdBlock GetBlockInAllBlocks(int index)
    {
        return allBloks[index];
    }

    public void Save()
    {
        SaveData.Instance.Save("currentSkin", skins.CurrentSkinId);
        foreach (Skin skin in skins.skin)
        {
            SaveData.Instance.Save("idSkin_" + skin.Id, skin.IsUnlock);
        }
        foreach (IdBlock idblock in allBloks)
        {
            foreach (Block block in idblock.Blocks)
            {
                SaveData.Instance.Save("idBlock_" + idblock.id + "_rarity_" + block.Rarity, block.IsWasBuy);
            }
        }
    }

    public void Load()
    {
        object currentSkinData = SaveData.Instance.GetData("currentSkin");
        if (currentSkinData != null)
            skins.CurrentSkinId = currentSkinData.ConvertTo<int>();
        foreach (Skin skin in skins.skin)
        {
            object skinData = SaveData.Instance.GetData("idSkin_" + skin.Id);
            if (skinData != null)
                skins.skin[skin.Id].IsUnlock = skinData.ConvertTo<Boolean>();
        }
        foreach (IdBlock idblock in allBloks)
        {
            foreach (Block block in idblock.Blocks)
            {
                object BlockData = SaveData.Instance.GetData("idBlock_" + idblock.id + "_rarity_" + block.Rarity);
                if (BlockData != null)
                {
                    allBloks[idblock.id].Blocks[(int)block.Rarity].IsWasBuy = BlockData.ConvertTo<Boolean>();
                }
            }
        }
    }

    public GameObject GetCurrentSkin()
    {
        return Resources.Load<GameObject>("Skins/Prefabs/" + (skins.CurrentSkinId + 1));
    }

    #endregion



    [Serializable]
    public struct IdBlock
    {
        [Tooltip("id блока(название папки, пример 0)")]
        public int id;
        public Block[] Blocks;
    }

    [Serializable]
    public struct Block
    {
        [Header("Ќавидитесь на строку, чтобы увидеть по€снение")]

        [Tooltip("им€ блока на русском")]
        public string BlockNameRu;
        [Tooltip("им€ блока на английском")]
        public string BlockNameEn;
        [Tooltip("им€ блока на турецком")]
        public string BlockNameTr;
        [Tooltip(" упили ли мы этот блок")]
        public bool IsWasBuy;
        [Tooltip("префаб блока")]
        public GameObject blockPrefab;
        [Tooltip("префаб иконки")]
        public GameObject blockIconPrefab;
        [Tooltip("редкость блока")]
        public BlockRarity Rarity;
        [Tooltip("донатный ли блок")]
        public bool IsDonateBlock;
        [Tooltip("колличество хп блока")]
        public int BlockHP;
        [Tooltip("скорость дл€ получени€ урона")]
        public int MinSpeedToGetDMG;
        [Tooltip("÷ена разблокировки блока")]
        public int CostToUnlock;
        [Tooltip("”ровень игрока дл€ разблокировки")]
        public int LevelToUnlock;
        [Tooltip("Ѕесконечные ли детали")]
        public bool NotInfinityBlock;
        [Tooltip(" олличество блоков дл€ поставки")]
        public int CountBlocks;
        [Tooltip("ћаксимальное колличество блоков дл€ поставки")]
        public int CountBlocksMax;

        [HideInInspector] //техническа€ штука, не надо настраивать вручную
        public int ourID;
    }

    [Serializable]
    public struct Skins
    {
        public int CurrentSkinId;
        public Skin[] skin;
    }
}
