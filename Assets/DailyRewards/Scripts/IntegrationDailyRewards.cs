using UnityEngine;
using SamuraiGames;

/** 
 * This is just a snippet of code to integrate Daily Rewards into your project
 * 
 * Copy / Paste the code below
 **/
public class IntegrationDailyRewards : MonoBehaviour
{
    void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
		DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
       //This returns a Reward object
		Reward myReward = DailyRewards.instance.GetReward(day);
        if (myReward.Crystall == true)
        {
            Debug.Log("GetCrystall");
        }
        // And you can access any property
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count
        var rewardsCount =0;// = YandexGame.savesData.money;
		rewardsCount += myReward.reward;

		//YandexGame.savesData.money= rewardsCount;
        //YandexGame.SaveProgress();
		PlayerPrefs.Save ();
    }
}