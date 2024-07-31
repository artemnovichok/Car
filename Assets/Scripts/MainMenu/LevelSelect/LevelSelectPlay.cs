using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectPlay : MonoBehaviour
{
    private LevelData levelData = new();
    [Header("число на кнопке запуска уровня(например 1)")]
    [SerializeField] private TMP_Text Leveltext;
    [Header("Замок, который показывает, что уровень заблокирован")]
    [SerializeField] private Image lockImg;
    [Header("Справйт для пустой звезды(не отображается в едиторе, нужна для кода)")]
    [SerializeField] private Sprite emptyStar;
    [Header("Справйт для полной звезды(не отображается в едиторе, нужна для кода)")]
    [SerializeField] private Sprite fullStar;
    [Header("Звезды(не отображаются в едиторе)")]
    [SerializeField] private Image[] starsImg;
    [Header("Иконка подарка")]
    [SerializeField] private Image bonusIcon;

    private void OnEnable() => gameObject.GetComponent<Button>().onClick.AddListener(loadLevel);
    private void OnDestroy() => gameObject.GetComponent<Button>().onClick.RemoveListener(loadLevel);

    public void Inicialize(int value, LevelType lvlType)// value - какой мы уровень по счету lvlType - тип уровня
    {
        bonusIcon.enabled = LevelEvent.Instance.Init(value + (30 * (int)lvlType), (int)lvlType) == EventType.BonusLevel ? true : false;
        print("уровень " + (value + (30 * (int)lvlType)).ToString() + " тип " + ((int)lvlType).ToString() + "бонус" + (LevelEvent.Instance.Init(value + (30 * (int)lvlType), (int)lvlType) == EventType.BonusLevel ? true : false).ToString());
        levelData.id = value;
        levelData.type = lvlType;
        if (LoadLevelData(value, lvlType) == null)
        {
            if (value == 0 && lvlType == LevelType.first)
            {
                levelData.isUnlock = true;
            }
        }
        else
        {
            levelData = LoadLevelData(value, lvlType);
        }
        castomize();
    }


    private void loadLevel()
    {
        if (levelData.isUnlock)
        {
            for (int i = 0; i < ItemHolder.Instance.GetAllBlocksLength(); i++)
        {

            for (int j = 0; j < ItemHolder.Instance.GetBlockInAllBlocks(i).Blocks.Length; j++)
            {
                ItemHolder.Instance.GetBlockInAllBlocks(i).Blocks[j].CountBlocks = ItemHolder.Instance.GetBlockInAllBlocks(i).Blocks[j].CountBlocksMax;
                ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocks = ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocksMax;
            }
        }
        SceneManager.LoadScene(1);
        PlayerPrefs.SetInt("numLevelWasSelected", levelData.id); //техническое сохранение для загрузки сцены игры, его удалять или переводить не надо
        PlayerPrefs.SetInt("LvlTypeWasSelected", (int)levelData.type);
    }
}

    public LevelData GetLevelData()
    {
        return levelData;
    }

    private LevelData LoadLevelData(int numLevel, LevelType lvlType)
    {
        string key = lvlType.ToString() + "level" + numLevel;
        object data = SaveData.Instance.GetData(key);
        if (data != null)
        {
            return JsonConvert.DeserializeObject<LevelData>(JsonConvert.SerializeObject(data));
        }
        else
        {
            return null;
        }
    }

    private void castomize()
    {
        Leveltext.text = (levelData.id + 1).ToString();
        if (levelData.isUnlock == true)
        {
            lockImg.enabled = false;
            int i = 0;
            foreach (Image image in starsImg)
            {
                image.enabled = true;
                if (i < levelData.countStar)
                {
                    image.sprite = fullStar;
                }
                else
                {
                    image.sprite = emptyStar;
                }
                i++;
            }
        }
        else
        {
            lockImg.enabled = true;
            foreach (Image image in starsImg)
            {
                image.enabled = true;
                image.sprite = emptyStar;
            }
        }
    }

    public int GetStarsNum()
    {
        return levelData.countStar;
    }
}
