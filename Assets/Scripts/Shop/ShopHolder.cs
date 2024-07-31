using System.Collections.Generic;
using UnityEngine;

public class ShopHolder : MonoBehaviour
{
    public static ShopHolder Instance { get; private set; }

    [Header("Shop")]
    [Space(5f)]
    [Header("Donate")]
    public List<DonateItem> donateItem = new();
    [Space(5f)]
    [Header("Change")]
    public List<DonateItem> changeItem = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
