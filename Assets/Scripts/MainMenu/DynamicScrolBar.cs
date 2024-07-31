using System.IO;
using UnityEditorInternal.VR;
using UnityEngine;

public class DynamicScrolBar : MonoBehaviour
{
    [SerializeField] private LevelType lvlType = LevelType.first;
    private void Start()
    {
        float height = gameObject.GetComponent<RectTransform>().rect.height;
        string path = string.Empty;
        switch (lvlType)
        {
            case LevelType.first:
                path = "1levels";
                break;
            case LevelType.second:
                path = "2levels";
                break;
            case LevelType.third:
                path = "3levels";
                break;
        }
        int levelsCount = CountObjectsInFolder("Resources/Levels/" + path);

        int linesAvaliable = (int)height / 500;

        int lines = Mathf.CeilToInt((float)levelsCount / 4);

        if (linesAvaliable < lines)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, gameObject.GetComponent<RectTransform>().rect.height + 200 * (lines - linesAvaliable));
        }
    }

    int CountObjectsInFolder(string folderPath)
    {
        int count = 0;
        string[] files = Directory.GetFiles(Application.dataPath + "/" + folderPath);

        foreach (string file in files)
        {
            if (file.EndsWith(".prefab"))
            {
                count++;
            }
        }

        return count;
    }
}
