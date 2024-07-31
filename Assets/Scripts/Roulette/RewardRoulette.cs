using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class RewardRoulette : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider slider;     
    public Button stopButton;  
    public TextMeshProUGUI rewardText;  

    [Header("Roulette Settings")]
    public float speed = 1f;  
    public float[] multipliers = { 2, 3, 4, 5, 4, 3, 2 };  

    private bool isMoving = true;

    void Start()
    {
        stopButton.onClick.AddListener(StopSlider);
        StartCoroutine(MoveSlider());
    }

    IEnumerator MoveSlider()
    {
        while (isMoving)
        {
            slider.value = Mathf.PingPong(Time.realtimeSinceStartup * speed, 7);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    private void StopSlider()
    {
        isMoving = false;
        stopButton.enabled = false;
        stopButton.onClick.RemoveListener(StopSlider);
            //вызов рекламы, после показа вызов метода OnReward
        OnReward();
    }

    private void OnReward()
    {
        AdvertisementAnalitic.Instance.WatchAdvInFinishMenu();
        AdvertisementAnalitic.Instance.WatchRewardAdv();
        int index = Mathf.FloorToInt(slider.value);
        float reward = multipliers[index];
        Debug.Log($"Reward Multiplier: x{reward}");
        rewardText.text = $"x{reward}";
        FindFirstObjectByType<CollectedCoin>().collectedCoinOnLevel = (int)(FindFirstObjectByType<CollectedCoin>().collectedCoinOnLevel * reward);
        EventManager.Instance.UpdateCoinTextOnLevel();
    }
}
