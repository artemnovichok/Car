using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _coin = 1;

    public int GetCoinValue()
    {
        return _coin;
    }

    public void SetCoinValue(int value)
    {
        _coin = value;
    }
}
