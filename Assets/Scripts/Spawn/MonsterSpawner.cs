using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] Transform topLineSpawnPoint;
    [SerializeField] Transform midLineSpawnPoint;
    [SerializeField] Transform botLineSpawnPoint;

    private float strengthSpawnMod = 1f;
    public float StrengthSpawnMod { get => strengthSpawnMod; set => strengthSpawnMod = value; }

    public void SpawnMonster(GameObject prefabObjMonster, int line)
    {
        GameUnit prefabUnitMonster = prefabObjMonster.GetComponent<GameUnit>();
        
        GameObject instanceMonster = Instantiate(
            prefabObjMonster,
            GenerateInstancePosition(line, prefabUnitMonster),
            Quaternion.Euler(0f, 180f, 0f));

        instanceMonster.GetComponent<GameUnit>().SetCurrentHealth(
            prefabUnitMonster.GetCurrentHealth() * strengthSpawnMod);
    }

    private Vector2 GenerateInstancePosition(int line, GameUnit prefabUnitMonster)
    {
        Vector2 linePosition = new Vector2(0, 0);

        if (line == 1)
            linePosition = topLineSpawnPoint.position;
        else if (line == 2)
            linePosition = midLineSpawnPoint.position;
        else
            linePosition = botLineSpawnPoint.position;

        Vector2 spawnPosition = new Vector2(
            linePosition.x + prefabUnitMonster.GetXSpawnOffset(),
            linePosition.y + prefabUnitMonster.GetYSpawnOffset());

        return spawnPosition;
    }
}
