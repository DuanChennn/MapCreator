using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System.Linq;
public class LeanPoolBridge : MonoBehaviour
{
    /// <summary>
    /// ????W?????index??????
    /// </summary>
    //public LeanPoolItemParameter parameter = null;
    /// <summary>
    /// ??t?C???X??
    /// </summary>
    //public LeanGameObjectPool [] leanPool;
    public List<LeanGameObjectPool> leanPool = new List<LeanGameObjectPool>();
    /// <summary>
    /// ???????parent
    /// </summary>
    public Transform root;
    /// <summary>
    /// index
    /// </summary>
    //private int _temp = -1;
    // Start is called before the first frame update
    void Start()
    {
        leanPool.Clear();
        var children = this.GetComponentsInChildren<LeanGameObjectPool>();
        foreach (var child in children)
        {
            leanPool.Add(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// ??????W????
    /// </summary>
    /// <param name="prefabName">????W??</param>
    public void SpawnByName(string prefabName)
    {
        //_temp =parameter.GetIndex(prefabName);
        leanPool.FirstOrDefault(r => r.Prefab.name == prefabName).Spawn();//   [_temp].Spawn();
    }

    /// <summary>
    /// ?????w??m????????W??????k
    /// </summary>
    /// <param name="prefabName"></param>
    /// <param name="root"></param>
    public void SpawnByName(string prefabName, Transform root)
    {
        //_temp = parameter.GetIndex(prefabName);
        //leanPool[_temp].Spawn(root);
        leanPool.FirstOrDefault(r => r.Prefab.name == prefabName).Spawn(root);
    }

    /// <summary>
    /// ?k?????P?@??lean gameobject pool??????
    /// </summary>
    /// <param name="prefabName"></param>
    public void DespawnAll(string prefabName)
    {
        //_temp = parameter.GetIndex(prefabName);
        //leanPool[_temp].DespawnAll();
        leanPool.FirstOrDefault(r => r.Prefab.name == prefabName).DespawnAll();
    }

    public GameObject Spawn(string prefabName)
    {
        Debug.Log("leanPool count:" + leanPool.Count);
        var pool = leanPool.FirstOrDefault(r => r.Prefab.name == prefabName);
        if (pool != null)
        {
            GameObject obj = leanPool.FirstOrDefault(r => r.Prefab.name == prefabName).Spawn(root, false);
            return obj;
        }
        else
        {
            Debug.LogErrorFormat("[Error] LeanPoolM Spawn error prefabName = {0}", prefabName);
            return null;
        }
    }



    public GameObject Spawn(string prefabName, Vector3 position, Transform parent)
    {
        //_temp = parameter.GetIndex(prefabName);
        GameObject obj = leanPool.FirstOrDefault(r => r.Prefab.name == prefabName).Spawn(position, Quaternion.identity, parent);
        return obj;
    }

    public void Despawn(GameObject clone)
    {
        //_temp = parameter.GetIndex(clone.gameObject.name);
        //leanPool[_temp].Despawn(clone,0f);
        Debug.Log("clone =" + clone.name);
        leanPool.FirstOrDefault(r => r.Prefab.name == clone.name).Despawn(clone, 0f);
    }



}
