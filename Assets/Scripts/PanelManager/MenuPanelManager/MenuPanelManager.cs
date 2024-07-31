using UnityEngine;

public class MenuPanelManager : MonoBehaviour
{
    public enum ActivePanel
    {
        None,
        Rewards,
        Donation,
        Skins,
        Race
    }
    [Header("Menu Panels")]
    [SerializeField][Tooltip("Panels of menu")] private GameObject rewardsPanel;
    [SerializeField][Tooltip("Panels of menu")] private GameObject donationPanel;
    [SerializeField][Tooltip("Panels of menu")] private GameObject skinsPanel;
    [SerializeField][Tooltip("Panels of menu")] private GameObject racePanel;

    [SerializeField] private ActivePanel defaultActivePanel = ActivePanel.None;

    private GameObject[] panels;
    [Header("Sounds")]
    [SerializeField] private AudioSource audioPressBTN;

    [Header("Player Exp. Slider")]
    [SerializeField] private GameObject expSlider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        panels = new GameObject[] { rewardsPanel, donationPanel, skinsPanel, racePanel };
        InitializePanels();
    }

    private void InitializePanels()
    {
        switch (defaultActivePanel)
        {
            case ActivePanel.Rewards:
                ShowRewardsPanel();
                break;
            case ActivePanel.Donation:
                ShowDonationPanel();
                break;
            case ActivePanel.Skins:
                ShowSkinsPanel();
                break;
            case ActivePanel.Race:
                ShowRacePanel();
                break;
            default:
                break;
        }
    }

    private void HideAllPanels()
    {
        foreach (var panel in panels)
        {
            panel.SetActive(false);
        }
    }

    public void PlayPressBTN()
    {
        audioPressBTN.Play();
    }

    public void ShowRewardsPanel()
    {
        HideAllPanels();
        rewardsPanel.SetActive(true);
        StateExpSlider(false);
    }

    public void ShowDonationPanel()
    {
        HideAllPanels();
        donationPanel.SetActive(true);
        StateExpSlider(false);
    }

    public void ShowSkinsPanel()
    {
        HideAllPanels();
        skinsPanel.SetActive(true);
        StateExpSlider(false);
    }

    public void ShowRacePanel()
    {
        HideAllPanels();
        racePanel.SetActive(true);
        StateExpSlider(true);
    }

    private void StateExpSlider(bool state)
    {
        expSlider.SetActive(state);
    }
}
