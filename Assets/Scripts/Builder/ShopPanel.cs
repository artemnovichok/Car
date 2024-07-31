using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject PanelShop;

    private bool close = true;

    public void PanelShopState()
    {
        if(close)
        {
            PanelShop.SetActive(true);
        }
        else if(!close)
        {
            PanelShop.SetActive(false);
        }
        close = !close;
    }
}
