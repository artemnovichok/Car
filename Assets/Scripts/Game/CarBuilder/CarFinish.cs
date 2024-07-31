using UnityEngine;

public class CarFinish : MonoBehaviour
{
    private GameObject _openedPanel;
    private AudioSource finishAudioSource;

    private void Start()
    {
        GameObject audioSourceObject = GameObject.FindGameObjectWithTag("FinishSound");
        if (audioSourceObject != null)
        {
            finishAudioSource = audioSourceObject.GetComponent<AudioSource>();
        }
        else
        {
            Debug.LogWarning("AudioSource with tag 'AudioSource' not found.");
        }
    }

    private void OnEnable()
    {
        EventManager.Instance.finishLevel.AddListener(Finish);
    }

    private void OnDisable()
    {
        EventManager.Instance.finishLevel.RemoveListener(Finish);
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Finish"))
        {
            EventManager.Instance.CantMove();
            if (PanelManager.Instance.TryOpenFinishPanel(out GameObject panel))
            {
                Time.timeScale = 0f;
                _openedPanel = panel;

                if (finishAudioSource != null)
                {
                    finishAudioSource.Play();
                }
                else
                {
                    Debug.LogWarning("No AudioSource component found to play the finish sound.");
                }
            }
            else
            {
                Debug.Log("Panel is either already open or some other issue occurred.");
            }
        }
    }

    private void Finish()
    {
        Time.timeScale = 1f;
        Destroy(_openedPanel);
    }
}
