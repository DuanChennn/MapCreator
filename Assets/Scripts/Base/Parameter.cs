using UnityEngine;
using System.Collections;
using System.Linq;

[CreateAssetMenu(menuName = "MyAsset/CellParameter")]
public class CellParameter : ScriptableObject
{
    [System.Serializable]
    public struct KeyValuesPair
    {
        public string _type;
        public Material _material;
    }

    [SerializeField]
    KeyValuesPair[] _cell = null;

    public KeyValuesPair[] Cells
    {
        get
        {
            return _cell;
        }
        set
        {
            _cell = value;
        }
    }


    public Material GetMaterial(string type) 
    {
        
        Material _cell = Cells.FirstOrDefault(r => r._type == type)._material;
        return _cell;
    }
    
}
