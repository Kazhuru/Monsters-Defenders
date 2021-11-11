using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour
{
    [SerializeField] int minutesEnemyBuffInterval = 1;
    [SerializeField] float monsterBuffIncrement = 0.05f;

    [SerializeField] int minutesSpawnBuffInterval = 1;
    [SerializeField] float spawnTimeReduction = 0.2f;

    private int counterEnemyBuff = 0;
    private int counterSpawnBuff = 0;
    private int currentMinute = -1;

    private MonsterGeneration monsterGeneration;
    private MonsterSpawner monsterSpawner;

    private void Start()
    {
        monsterGeneration = FindObjectOfType<MonsterGeneration>();
        monsterSpawner = FindObjectOfType<MonsterSpawner>();
    }

    void Update()
    {
        int auxPastMin = currentMinute;
        currentMinute =  (int) GameTimer.instance.ElapsedTime/60;

        if(currentMinute != auxPastMin)
        {
            if(currentMinute != 0)
            {
                if (currentMinute % minutesEnemyBuffInterval == 0)
                    HarderEnemies();

                if (currentMinute % minutesSpawnBuffInterval == 0)
                    FasterSpawners();
            }
        }
    }

    private void HarderEnemies()
    {
        counterEnemyBuff++;

        monsterSpawner.StrengthSpawnMod += monsterBuffIncrement;
    }

    private void FasterSpawners()
    {
        counterSpawnBuff++;
        if (monsterGeneration.SpawnInterval > 0.5f)
            monsterGeneration.SpawnInterval -= spawnTimeReduction;

    }

    public void resetGameDifficulty()
    {
        monsterSpawner.StrengthSpawnMod = 1f;
        monsterGeneration.SpawnInterval = monsterGeneration.StartingSpawnInterval;
    }
}
