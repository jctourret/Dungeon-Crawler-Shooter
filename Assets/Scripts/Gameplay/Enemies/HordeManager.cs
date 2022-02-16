using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HordeManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawners;

    [Serializable]
    public class Horde {
        public enum Order {
            Melee, Distance
        }
        public Order[] order;
        [Space]
        public int[] cantOfEnemiesToCreateOfTypeInOrder;
        [Space]
        public float[] timeBetweenEveryEnemy;
    }

    [SerializeField] List<Horde> hordes;
    [SerializeField] List<Enemy> enemiesTypes;
    [SerializeField] float timeBetweenHordes;
    float timerChangeHorde = 0f;

    [SerializeField] int actualHorde;
    [SerializeField] int enemiesRemaining;

    int totalEnemiesInHorde;
    int actualEnemiesCreatedInHorde = 0;
    
    [SerializeField] Transform enemiesCreatedParent;

    List<float> enemiesSpawnTimers;
    List<int> enemiesOfOrderCreated;

    public enum HordeManagerState {
        Waiting, ChangingHorde, CreatingEnemies
    }
    [SerializeField] HordeManagerState hordeState;

    [SerializeField] List<Enemy> enemiesCreated;

    void Start()
    {
        enemiesSpawnTimers = new List<float>();
        enemiesOfOrderCreated = new List<int>();
        ChangeHorde();

        Enemy.EnemyDead += RemoveEnemy;

    }

    void OnDisable() {
        Enemy.EnemyDead -= RemoveEnemy;
    }

    void Update()
    {
        if (hordeState == HordeManagerState.Waiting)
            return;

        if(hordeState == HordeManagerState.ChangingHorde) {
            timerChangeHorde += Time.deltaTime;
            if(timerChangeHorde >= timeBetweenHordes) {
                timerChangeHorde = 0f;
                actualHorde++;
                ChangeHorde();
            }
            return;
        }

        for(int i = 0; i < hordes[actualHorde].order.Length; i++) {
            enemiesSpawnTimers[i] += Time.deltaTime;
            if(enemiesSpawnTimers[i] >= hordes[actualHorde].timeBetweenEveryEnemy[i] && enemiesOfOrderCreated[i] < hordes[actualHorde].cantOfEnemiesToCreateOfTypeInOrder[i]) {
                SpawnEnemy(i);
                enemiesSpawnTimers[i] = 0f;
                enemiesOfOrderCreated[i]++;
            }
        }
    }

    void SpawnEnemy(int order) {
        int spawn = UnityEngine.Random.Range(0, spawners.Count);
        Enemy e = Instantiate(enemiesTypes[(int)hordes[actualHorde].order[order]], spawners[spawn].transform.position, Quaternion.identity, enemiesCreatedParent);
        enemiesCreated.Add(e);

        actualEnemiesCreatedInHorde++;
        if (actualEnemiesCreatedInHorde == totalEnemiesInHorde)
            hordeState = HordeManagerState.Waiting;
    }

    void RemoveEnemy(Enemy e) {
        enemiesCreated.Remove(e);
        enemiesRemaining--;

        if (enemiesRemaining <= 0 && enemiesCreated.Count <= 0) 
            hordeState = HordeManagerState.ChangingHorde;
    }

    void ChangeHorde() {
        if (actualHorde >= hordes.Count)
            hordeState = HordeManagerState.Waiting;
        else {

            enemiesSpawnTimers.Clear();
            enemiesOfOrderCreated.Clear();
            
            for (int i = 0; i < hordes[actualHorde].order.Length; i++) {
                enemiesSpawnTimers.Add(0f);
                enemiesOfOrderCreated.Add(0);
            }

            enemiesRemaining = 0;
            actualEnemiesCreatedInHorde = 0;

            for (int i = 0; i < hordes[actualHorde].cantOfEnemiesToCreateOfTypeInOrder.Length; i++)
                enemiesRemaining += hordes[actualHorde].cantOfEnemiesToCreateOfTypeInOrder[i];
            totalEnemiesInHorde = enemiesRemaining;

            hordeState = HordeManagerState.CreatingEnemies;
        }
    }

}
