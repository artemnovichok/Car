using System;
using UnityEngine;

public class SettingHolder : MonoBehaviour
{
    public static SettingHolder Instance { get; private set; }

    private float musicValue = 1f;

    private int graphValue = 0;

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
    }

    public void LoadSettings()
    {
        object musicValueData = SaveData.Instance.GetData("musicValue");
        musicValue = musicValueData == null ? 1f : Convert.ToSingle(musicValueData);
        object graphValueData = SaveData.Instance.GetData("graphValue");
        graphValue = graphValueData == null ? 0 : (int)Convert.ToSingle(graphValueData);
    }

    public float MusicValue
    {
        get
        {
            return musicValue;
        }
        set
        {
            musicValue = value;
            SaveData.Instance.Save("musicValue", value);
        }
    }

    public int GraphValue
    {
        get
        {
            return graphValue;
        }
        set
        {
            graphValue = value;
            SaveData.Instance.Save("graphValue", value);
        }
    }
}