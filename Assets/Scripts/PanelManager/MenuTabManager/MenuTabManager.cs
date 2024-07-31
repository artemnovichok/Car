using UnityEngine;
using UnityEngine.UI;

public class MenuTabManager : MonoBehaviour
{
    public enum TabType { AWARDS, SHOP, SKINS, RACE }

    [System.Serializable]
    public class Tab
    {
        public TabType tabType;
        public Button button;
        public GameObject background;
    }

    [Header("Fields for BTN")]
    public Tab[] tabs;
    [Header("Active BTN on start")]
    [SerializeField] private TabType defaultTab;

    void Start()
    {
        foreach (var tab in tabs)
        {
            tab.background.SetActive(false);
            tab.button.onClick.AddListener(() => OnTabSelected(tab));
        }
        Tab defaultTabInstance = System.Array.Find(tabs, tab => tab.tabType == defaultTab);
        if (defaultTabInstance != null)
        {
            OnTabSelected(defaultTabInstance);
        }
    }

    void OnTabSelected(Tab selectedTab)
    {
        foreach (var tab in tabs)
        {
            tab.background.SetActive(tab == selectedTab);
        }
    }
    public void ActivateShopTab()
    {
        Tab shopTab = System.Array.Find(tabs, tab => tab.tabType == TabType.SHOP);
        if (shopTab != null)
        {
            OnTabSelected(shopTab);
        }
    }
}
