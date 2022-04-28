using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CubesDataFile : MonoBehaviour
{
    
    public List<SingleCubeCtrl> cubeScrs = new List<SingleCubeCtrl>();

    public void Refresh()
    {
        cubeScrs.Clear();
    }

    public void Save(SingleCubeCtrl scr) 
    {
        cubeScrs.Add(scr);
    }


    public SingleCubeCtrl Load(int x, int y) 
    {
        return cubeScrs.FirstOrDefault(r => r.cellX == x && r.cellY == y);
    }

    /// <summary>
    /// check this cube if already gets terrian setting
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool CubeStatus(int x, int y)
    {
        if (cubeScrs.FirstOrDefault(r => r.cellX == x && r.cellY == y) != null)
            return cubeScrs.FirstOrDefault(r => r.cellX == x && r.cellY == y).status;
        else
            Debug.LogFormat("null ref at {0},{1}",x,y);
            return true;
    }


    /// <summary>
    /// Delete specific data
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Purge(int x, int y) 
    {
        cubeScrs.Remove(cubeScrs.FirstOrDefault(r => r.cellX == x && r.cellY == y));
    }





    [ContextMenu("FileData")]
    void CheckData()
    {
        foreach (var data in cubeScrs)
        {
            Debug.LogFormat("{0},{1},{2},{3}",data.cellX,data.cellY,data.type,data.baseH);
        }
    }
}
