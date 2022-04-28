using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapTerrainEditor : MonoBehaviour
{
    public CellParameter parameter;

    public LeanPoolBridge lean;
    /// <summary>
    /// cells生成的root
    /// </summary>
    public Transform root;
    /// <summary>
    /// cube檔案庫
    /// </summary>
    public CubesDataFile file;
    /// <summary>
    /// 對應的cube位置二維陣列
    /// </summary>
    private bool[,] _cellArray = new bool[10, 10];
    /// <summary>
    /// 寫座標用而已
    /// </summary>
    public CubeBaseInfo[,] cubes;
    /// <summary>
    /// 暫存單筆地形資料
    /// </summary>
    private TerrianSize _tempTerrainType = new TerrianSize();


    // Start is called before the first frame update
    void Start()
    {

        

    }
    /// <summary>
    /// 更新cells對於地圖的絕對位置到資料庫
    /// </summary>
    public void UpdateCellsData() 
    {
        file.Refresh();

        cubes = new CubeBaseInfo[100, 100];
        int cellXMax = cubes.GetLength(0);
        int cellYMax = cubes.GetLength(1);

        for (int i = 0; i < cellXMax; ++i) 
        {
            for (int j = 0; j < cellYMax; ++j) 
            {
                cubes[i ,j] = new CubeBaseInfo();
                cubes[i, j].x = i;
                cubes[i, j].y = j;
                var cube = lean.Spawn("MapCube", transform.position + new Vector3(i, 0f, j), root);
                var scr = cube.GetComponent<SingleCubeCtrl>();
                scr.cellX = cubes[i, j].x;
                scr.cellY = cubes[i, j].y;
                scr.cube = cube;
                file.Save(scr);
            }
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 地形初始化
    /// </summary>
    public void Init()
    {
        UpdateCellsData();
    }
    /// <summary>
    /// 造山
    /// </summary>
    public void MakeMountain() 
    {
        //挑點
        int pointX = Random.Range(0, 100);
        int pointY = Random.Range(0, 100);
        //挑地形
        var index = Random.Range(1, 4);
        TerrainType(index);

        TryBuild(pointX, pointY);
    }
    /// <summary>
    /// 挑地形
    /// </summary>
    /// <param name="index"></param>
    void TerrainType(int index) 
    {
        _tempTerrainType.x = Random.Range(1, 20);
        _tempTerrainType.y = Random.Range(1, 20);

        ///此類地形的範圍
        switch (index) 
        {
            case 0:
                Debug.Log("Select Terrain Type = 0");
                break;
            case 1:
                _tempTerrainType.type = "M";
                //_tempTerrainType.x = 10;
                //_tempTerrainType.y = 7;
                break;
            case 2:
                _tempTerrainType.type = "U";
                //_tempTerrainType.x = 10;
                //_tempTerrainType.y = 5;
                break;
            case 3:
                _tempTerrainType.type = "F";
                _tempTerrainType.x = 3;
                _tempTerrainType.y = 5;
                break;
        }
    }

    /// <summary>
    /// Check the area is clean for building
    /// </summary>
    /// <param name="pointX"></param>
    /// <param name="pointY"></param>
    void TryBuild(int pointX, int pointY) 
    {
        bool _isOccupied = false;

        ///先判斷這個範圍可不可以建該地形
        for (int i = pointX; i < pointX + _tempTerrainType.x; i++)
        {
            for (int j = pointY; j < pointY + _tempTerrainType.y; j++)
            {
                if (!file.CubeStatus(i, j))
                {
                    Debug.LogFormat("{0},{1} is empty",i,j);
                }
                else
                {
                    _isOccupied = true;
                    Debug.Log("can't do this");
                }


            }
        }
        if (!_isOccupied) 
        {

            for (int i = pointX; i < pointX + _tempTerrainType.x; i++) 
            {
                for (int j = pointY; j < pointY + _tempTerrainType.y; j++) 
                {
                    switch (_tempTerrainType.type) 
                    {
                        case "M":
                            int h = Random.Range(0, 15);
                            file.Load(i, j).HeightAdjust(h);
                            file.Load(i, j).status = true;
                            break;
                        case "F":
                            file.Load(i, j).HeightAdjust(3);
                            file.Load(i, j).status = true;
                            break;
                        case "U":
                            file.Load(i, j).DigAction(1);
                            file.Load(i, j).status = true;
                            break;
                    }
                    file.Load(i, j).type = _tempTerrainType.type;

                }
            }
        }


    }
    [ContextMenu("color up")]
    public void ColorUp() 
    {
        for (int i = 0; i < 100; i++) 
        {
            for (int j = 0; j < 100; j++) 
            {
                file.Load(i, j).ApplyMaterial(parameter.GetMaterial(file.Load(i, j).type));
            }
        }
    }
    

    
}
