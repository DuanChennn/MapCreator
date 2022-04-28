using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCubeCtrl : MonoBehaviour
{
    public int cellX;
    public int cellY;
    public GameObject cube;
    /// <summary>
    /// cube資料庫
    /// </summary>
    public CubesDataFile file;
    /// <summary>
    /// Lean pool
    /// </summary>
    public LeanPoolBridge lean;
    /// <summary>
    /// 是否已有地形
    /// </summary>
    public bool status = false;
    /// <summary>
    /// 地形的類型
    /// </summary>
    public string type = string.Empty;
    public int baseH = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public CubeBaseInfo info = new CubeBaseInfo();
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="cubeInfo"></param>
    public void Init(CubeBaseInfo cubeInfo)
    {
        cube = this.gameObject;
        ///目前只得到chunkIndex
        info = cubeInfo;
    }

    /// <summary>
    /// Save to the file
    /// </summary>
    public void Save() 
    {
        info.x = cellX;
        info.y = cellY;
        
        file.Save(this);
    }

    public void Save(CubeBaseInfo data) 
    {
        cellX = data.x;
        cellY = data.y;
    }

    public void HeightAdjust(int h) 
    {
        if (h == 0)
            return;
        for (int i = 0; i < h; i++) 
        {
            lean.Spawn("MapCube", GetVecBase(i), this.transform);
        }

        SaveHeight(); 
    }

    Vector3 GetVecBase(int times) 
    {
        float myHeight = this.gameObject.GetComponent<BoxCollider>().size.y;
        Vector3 vec = new Vector3(transform.position.x, transform.position.y + myHeight*times, transform.position.z);
        return vec;
    }

    public void Clear()
    {
        file.Purge(info.x,info.y);
    }

    public void DigAction(int d) 
    {
        var children = cube.GetComponentsInChildren<SingleCubeCtrl>();
        if (children.Length > 1)
        {
            for (int i = 0; i < d; i++)
            {
                children[i].Despawn();
            }
        }
        else 
        {
            Debug.Log("nothing to dig");
        }


        SaveHeight();
    }

    public void Despawn() 
    {
        lean.Despawn(cube);
    }

    void SaveHeight() 
    {
        baseH = cube.GetComponentsInChildren<SingleCubeCtrl>().Length;
    }

    public void ApplyMaterial(Material m) 
    {
        GetComponent<MeshRenderer>().materials[0] = m;
    }

}
