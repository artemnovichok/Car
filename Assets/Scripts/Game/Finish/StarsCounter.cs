using Newtonsoft.Json;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StarsCounter : MonoBehaviour
{
    [SerializeField] private Image[] stars;
    [SerializeField] private Sprite spriteEmpty;
    [SerializeField] private Sprite spriteFull;
    private int starsNum = 0;
    private void OnEnable()
    {
        EventManager.Instance.finishLevel.AddListener(save);
    }

    private void OnDisable()
    {
        EventManager.Instance.finishLevel.RemoveListener(save);
    }

    private void Start()
    {
        foreach (Image img in stars)
        {
            img.sprite = spriteEmpty;
        }
        count();
    }
    private void count()
    {
        CountBlockOnStart countBlockStart = GameObject.FindObjectOfType<CountBlockOnStart>();
        int blocksWas = countBlockStart.GetCount();
        CarBlocksSettings[] carBlokSettings = GameObject.FindObjectsOfType<CarBlocksSettings>();
        int blocksNow = carBlokSettings.Length;
        int procent = 0;
        if (blocksWas > 0)
        {
            procent = (int)Mathf.Round((float)blocksNow / blocksWas * 100);
        }
        if (procent < 50)
        {
            starsNum = 1;
        }
        else if (procent < 85)
        {
            starsNum = 2;
        }
        else
        {
            starsNum = 3;
        }
        for (int i = 0; i < starsNum; i++)
        {
            stars[i].sprite = spriteFull;
        }
    }

    private void save()
    {
        int currentLevel = PlayerPrefs.GetInt("numLevelWasSelected");
        string currLvlType = Enum.GetName(typeof(LevelType), PlayerPrefs.GetInt("LvlTypeWasSelected"));
        LevelData curLvlData = LoadLevelData(currentLevel, currLvlType);
        int curLvlDataStars = curLvlData == null ? 0 : curLvlData.countStar; //имитируем данные
        if (curLvlDataStars < starsNum)
        {
            LevelData lvlData = new LevelData();
            lvlData.id = currentLevel;
            lvlData.type = (LevelType)PlayerPrefs.GetInt("LvlTypeWasSelected");
            lvlData.isUnlock = true;
            lvlData.countStar = starsNum;
            SaveData.Instance.Save(currLvlType + "level" + currentLevel, lvlData);
            object starsData = SaveData.Instance.GetData("CurrentNumOfStar");
            int currentStars = starsData == null ? 0 : Convert.ToInt32(starsData);
            SaveData.Instance.Save("CurrentNumOfStar", currentStars + (starsNum - curLvlDataStars));
            if (currentLevel != 29)
            {
                if(LoadLevelData(currentLevel + 1, currLvlType) == null)
                {
                    LevelData nextlvlData = new LevelData();
                    nextlvlData.id = currentLevel + 1;
                    nextlvlData.isUnlock = true;
                    nextlvlData.countStar = 0;
                    nextlvlData.type = (LevelType)PlayerPrefs.GetInt("LvlTypeWasSelected");
                    SaveData.Instance.Save(currLvlType + "level" + (currentLevel + 1), nextlvlData);
                }
            }
            else
            {
                if(PlayerPrefs.GetInt("LvlTypeWasSelected") != 2 && LoadLevelData(0, Enum.GetName(typeof(LevelType), PlayerPrefs.GetInt("LvlTypeWasSelected") +1)) == null)//3-ему режиму
                {
                    
                    int nextLvlTypeInt = PlayerPrefs.GetInt("LvlTypeWasSelected") + 1;
                    LevelData nextlvlData = new LevelData();
                    nextlvlData.id = 0;
                    nextlvlData.isUnlock = true;
                    nextlvlData.countStar = 0;
                    nextlvlData.type = (LevelType)nextLvlTypeInt;
                    SaveData.Instance.Save(Enum.GetName(typeof(LevelType), nextLvlTypeInt) + "level" + 0, nextlvlData);
                }    
            }
        }
    }

    private LevelData LoadLevelData(int numLevel, string lvlType)
    {
        string key = lvlType + "level" + numLevel;
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
}
