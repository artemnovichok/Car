using System.IO;
using UnityEngine;

public class InstanceLevel : MonoBehaviour
{
    [Header("Нивидимая штука нужна только для кода")]
    [SerializeField] private RectTransform contentFirstMode;
    [SerializeField] private RectTransform contentSecondMode;
    [SerializeField] private RectTransform contentThirdMode;
    [SerializeField] private StarsOnButtonCounter textCounter;

    private GameObject levelButton;

    private void Start()
    {
        levelButton = Resources.Load<GameObject>("LevelButton(Btn)");
        for (int i = 0; i < CountObjectsInFolder("Resources/Levels/1levels"); i++)
        {
            GameObject gb = Instantiate(levelButton, contentFirstMode.transform);
            if (gb.TryGetComponent<LevelSelectPlay>(out LevelSelectPlay component))
            {
                component.Inicialize(i, LevelType.first);
            }
        }
        for (int i = 0; i < CountObjectsInFolder("Resources/Levels/2levels"); i++)
        {
            GameObject gb = Instantiate(levelButton, contentSecondMode.transform);
            if (gb.TryGetComponent<LevelSelectPlay>(out LevelSelectPlay component))
            {
                component.Inicialize(i, LevelType.second);
            }
        }
        for (int i = 0; i < CountObjectsInFolder("Resources/Levels/3levels"); i++)
        {
            GameObject gb = Instantiate(levelButton, contentThirdMode.transform);
            if (gb.TryGetComponent<LevelSelectPlay>(out LevelSelectPlay component))
            {
                component.Inicialize(i, LevelType.third);
            }
        }
        textCounter.setup();
    }

    private int CountObjectsInFolder(string folderPath)
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
