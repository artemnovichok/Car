using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SetSettings : MonoBehaviour
{
    [Header("Sound")]
    [SerializeField] private Slider musicSlider;

    [Header("Graphics")]
    [SerializeField]
    private TMP_Dropdown graf;

    [Header("Quality Settings")]
    [SerializeField]
    private UniversalRenderPipelineAsset[] qualityLevels; // Добавить массив Render Pipeline Assets

    private void Start()
    {
        if (GameLanguage.Instance.GetLanguage() == "ru")
        {
            List<string> list = new List<string>() { "Очень Низкий", "Низкий", "Средний", "Высокий", "Очень Высокий", "Ультра" };
            graf.options.Clear();
            graf.AddOptions(list);
        }
        else if (GameLanguage.Instance.GetLanguage() == "en")
        {
            graf.options.Clear();
            graf.AddOptions(QualitySettings.names.ToList());
        }
        else
        {
            List<string> list = new List<string>() { "Çok Düşük", "Düşük", "Orta", "Yüksek", "Çok Yüksek", "Ultra" };
            graf.options.Clear();
            graf.AddOptions(list);
        }

        SetQuality(SettingHolder.Instance.GraphValue);
    }

    private void OnEnable()
    {
        SetQuality(SettingHolder.Instance.GraphValue);
        graf.onValueChanged.AddListener(SetQuality);

        musicSlider.value = SettingHolder.Instance.MusicValue;
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
    }

    private void OnDisable()
    {
        graf.onValueChanged.RemoveListener(SetQuality);
        musicSlider.onValueChanged.RemoveListener(OnMusicSliderChanged);
    }

    private void OnMusicSliderChanged(float value)
    {
        SettingHolder.Instance.MusicValue = value;
    }

    private void SetQuality(int value)
    {
        graf.value = value;
        QualitySettings.SetQualityLevel(value);
        SettingHolder.Instance.GraphValue = value;

        // Изменить активный Render Pipeline Asset
        if (value >= 0 && value < qualityLevels.Length)
        {
            UniversalRenderPipelineAsset asset = qualityLevels[value];
            if (asset != null)
            {
                GraphicsSettings.renderPipelineAsset = asset;
                Debug.Log("Graphics quality set to: " + asset.name);
            }
        }
        else
        {
            Debug.LogWarning("Invalid quality level index");
        }
    }
}
