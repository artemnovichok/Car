using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private const string TutorialShownKey = "TutorialShown";

    public enum TutorialState
    {
        Menu,
        Builder,
        Game
    }

    [Header("UI Elements")]
    [SerializeField] private GameObject tutorialBackground;
    [SerializeField] private GameObject pointerIcon;
    [SerializeField] private Button targetButton;

    [Header("Tutorial Settings")]
    [SerializeField] private TutorialState currentState;
    [SerializeField] private bool debugMode;
    [SerializeField] private bool turnOffTutorial;

    [Header("Builder Animation Settings")]
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Sprite newPointerIcon;

    [Header("Builder Animation Settings")]
    [SerializeField] private Timer timer;

    private Sprite originalPointerIcon;

    private void Start()
    {
        if (debugMode || !IsTutorialShown())
        {
            if(!turnOffTutorial)
            {
                Debug.Log(IsTutorialShown());
                ShowTutorial();
            }
        }
    }

    private bool IsTutorialShown()
    {
        return PlayerPrefs.GetInt(TutorialShownKey, 0) == 1;
    }

    private void SetTutorialShown()
    {
        PlayerPrefs.SetInt(TutorialShownKey, 1);
        PlayerPrefs.Save();
        Debug.Log("SAVE ACTIVATED");
    }

    private void ShowTutorial()
    {
        tutorialBackground.SetActive(true);
        pointerIcon.SetActive(true);
        originalPointerIcon = pointerIcon.GetComponent<Image>().sprite;
            StartCoroutine(PointerAnimation());

        if (currentState != TutorialState.Game)
        {
            targetButton.onClick.AddListener(OnTargetButtonClicked);
        }
    }

    private void OnTargetButtonClicked()
    {
       /* tutorialBackground.SetActive(false);
        pointerIcon.SetActive(false);
        if (!debugMode)
        {
            SetTutorialShown();
        }
        targetButton.onClick.RemoveListener(OnTargetButtonClicked);*/
    }

    private IEnumerator PointerAnimation()
    {
        switch (currentState)
        {
            case TutorialState.Menu:
                yield return MenuAnimation();
                break;
            case TutorialState.Builder:
                yield return BuilderAnimation();
                break;
            case TutorialState.Game:
                yield return GameAnimation();
                break;
        }
    }

    private IEnumerator MenuAnimation()
    {
        Vector3 startPos = pointerIcon.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 25, 0);
        float duration = 1.5f;

        for (int i = 0; i < 10; i++)
        {
            float elapsedTime = 0;

            while (elapsedTime < duration)
            {
                pointerIcon.transform.position = Vector3.Lerp(startPos, endPos, (Mathf.Sin(elapsedTime / duration * Mathf.PI * 2) + 1) / 2);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Vector3 temp = startPos;
            startPos = endPos;
            endPos = temp;
        }
    }

    private IEnumerator BuilderAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        pointerIcon.GetComponent<Image>().sprite = newPointerIcon;

        Vector3 startPos = pointerIcon.transform.position;
        Vector3 endPos = targetTransform.position;

        yield return new WaitForSeconds(0.5f);
        float elapsedTime = 0;
        float duration = 1f;
        while (elapsedTime < duration)
        {
            pointerIcon.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        pointerIcon.transform.position = startPos;
        pointerIcon.GetComponent<Image>().sprite = originalPointerIcon;
        yield return new WaitForSeconds(0.5f);
        pointerIcon.GetComponent<Image>().sprite = newPointerIcon;
        elapsedTime = 0;
        yield return new WaitForSeconds(0.5f);
        while (elapsedTime < duration)
        {
            pointerIcon.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        tutorialBackground.SetActive(false);
    }

    private IEnumerator GameAnimation()
    {
        SetTutorialShown();
        timer.enabled = false;

        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.6f);
            pointerIcon.GetComponent<Image>().sprite = newPointerIcon;
            pointerIcon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            yield return new WaitForSeconds(0.8f);
            pointerIcon.GetComponent<Image>().sprite = originalPointerIcon;
            pointerIcon.transform.localScale = Vector3.one;
        }
        tutorialBackground.SetActive(false);
        yield return new WaitForSeconds(4f);
        timer.enabled = true;
    }
}
