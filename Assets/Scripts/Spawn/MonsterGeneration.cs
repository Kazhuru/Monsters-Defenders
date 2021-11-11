using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGeneration : MonoBehaviour
{
    public enum monsterType { Normal, Strong, Boss }

    public struct monsterSpawnNode
    {
        private monsterType type;
        public int numberOfSpawns;

        public monsterSpawnNode(monsterType type, int numberOfSpawns)
        {
            this.type = type;
            this.numberOfSpawns = numberOfSpawns;
        }

        public monsterType Type { get => type; set => type = value; }
        public int NumberOfSpawns { get => numberOfSpawns; set => numberOfSpawns = value; }
    }

    Dictionary<monsterType, GameObject[]> monsterDictionary;
    monsterSpawnNode[] spawnPattern;

    [SerializeField] float startingSpawnInterval = 1;
    [SerializeField] GameObject[] normalMonsters;
    [SerializeField] GameObject[] strongMonsters;
    [SerializeField] GameObject[] bossMonsters;

    // Line Spawn Variables
    private int counterSpawnLine;
    private int curretSpawnLine;

    // Monsters Spawn Variables
    [SerializeField] private float spawnInterval;
    private int counterSpawnPattern;
    private int counterNode;
    private int currentNodeIndex;
    private int counterNodeIndex;

    private MonsterSpawner monsterSpawner;

    void Start()
    {
        // structures setup:
        monsterDictionary = new Dictionary<monsterType, GameObject[]>
        {
            { monsterType.Normal, normalMonsters },
            { monsterType.Strong, strongMonsters },
            { monsterType.Boss, bossMonsters }
        };
        spawnPattern = new monsterSpawnNode[]
        {
            new monsterSpawnNode(monsterType.Normal, 4),
            new monsterSpawnNode(monsterType.Normal, 4),
            new monsterSpawnNode(monsterType.Normal, 4),
            new monsterSpawnNode(monsterType.Strong, 1),
            new monsterSpawnNode(monsterType.Normal, 3),
            new monsterSpawnNode(monsterType.Strong, 1),
            new monsterSpawnNode(monsterType.Normal, 4),
            new monsterSpawnNode(monsterType.Strong, 2),
            new monsterSpawnNode(monsterType.Normal, 3),
            new monsterSpawnNode(monsterType.Boss, 1),
        };
        // Line Spawn Variables setup:
        counterSpawnLine = 0;
        curretSpawnLine = -1;
        // Monsters Spawn Variables setup:
        counterSpawnPattern = 0;
        counterNode = 0;
        counterNodeIndex = 0;
        currentNodeIndex = -1;
        spawnInterval = startingSpawnInterval;

        monsterSpawner = FindObjectOfType<MonsterSpawner>();

        StartCoroutine(MonsterSpawnGeneration());
    }

    IEnumerator MonsterSpawnGeneration()
    {
        while(true)
        {
            curretSpawnLine = GeneratePseudoRandomNumber(1, 3, 3, curretSpawnLine, ref counterSpawnLine);

            if (counterNode >= spawnPattern[counterSpawnPattern].NumberOfSpawns)
                SpawnResetAndContinueCounterNode();

            monsterSpawnNode currentNode = spawnPattern[counterSpawnPattern];
            int maxNumByMonsterType = monsterDictionary[currentNode.Type].Length - 1;
            currentNodeIndex = GeneratePseudoRandomNumber(0, maxNumByMonsterType, 2, currentNodeIndex, ref counterNodeIndex);
            GameObject monsterToSpawn = monsterDictionary[currentNode.Type][currentNodeIndex];

            counterNode++;
            if (counterSpawnPattern >= spawnPattern.Length - 1)
                counterSpawnPattern = 0;

            monsterSpawner.SpawnMonster(monsterToSpawn, curretSpawnLine);
            yield return new WaitForSeconds(spawnInterval);
            
        }
    }

    private void SpawnResetAndContinueCounterNode()
    {
        counterSpawnPattern++;
        counterNode = 0;
        counterNodeIndex = 0;
        currentNodeIndex = -1;
    }

    private int GeneratePseudoRandomNumber(int minN, int maxN, int pseudoLimit, int currentValue, ref int counterValue)
    {
        int randomLine = Random.Range(minN, maxN+1);
        if (currentValue == randomLine)
            counterValue++;
        else
            counterValue = 0;

        if (counterValue > pseudoLimit-1)
        {
            do
                randomLine = Random.Range(minN, maxN+1);
            while (currentValue == randomLine);
            counterValue = 0;
        }

        return randomLine;
    }

    public float SpawnInterval { get => spawnInterval; set => spawnInterval = value; }
    public float StartingSpawnInterval { get => startingSpawnInterval; set => startingSpawnInterval = value; }
}
