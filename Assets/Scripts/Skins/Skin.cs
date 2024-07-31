using System;
using UnityEngine;

[Serializable]
public struct Skin
{
    [Tooltip("Системное, не трогать")]
    public int Id;
    [Tooltip("Имя на русском")]
    public string NameRu;
    [Tooltip("Имя на английском")]
    public string NameEn;
    [Tooltip("Имя на турецком")]
    public string NameTr;
    [Tooltip("Купленео ли")]
    public bool IsUnlock;
    [Tooltip("Донатный ли блок")]
    public bool IsDonate;
    [Tooltip("Блок ли за даймонды")]
    public bool IsDimond;
    [Tooltip("Цена на разблокировку")]
    public int Cost;
}
