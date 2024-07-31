using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* 
 * Daily Reward Object UI representation
 */
namespace SamuraiGames
{
    /** 
     * The UI Representation of a Daily Reward.
     * 
     *  There are 3 states:
     *  
     *  1. Unclaimed and available:
     *  - Shows the Color Claimed
     *  
     *  2. Unclaimed and Unavailable
     *  - Shows the Color Default
     *  
     *  3. Claimed
     *  - Shows the Color Claimed
     *  
     **/
    public class DailyRewardUI : MonoBehaviour
    {
        public bool showRewardName;

        [Header("UI Elements")]
        public TextMeshProUGUI textDay;                // Text containing the Day text eg. Day 12
        public TextMeshProUGUI textReward;             // The Text containing the Reward amount
        public Image imageRewardBackground; // The Reward Image Background
        public Image imageReward;           // The Reward Image
        public Sprite UnclaimBackground;
        public Sprite ClaimBackground;
        public GameObject checkMark;
        [Header("Internal")]
        public int day;

        [HideInInspector]
        public Reward reward;

        public DailyRewardState state;

        // The States a reward can have
        public enum DailyRewardState
        {
            UNCLAIMED_AVAILABLE,
            UNCLAIMED_UNAVAILABLE,
            CLAIMED
        }

        public void Initialize()
        {
            int lang;
            if (GameLanguage.Instance.GetLanguage() == "ru")
            {
                lang = 1;
            }
            else if(GameLanguage.Instance.GetLanguage() == "en")
            {
                lang = 0;
            }
            else
            {
                lang = 2;
            }
            if (lang == 0)
            {
                textDay.text = string.Format("Day {0}", day.ToString());
            }
            else if (lang == 1)
            {
                textDay.text = string.Format("День {0}", day.ToString());
            }
            else if (lang == 2)
            {
                textDay.text = string.Format("Gün {0}", day.ToString());
            }
            
            if (reward.reward > 0)
            {
                if (showRewardName)
                {
                    
                }
                else
                {
                    textReward.text = reward.reward.ToString();
                }
            }
            else
            {
                textReward.text = reward.unit.ToString();                              
            }
            imageReward.sprite = reward.sprite;
        }

        // Refreshes the UI
        public void Refresh()
        {
            switch (state)
            {
                case DailyRewardState.UNCLAIMED_AVAILABLE:

                    imageRewardBackground.sprite = ClaimBackground;
                    checkMark.SetActive(false);
                    break;
                case DailyRewardState.UNCLAIMED_UNAVAILABLE:
                    imageRewardBackground.sprite = UnclaimBackground;
                    checkMark.SetActive(false);
                    break;
                case DailyRewardState.CLAIMED:
                    imageRewardBackground.sprite = ClaimBackground;
                    checkMark.SetActive(true);
                    break;
            }
        }
    }
}