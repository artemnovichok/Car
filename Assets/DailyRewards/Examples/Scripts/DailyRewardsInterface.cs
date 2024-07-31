/***************************************************************************\
Project:      Daily Rewards
Copyright (c) Niobium Studios.
Author:       Guilherme Nunes Barbosa (gnunesb@gmail.com)
\***************************************************************************/
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using TMPro;
using System.Collections.Generic;
using static SamuraiGames.DailyRewards;
//using Firebase.Analytics;

namespace SamuraiGames
{
    /**
     * The UI Logic Representation of the Daily Rewards
     **/
    public class DailyRewardsInterface : MonoBehaviour
    {
        public GameObject dailyRewardPrefab;        // Prefab containing each daily reward

        [Header("Panel Debug")]
        public bool isDebug;
        public GameObject panelDebug;
        public Button buttonAdvanceDay;
        public Button buttonAdvanceHour;
        public Button buttonReset;
        public Button buttonReloadScene;

        [Header("Panel Reward Message")]
        public GameObject panelReward;              // Rewards panel
        public Text textReward;                     // Reward Text to show an explanatory message to the player
        public Button buttonCloseReward;            // The Button to close the Rewards Panel
        public Image imageReward;                   // The image of the reward

        [Header("Panel Reward")]
        public Button buttonClaim;
        public TextMeshProUGUI textTimeDue;                    // Text showing how long until the next claim
        public GridLayoutGroup dailyRewardsGroup;   // The Grid that contains the rewards
        public ScrollRect scrollRect;               // The Scroll Rect

        private bool readyToClaim;                  // Update flag
        public List<DailyRewardUI> dailyRewardsUI = new List<DailyRewardUI>();

        private DailyRewards dailyRewards;
        int lang;

        void Awake()
        {
            dailyRewards = GetComponent<DailyRewards>();
        }

        void Start()
        {
            if (GameLanguage.Instance.GetLanguage() == "ru")
            {
                lang = 1;
            }
            else if (GameLanguage.Instance.GetLanguage() == "en")
            {
                lang = 0;
            }
            else
            {
                lang = 2;
            }
            InitializeDailyRewardsUI();

            if (panelDebug)
                panelDebug.SetActive(isDebug);


            buttonClaim.onClick.AddListener(() =>
            {
                Debug.Log("AddButton");
                dailyRewards.ClaimPrize();
                readyToClaim = false;
                UpdateUI();
            });

            buttonCloseReward.onClick.AddListener(() =>
            {
                var keepOpen = dailyRewards.keepOpen;
                panelReward.SetActive(false);
            });

            // Simulates the next Day
            if (buttonAdvanceDay)
                buttonAdvanceDay.onClick.AddListener(() =>
                {
                    dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0, 0));
                    UpdateUI();
                });

            // Simulates the next hour
            if (buttonAdvanceHour)
                buttonAdvanceHour.onClick.AddListener(() =>
                {
                    dailyRewards.debugTime = dailyRewards.debugTime.Add(new TimeSpan(1, 0, 0));
                    UpdateUI();
                });

            if (buttonReset)
                // Resets Daily Rewards from Player Preferences
                buttonReset.onClick.AddListener(() =>
                {
                    dailyRewards.Reset();
                    dailyRewards.debugTime = new TimeSpan();
                    dailyRewards.lastRewardTime = System.DateTime.MinValue;
                    readyToClaim = false;
                });
            if (buttonReloadScene)
                // Reloads the same scene
                buttonReloadScene.onClick.AddListener(() =>
                {
                    Application.LoadLevel(Application.loadedLevelName);
                });

