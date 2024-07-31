using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarsOnButtonCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _changeEvent;
    [SerializeField] private GameObject _first;
    [SerializeField] private GameObject _second;
    [SerializeField] private GameObject _third;

    private void OnEnable ()
    {
        _changeEvent.onClick.AddListener(setup);
    }
    
    private void OnDisable()
    {
        _changeEvent.onClick.RemoveListener(setup);
    }

    private void Start() => setup();

    public void setup()
    {
        int currentStars = 0;

        if (_first.activeInHierarchy)
        {
            currentStars = count(_first);
        }
        else if (_second.activeInHierarchy)
        {
            currentStars = count(_second);
        }
        else
        {
            currentStars = count(_third);
        }

        _text.text = currentStars + "/90";
    }

    private int count(GameObject go)
    {
        int stars = 0;
        int childs = go.transform.childCount;
        for (int i = 0; i < childs; i++)
        {
            stars += go.transform.GetChild(i).GetComponent<LevelSelectPlay>().GetStarsNum();
        }
        return stars;
    }

}
