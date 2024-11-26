using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "MapData", menuName = "ScriptableObjects/MapData", order = 1)]
public class MapData : ScriptableObject
{
    [SerializeField] public Level[] MapPrefabs;

    public Level GetLevel(int indexlv)
    {
        return MapPrefabs[indexlv];
    }
}
