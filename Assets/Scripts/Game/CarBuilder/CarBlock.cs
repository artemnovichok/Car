using System;

[Serializable]
public struct CarBlock
{
    public int id;
    public BlockRarity ourRarity;
    public int hpMax;
    public bool weConnectUp;
    public bool weConnectDown;
    public bool weConnectLeft;
    public bool weConnectRight;
}