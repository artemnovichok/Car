using UnityEngine;

public class SpawnLevel : MonoBehaviour
{
    [Header("Tag and Layers")]
    [SerializeField] private string _tag = "";
    [SerializeField] private string _Layer = "";

    private void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("numLevelWasSelected");
        string fold = PlayerPrefs.GetInt("LvlTypeWasSelected") == 0 ? "1levels" : PlayerPrefs.GetInt("LvlTypeWasSelected") == 1 ? "2levels" : "3levels";
        string path = "Levels/" + fold + "/Level" + (currentLevel + 1) + "Path";
        print(path);
        GameObject levelPrefab = Resources.Load<GameObject>(path);
        GameObject instantiatedLevel = Instantiate(levelPrefab, new Vector3(-10, -10, 2), Quaternion.Euler(0, 90, 0));
        SetTagAndLayer(instantiatedLevel, _tag, _Layer);
    }

    private void SetTagAndLayer(GameObject obj, string tag, string layerName)
    {
        if (obj.name != "Finish")
        {
            obj.tag = tag;
            obj.layer = LayerMask.NameToLayer(layerName);
        }
        foreach (Transform child in obj.GetComponentsInChildren<Transform>(true))
        {
            if (child.gameObject.name != "Finish")
            {
                child.gameObject.tag = tag;
                child.gameObject.layer = LayerMask.NameToLayer(layerName);
            }
        }
    }
}