            UpdateUI();
        }

        void OnEnable()
        {
            dailyRewards.onClaimPrize += OnClaimPrize;
            dailyRewards.onInitialize += OnInitialize;
        }

        void OnDisable()
        {
            if (dailyRewards != null)
            {
                dailyRewards.onClaimPrize -= OnClaimPrize;
                dailyRewards.onInitialize -= OnInitialize;
            }
        }

        // Initializes the UI List based on the rewards size
        private void InitializeDailyRewardsUI()
        {
            for (int i = 0; i < dailyRewards.rewards.Count; i++)
            {
                int day = i + 1;
                var reward = dailyRewards.GetReward(day);

                GameObject dailyRewardGo = GameObject.Instantiate(dailyRewardPrefab) as GameObject;

                DailyRewardUI dailyRewardUI = dailyRewardGo.GetComponent<DailyRewardUI>();
                dailyRewardUI.transform.SetParent(dailyRewardsGroup.transform);
                dailyRewardGo.transform.localScale = Vector2.one;

                dailyRewardUI.day = day;
                dailyRewardUI.reward = reward;
                dailyRewardUI.Initialize();

                dailyRewardsUI.Add(dailyRewardUI);

            }
        }

        public void UpdateUI()
        {
            dailyRewards.CheckRewards();

            bool isRewardAvailableNow = false;

            var lastReward = dailyRewards.lastReward;
            var availableReward = dailyRewards.availableReward;

            foreach (var dailyRewardUI in dailyRewardsUI)
            {
                var day = dailyRewardUI.day;

                if (day == availableReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_AVAILABLE;

                    isRewardAvailableNow = true;
                }
                else if (day <= lastReward)
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.CLAIMED;
                }
                else
                {
                    dailyRewardUI.state = DailyRewardUI.DailyRewardState.UNCLAIMED_UNAVAILABLE;
                }

                dailyRewardUI.Refresh();
            }
            if (isRewardAvailableNow)
            {
                SnapToReward();
                if (lang == 0)
                {
                    textTimeDue.text = "You can claim your reward!";
                }
                else if (lang == 1)
                {
                    textTimeDue.text = "Награда доступна!";
                }
                else if (lang == 2)
                {
                    textTimeDue.text = "Ödülünüzü talep edebilirsiniz!";
                }
            }
            readyToClaim = isRewardAvailableNow;
        }

        // Snap to the next reward
        public void SnapToReward()
        {
            Canvas.ForceUpdateCanvases();

            var lastRewardIdx = dailyRewards.lastReward;

            // Scrolls to the last reward element
            if (dailyRewardsUI.Count - 1 < lastRewardIdx)
                lastRewardIdx++;

            if (lastRewardIdx > dailyRewardsUI.Count - 1)
                lastRewardIdx = dailyRewardsUI.Count - 1;

            Debug.Log("Target = " + lastRewardIdx);
            Debug.Log("Massive = " + dailyRewardsUI.Count);
            if (lastRewardIdx < 0)
            {
                lastRewardIdx = 0;
                Debug.Log("Target = " + lastRewardIdx);
            }

            // var target = dailyRewardsUI[lastRewardIdx].GetComponent<RectTransform>();

            // var content = scrollRect.content;

            ////////content.anchoredPosition = (Vector2)scrollRect.transform.InverseTransformPoint(content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);

            // float normalizePosition = (float)target.GetSiblingIndex() / (float)content.transform.childCount;
            // scrollRect.verticalNormalizedPosition = normalizePosition;

        }

        void Update()
        {
            dailyRewards.TickTime();
            // Updates the time due
            CheckTimeDifference();
        }

        private void CheckTimeDifference()
        {
            if (!readyToClaim)
            {
                TimeSpan difference = dailyRewards.GetTimeDifference();

                // If the counter below 0 it means there is a new reward to claim
                if (difference.TotalSeconds <= 0)
                {
                    readyToClaim = true;
                    UpdateUI();
                    SnapToReward();
                    return;
                }

                string formattedTs = dailyRewards.GetFormattedTime(difference);
                if (lang == 0)
                {
                    textTimeDue.text = string.Format("Come back in {0} ", formattedTs);
                }
                else if (lang == 1)
                {
                    textTimeDue.text = string.Format("Возвращайтесь через {0} ", formattedTs);
                }
                else if (lang == 2)
                {
                    textTimeDue.text = string.Format("Bir sonraki ödülünüz için {0} ", formattedTs);
                }
            }
        }

        // Delegate
        private void OnClaimPrize(int day)
        { 
            panelReward.SetActive(true);

            var reward = dailyRewards.GetReward(day);
            var unit = reward.unit;
            var rewardQt = reward.reward;
            imageReward.sprite = reward.sprite;
            if (reward.unit == "Shotgun")
            {
                imageReward.enabled = false;
            }
            else
            {
                imageReward.enabled = true;
            }

            if (rewardQt > 0)
            {

                if (lang == 0)
                {
                    textReward.text = string.Format("You got {0} {1}!", reward.reward, unit);

                }
                else if (lang == 1)
                {
                    textReward.text = string.Format("Вы получили {0} {1}!", reward.reward, unit);
                }
                else if (lang == 2)
                {
                    textReward.text = string.Format("Sende {0} {1} var!", reward.reward, unit);
                }
            }
            else
            {
                if (lang == 0)
                {
                    textReward.text = string.Format("You got {0}!", reward.reward, unit);
                }
                else if (lang == 1)
                {
                    textReward.text = string.Format("Вы получили {0}!", reward.reward, unit);
                }
                else if (lang == 2)
                {
                    textReward.text = string.Format("Sende {0}var!", reward.reward, unit);
                }
            }
        }

        private void OnInitialize(bool error, string errorMessage)
        {
            if (!error)
            {
                var showWhenNotAvailable = dailyRewards.keepOpen;
                var isRewardAvailable = dailyRewards.availableReward > 0;

                UpdateUI();

                SnapToReward();
                CheckTimeDifference();
            }
        }
    }
}
