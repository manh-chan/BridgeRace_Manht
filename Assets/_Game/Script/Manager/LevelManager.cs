using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Blue, ColorType.Red, ColorType.Green, ColorType.Violet };

    public Bot botPrefab;
    public Player player;

    private List<Bot> bots = new List<Bot>();
    private Level currentLevel;
    private int levelIndex;

    public int CharacterAmount => currentLevel.botAmount;
    public Vector3 FinishPoint => currentLevel.finishPoint.position;
    public int LevelIndex { get => levelIndex; set => levelIndex = value; }

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 0);
    }
    public void OnInit()
    {
        //init vi tri bat dau game
        Vector3 index = currentLevel.startPoint.position;
        float space = 2f;
        Vector3 leftPoint = ((CharacterAmount / 2) + (CharacterAmount % 2) * 0.5f - 0.5f) * space * Vector3.left + index;

        List<Vector3> startPoints = new List<Vector3>();

        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(leftPoint + space * Vector3.right * i);
        }

        //update navmesh data
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(currentLevel.navMeshData);

        //init random mau
        List<ColorType> colorDatas = Utilities.SortOrder(colorTypes, CharacterAmount);
        //

        //set vi tri player
        int rand = Random.Range(0, CharacterAmount);
        player.TF.position = startPoints[rand];
        player.TF.rotation = Quaternion.identity;
        startPoints.RemoveAt(rand);
        //set color player
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        player.OnInit();
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            Bot bot = SimplePool.Spawn<Bot>(ObjectType.Bot, startPoints[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
            bots.Add(bot);
        }
    }
    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            currentLevel.ClearAllStages();
            Destroy(currentLevel.gameObject);
        }

        if (level < DataManager.Instance.mapData.MapPrefabs.Length)
        {
            currentLevel = Instantiate(DataManager.Instance.mapData.GetLevel(level));
            currentLevel.OnInit();
        }
        if (levelIndex >= DataManager.Instance.mapData.MapPrefabs.Length - 1)
        {
            levelIndex = 0;
        }
    }
    public void OnLoadLevel(int currentLevel)
    {
        levelIndex = currentLevel;
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
    }
    public void OnReset()
    {
        SimplePool.Release(ObjectType.Bot);
        bots.Clear();
    }

    public void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
    }
    public void OnNextLevel()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        levelIndex++;
    }
    public void OnFinishGame()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(null);
            bots[i].MoveStop();
        }
    }
}
