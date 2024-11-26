using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public ItemTemPlate[] mapTemplate;
    public GameObject[] mapSO;
    public Button[] mapBtns;

    private void Start()
    {
        for (int i = 0; i < DataManager.Instance.mapData.MapPrefabs.Length; i++)
        {
            mapSO[i].SetActive(true);
        }
        LoadPanel();
    }
    private void LoadPanel()
    {
        var listMapData = DataManager.Instance.mapData.MapPrefabs;
        for (int i = 0; i < DataManager.Instance.mapData.MapPrefabs.Length; i++)
        {
            mapTemplate[i].titleTxt.text = i.ToString();
        }
    }
    public void LevelBtns(int btnNo)
    {
        LevelManager.Instance.OnLoadLevel(btnNo);
    }

}
