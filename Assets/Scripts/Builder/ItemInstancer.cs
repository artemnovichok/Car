using System.Collections;
using UnityEngine;

public class ItemInstancer : MonoBehaviour
{
    [Header("Ќаведитесь дл€ получени€ справки")]

    [Space(5f)]

    [Tooltip("“рансформ объекта content, нужен дл€ кода(к визуалу не относитс€)")]
    [SerializeField] private Transform _contentTransform;

    private void OnEnable() => EventManager.Instance.updateBlocks.AddListener(Start);
    private void OnDisable() => EventManager.Instance.updateBlocks.RemoveListener(Start);


    private void Start()
    {
        StartCoroutine(InitializeBlocks());
    }

    private IEnumerator InitializeBlocks()
    {
        ItemHolder.Instance.SetBlockOnLevel();
        yield return new WaitForEndOfFrame();

        //очищ€ем предыдущие объекты
        for (int i = 0; i < _contentTransform.childCount; i++)
        {
            Destroy(_contentTransform.GetChild(i).gameObject);
        }

        //ставим блок доставки если надо
        if (LevelEvent.Instance.Init() == EventType.Delivery)
        {
            GameObject goDelivery = Instantiate(ItemHolder.Instance.blockDelivery.Blocks[0].blockIconPrefab, _contentTransform);
            IconCharacteristic goDeliveryItI = goDelivery.GetComponent<IconCharacteristic>();
            goDeliveryItI.IsDonate = false;
            goDeliveryItI.WasBuy = true;
            goDeliveryItI.costToUnlock = 0;
            goDeliveryItI.ourRarity = BlockRarity.Grey;
            goDeliveryItI.ourId = 123123123; //кастомное id дл€ проверки. Ќ≈ ћ≈Ќя“№ » Ќ≈ ”ƒјЋя“№!
            goDeliveryItI.NotInfinityBlock = true;
            goDeliveryItI.StartCustom();
        }

        //ставим открытые блоки
        for (int i = 0; i < ItemHolder.Instance.GetListBoughtBloksLength(); i++)
        {
            GameObject go = Instantiate(ItemHolder.Instance.GetBoughtBlok(i).blockIconPrefab, _contentTransform);
            IconCharacteristic goItI = go.GetComponent<IconCharacteristic>();
            goItI.IsDonate = ItemHolder.Instance.GetBoughtBlok(i).IsDonateBlock;
            goItI.WasBuy = ItemHolder.Instance.GetBoughtBlok(i).IsWasBuy;
            goItI.costToUnlock = ItemHolder.Instance.GetBoughtBlok(i).CostToUnlock;
            goItI.ourRarity = ItemHolder.Instance.GetBoughtBlok(i).Rarity;
            goItI.ourId = ItemHolder.Instance.GetBoughtBlok(i).ourID;
            goItI.NotInfinityBlock = ItemHolder.Instance.GetBoughtBlok(i).NotInfinityBlock;
            goItI.StartCustom();
        }

        //ставим не открытые блоки, но доступные к покупке
        for (int i = 0; i < ItemHolder.Instance.GetListAvailableBloksLength(); i++)
        {
            GameObject goA = Instantiate(ItemHolder.Instance.GetAvailableBlok(i).blockIconPrefab, _contentTransform);
            IconCharacteristic goAItI = goA.GetComponent<IconCharacteristic>();
            goAItI.IsDonate = ItemHolder.Instance.GetAvailableBlok(i).IsDonateBlock;
            goAItI.WasBuy = ItemHolder.Instance.GetAvailableBlok(i).IsWasBuy;
            goAItI.costToUnlock = ItemHolder.Instance.GetAvailableBlok(i).CostToUnlock;
            goAItI.ourRarity = ItemHolder.Instance.GetAvailableBlok(i).Rarity;
            goAItI.ourId = ItemHolder.Instance.GetAvailableBlok(i).ourID;
            goAItI.NotInfinityBlock = ItemHolder.Instance.GetAvailableBlok(i).NotInfinityBlock;
            goAItI.StartCustom();
        }

        //ставим остальные блоки
        for (int i = 0; i < ItemHolder.Instance.NotAvaliableBlock.Count; i++)
        {
            GameObject goNotA = Instantiate(ItemHolder.Instance.NotAvaliableBlock[i].blockIconPrefab, _contentTransform);
            IconCharacteristic goAItI = goNotA.GetComponent<IconCharacteristic>();
            goAItI.StartCustomNotAvaliableBlock(ItemHolder.Instance.NotAvaliableBlock[i].LevelToUnlock);
        }
    }
}
