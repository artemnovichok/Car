using UnityEngine;

public class DonateInstance : MonoBehaviour
{
    [SerializeField] private Transform _content;
    private GameObject _donateItem;
    private void Start()
    {
        _donateItem = Resources.Load<GameObject>("Donate(Image)");
        for (int i = 0; i < ShopHolder.Instance.changeItem.Count; i++)
        {
            GameObject go = Instantiate(_donateItem, _content);
            go.GetComponent<DonateItemVisual>().Init(ShopHolder.Instance.changeItem[i]);
        }
        for (int i = 0; i < ShopHolder.Instance.donateItem.Count; i++)
        {
            GameObject go = Instantiate(_donateItem, _content);
            go.GetComponent<DonateItemVisual>().Init(ShopHolder.Instance.donateItem[i]);
        }
    }
}
