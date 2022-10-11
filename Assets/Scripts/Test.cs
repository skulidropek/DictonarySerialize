using System.Linq;
using UnityEditor;
using UnityEngine;
public class Test : MonoBehaviour
{
    [SerializeField] private DictonarySerialize<string, string> _pairDict1 = new DictonarySerialize<string, string>();

    private void Start()
    {
        foreach(var keyPair in _pairDict1)
        {
            Debug.Log(keyPair.Key + keyPair.Value);
        }
    }
}
