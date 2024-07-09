using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int waveID;
    public WaveData waveData;
    public List<MiniWave> listMiniWaves;
    public List<MiniWaveData> listMiniWaveData;

    public void InitWave(WaveData data)
    {
        waveData = data;
        listMiniWaveData = waveData.listMiniWaveData;
        CreateMiniWave();
    }

    private void CreateMiniWave()
    {
        for (int i = 0; i < listMiniWaveData.Count; ++i)
        {
            SpawnMiniWave(i);
        }
    }

    private void SpawnMiniWave(int id)
    {
        var miniWave = Instantiate(LevelManager.Instance.dataBase.prefabData.miniWavePrefab, transform);
        miniWave.miniWaveID = id;
        miniWave.wave = this;
        miniWave.Init(listMiniWaveData[id]);
        listMiniWaves.Add(miniWave);
    }

    public void CheckIfAllMiniWaveClear()
    {
        if (listMiniWaves.Count == 0)
        {
            this.PostEvent(EventID.On_Spawn_Next_Wave,waveID);
            LevelManager.Instance.listWaves.Remove(this);
            Destroy(gameObject);
        }
    }
}
