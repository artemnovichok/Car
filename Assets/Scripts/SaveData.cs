using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }

    private Dictionary<string, object> data = new Dictionary<string, object>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //SaveData.Instance.Save("CurrentNumOfStar", 0);
            Load();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Load()
    {
        if (PlayerPrefs.HasKey("saveData"))
        {
            string jsonData = PlayerPrefs.GetString("saveData");
            data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
        }
        SettingHolder.Instance.LoadSettings(); //вызываем сеттинг холдер только после загрузки всех данных
    }

    public void Save(string key, object value)
    {
        data[key] = value;
        string jsonData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString("saveData", jsonData);
        PlayerPrefs.Save();
    }

    public object GetData(string key)
    {
        if (data.ContainsKey(key))
        {
            return data[key];
        }
        else
        {
            return null;
        }
    }
}
