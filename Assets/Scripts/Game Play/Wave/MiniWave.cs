using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MiniWave : MonoBehaviour
{
    public int miniWaveID;
    public Wave wave;
    public MiniWaveData miniWaveData;

    public List<MonsterData> listMonsterData;
    public List<Monster> listMonsters;
    public float spawnCooldown;

    private Transform waveTrf;
    private Transform spawnerTrf;
    public Pathway pathway;

    public void Init(MiniWaveData data)
    {
        miniWaveData = data;
        spawnCooldown = data.spawnCooldown;

        var listMonstersID = data.listMonstersID;
        for (int i = 0; i < listMonstersID.Count; ++i)
        {
            listMonsterData.Add(LevelManager.Instance.dataBase.listMonstersData[listMonstersID[i]]);
        }

        spawnerTrf = LevelManager.Instance.listSpawners[data.spawnerID].transform;
        waveTrf = LevelManager.Instance.listSpawners[data.spawnerID].transform;
        pathway = LevelManager.Instance.listPathWays[data.pathwayID];
        transform.SetParent(waveTrf);

        StartCoroutine(SpawnMiniWave());
    }

    private IEnumerator SpawnMiniWave()
    {
        for (int i = 0; i < listMonsterData.Count; ++i)
        {
            SpawnEnermy(i);
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    private void SpawnEnermy(int IDInWave)
    {
        var enermy = PoolingManager.Spawn(LevelManager.Instance.dataBase.listMonstersData[miniWaveData.listMonstersID[IDInWave]].monsterPrefab);
        enermy.name = listMonsterData[IDInWave].monsterName + " " + (IDInWave + 1);
        enermy.miniWave = this;
        enermy.IDInWave= IDInWave;
        enermy.transform.position = spawnerTrf.position;
        enermy.InitMonster(listMonsterData[IDInWave]);
        listMonsters.Add(enermy);
        enermy.transform.SetParent(transform);
    }

    public void CheckIfAllEnermyDead()
    {
        if (listMonsters.Count == 0)
        {
            wave.listMiniWaves.Remove(this);
            wave.CheckIfAllMiniWaveClear();
            Destroy(gameObject);
        }
    }

}
