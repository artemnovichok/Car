using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;
    private float maxTime = 4f;
    private float time;
    private bool needTimer;

    private void Start()
    {
        time = maxTime;
    }
    private void Update()
    {
        if (needTimer)
        {
            time -= Time.deltaTime;
            _timerText.text = ((int)time).ToString();
            if(time <= 0f)
            {
                EventManager.Instance.CantMove();
                Time.timeScale = 0f;
                needTimer = false;
                _timerText.enabled = false;
                EventManager.Instance.GameOver();
            }
        }
    }

    public bool NeedTimer
    {
        get
        {
            return needTimer;
        }
        set
        {
            needTimer = value;
            if (!needTimer)
            {
                time = maxTime;
                _timerText.enabled = false;
            }
            else
            {
                if(_timerText.enabled == false)
                {
                    _timerText.enabled = true;
                }
            }
        }
    }
}
