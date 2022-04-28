using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBaseInfo 
{
    public int x = -1;
    public int y = -1;

    //地形編輯之後才寫入的資訊 並給出這些數值
    public SingleCubeCtrl scr;
    public int chunkIndex = -1;
    public int height = -1;
}

public class ChunkBaseInfo
{
    public int mapX = -1;
    public int mapZ = -1;
    public int index = -1;
    public int x = -1;
    public int y = -1;
    public int z = -1;
}

public class MapBaseInfo 
{
    public int x = -1;
    public int y = -1;
    public int z = -1;
}

public class TerrianSize 
{
    public string type = string.Empty;
    public int x = -1;
    public int y = -1;
    //public int z = -1;
}