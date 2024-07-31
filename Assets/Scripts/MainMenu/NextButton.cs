using UnityEngine;
using UnityEngine.SceneManagement;

public class NextButton : MonoBehaviour
{
    [SerializeField] private GameObject _firstMode;
    [SerializeField] private GameObject _secondMode;
    [SerializeField] private GameObject _thirdMode;
    public void StartLastLevel()
    {
        LevelData lastLevel = new();
        if (_firstMode.activeInHierarchy)
        {
            for (int i = 0; i < _firstMode.transform.childCount; i++)
            {
                LevelData compOnButton = _firstMode.transform.GetChild(i).gameObject.GetComponent<LevelSelectPlay>().GetLevelData();
                if (compOnButton.isUnlock)
                {
                    lastLevel = compOnButton;
                }
            }
        }
        if (_secondMode.activeInHierarchy)
        {
            for (int i = 0; i < _secondMode.transform.childCount; i++)
            {
                LevelData compOnButton = _secondMode.transform.GetChild(i).gameObject.GetComponent<LevelSelectPlay>().GetLevelData();
                if (compOnButton.isUnlock)
                {
                    lastLevel = compOnButton;
                }
            }
        }
        if (_thirdMode.activeInHierarchy)
        {
            for (int i = 0; i < _thirdMode.transform.childCount; i++)
            {
                LevelData compOnButton = _thirdMode.transform.GetChild(i).gameObject.GetComponent<LevelSelectPlay>().GetLevelData();
                if (compOnButton.isUnlock)
                {
                    lastLevel = compOnButton;
                }
            }
        }
        PlayerPrefs.SetInt("numLevelWasSelected", lastLevel.id);
        PlayerPrefs.SetInt("LvlTypeWasSelected", (int)lastLevel.type);
        SceneManager.LoadScene(1);
    }
}
