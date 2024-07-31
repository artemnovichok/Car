using System;

[Serializable]
public struct CellSettingHolder
{
    public float CellPosX;
    public float CellPosY;
    public bool IsBlockHere;
    public int BlockID;
    public int BlocRarity;
    public int Rotate;
    public bool ConnectUp;
    public bool ConnectDown;
    public bool ConnectLeft;
    public bool ConnectRight;
}