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
    [Header("������ ���� ������ � ����")]
    public IdBlock[] allBloks;
    [Header("���� ��������")]
    public IdBlock blockDelivery;
    [Header("������ ���� ������ � ����")]
    public Skins skins;


    public void SetBlockOnLevel()
    {
        availableBloks.Clear();
        boughtBloks.Clear();
        NotAvaliableBlock.Clear();
        //������������ id ������� �����(����������� �����)
        for (int i = 0; i < allBloks.Length; i++)
        {
            for (int j = 0; j < allBloks[i].Blocks.Length; j++)
            {
                allBloks[i].Blocks[j].ourID = allBloks[i].id;
            }
        }

        //������������� ������ �� ��������� � ���(��� �������� ������)
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
        [Tooltip("id �����(�������� �����, ������ 0)")]
        public int id;
        public Block[] Blocks;
    }

    [Serializable]
    public struct Block
    {
        [Header("���������� �� ������, ����� ������� ���������")]

        [Tooltip("��� ����� �� �������")]
        public string BlockNameRu;
        [Tooltip("��� ����� �� ����������")]
        public string BlockNameEn;
        [Tooltip("��� ����� �� ��������")]
        public string BlockNameTr;
        [Tooltip("������ �� �� ���� ����")]
        public bool IsWasBuy;
        [Tooltip("������ �����")]
        public GameObject blockPrefab;
        [Tooltip("������ ������")]
        public GameObject blockIconPrefab;
        [Tooltip("�������� �����")]
        public BlockRarity Rarity;
        [Tooltip("�������� �� ����")]
        public bool IsDonateBlock;
        [Tooltip("����������� �� �����")]
        public int BlockHP;
        [Tooltip("�������� ��� ��������� �����")]
        public int MinSpeedToGetDMG;
        [Tooltip("���� ������������� �����")]
        public int CostToUnlock;
        [Tooltip("������� ������ ��� �������������")]
        public int LevelToUnlock;
        [Tooltip("����������� �� ������")]
        public bool NotInfinityBlock;
        [Tooltip("����������� ������ ��� ��������")]
        public int CountBlocks;
        [Tooltip("������������ ����������� ������ ��� ��������")]
        public int CountBlocksMax;

        [HideInInspector] //����������� �����, �� ���� ����������� �������
        public int ourID;
    }

    [Serializable]
    public struct Skins
    {
        public int CurrentSkinId;
        public Skin[] skin;
    }
}
