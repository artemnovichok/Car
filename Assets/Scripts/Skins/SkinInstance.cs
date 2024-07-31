using UnityEngine;

public class SkinInstance : MonoBehaviour
{
    [SerializeField] private Transform _content;
    private GameObject _goPrefab;

    private void OnEnable()
    {
        EventManager.Instance.updateSkin.AddListener(Instance);
    }

    private void OnDisable()
    {
        EventManager.Instance.updateSkin.RemoveListener(Instance);
    }

    private void Start()
    {
        
        _goPrefab = Resources.Load<GameObject>("SkinBack(Image)");
        Instance();
    }

    private void Instance()
    {
        for (int i = 0; i < _content.childCount; i++)
        {
            Destroy(_content.GetChild(i).gameObject);
        }
        for (int i = 0; i < ItemHolder.Instance.skins.skin.Length; i++)
        {
            GameObject go = Instantiate(_goPrefab, _content);
            SkinSetup skSetup = go.GetComponent<SkinSetup>();
            skSetup.Init(ItemHolder.Instance.skins.skin[i]);
            if (ItemHolder.Instance.skins.CurrentSkinId == skSetup.GetID())
            {
                skSetup.Select();
            }
        }
    }
}
