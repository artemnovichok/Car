using UnityEngine;

[System.Serializable]
public class GradientColors
{
    public Color topColor;
    public Color bottomColor;
}

public class BackgroundChanger : MonoBehaviour
{
    [Header("3D Cubes")]
    public GameObject[] cubes;

    [Header("Shader Material")]
    public Material material;
    [Header("Gradient Colors by Level Range")]
    public GradientColors[] gradientColors;

    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("numLevelWasSelected");
        int colorIndex = GetColorIndexByLevel(currentLevel);
        if (material != null && colorIndex < gradientColors.Length)
        {
            material.SetColor("_TopColor", gradientColors[colorIndex].topColor);
            material.SetColor("_BottomColor", gradientColors[colorIndex].bottomColor);
            foreach (var cube in cubes)
            {
                Renderer renderer = cube.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = material;
                }
                else
                {
                    Debug.LogError("Renderer не найден на объекте " + cube.name);
                }
            }
        }
        else
        {
            Debug.LogError("Материал или градиентные цвета не заданы корректно!");
        }
    }

    int GetColorIndexByLevel(int level)
    {
        if (level >= 0 && level <= 10)
        {
            return 0;
        }
        else if (level >= 11 && level <= 20)
        {
            return 1;
        }
        else if (level >= 21 && level <= 30)
        {
            return 2;
        }


        else if (level >= 31 && level <= 40)
        {
            return 3;
        }
        else if (level >= 41 && level <= 51)
        {
            return 4;
        }
        else if (level >= 51 && level <= 60)
        {
            return 5;
        }


        else if (level >= 61 && level <= 70)
        {
            return 6;
        }
        else if (level >= 71 && level <= 80)
        {
            return 7;
        }
        else if (level >= 81 && level <= 90)
        {
            return 8;
        }


        else
        {
            return 0;
        }
    }
}