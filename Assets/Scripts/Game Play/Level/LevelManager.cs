using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public GameData gameData;
    public LevelData levelData;
    public DataBase dataBase;
    
    public Transform spawnersTrf;
    public Transform pathWaysTrf;
    public Transform pilotTrf;

    public List<Wave> listWaves = new List<Wave>();
    public List<Spawner> listSpawners = new List<Spawner>();
    public List<Pathway> listPathWays = new List<Pathway>();
    private int spiritStone = 0;
    private int lives = 0;

    public int SpiritStone
    {
        get => spiritStone;
        set => spiritStone = value;
    }

    public int Lives
    {
        get => lives;
        set => lives = value;
    }

    protected override void Awake()
    {
        base.Awake();
        InitLevel();
    }

    public void InitLevel()
    {
        SpiritStone = levelData.spiritStoneStart;
        Lives = levelData.liveStart;
        CreateSpawnersAndPathWays();
    }

    public void CreateSpawnersAndPathWays()
    {
        for (int i = 0; i < levelData.layoutData.spawnersData.Count; ++i)
        {
            var spawner = Instantiate(dataBase.prefabData.spawnerPrefab,spawnersTrf);
            spawner.spanwerID = i;
            spawner.InitSpawner(levelData.layoutData.spawnersData[i]);
            listSpawners.Add(spawner);
        }
    }

    public void CreateWave()
    {
        
    }
}
