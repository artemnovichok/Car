//using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
    }

    public void startGame()
    {
        bool wasEngineFind = false;
        bool wasDeliverFind = false;
        foreach(GameObject go in GameObject.Find("CellsHolder").gameObject.GetComponent<GridSectionsHolder>().cells) //не запускаем игру, если нет кабины
        {
            if(go.GetComponent<CellSetup>().cellSettingHolder.IsBlockHere)
            {
                if(go.GetComponent<CellSetup>().cellSettingHolder.BlockID == 0)
                {
                    wasEngineFind = true;
                }
                if (go.GetComponent<CellSetup>().cellSettingHolder.BlockID == 123123123)
                {
                    wasDeliverFind = true;
                }
            }
        }
        if (wasEngineFind)
        {
            if (LevelEvent.Instance.Init() == EventType.Delivery)
            {
                if (wasDeliverFind)
                {
                    PlayerPrefs.SetInt("TimesFailed", 0);
                    GameObject.Find("CellsHolder").gameObject.GetComponent<GridSectionsHolder>().SaveBlocksInGrid();
                    //FirebaseAnalytics.LogEvent("building_car_time", new Parameter("timeInBuld", (int)time));
                    //FirebaseAnalytics.LogEvent("players_entered_level");
                    SceneManager.LoadScene(2);
                }
                else
                {
                    //FirebaseAnalytics.LogEvent("failed_start_level");
                }
            }
            else
            {
                PlayerPrefs.SetInt("TimesFailed", 0);
                GameObject.Find("CellsHolder").gameObject.GetComponent<GridSectionsHolder>().SaveBlocksInGrid();
                //FirebaseAnalytics.LogEvent("building_car_time", new Parameter("timeInBuld", (int)time));
                //FirebaseAnalytics.LogEvent("players_entered_level");
                SceneManager.LoadScene(2);
            }
        }
        else
        {
            //FirebaseAnalytics.LogEvent("failed_start_level");
        }
    }
}
