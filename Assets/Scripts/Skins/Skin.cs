using System;
using UnityEngine;

[Serializable]
public struct Skin
{
    [Tooltip("���������, �� �������")]
    public int Id;
    [Tooltip("��� �� �������")]
    public string NameRu;
    [Tooltip("��� �� ����������")]
    public string NameEn;
    [Tooltip("��� �� ��������")]
    public string NameTr;
    [Tooltip("�������� ��")]
    public bool IsUnlock;
    [Tooltip("�������� �� ����")]
    public bool IsDonate;
    [Tooltip("���� �� �� ��������")]
    public bool IsDimond;
    [Tooltip("���� �� �������������")]
    public int Cost;
}
