using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AbstractFactory
{
    public class SOBasedSpawner : MonoBehaviour
    {
        [SerializeField] AbstractEnemyFactorySO factory;
        List<Enemy> aliveEnemies = new List<Enemy>();

        //Esta vez necesitamos la variable porque igual usamos funciones distintas
        int enemyTypes = 3;

        [Header("Spawn Settings")]
        [SerializeField] int maxEnemies;
        [SerializeField] float spawnTime;
        [SerializeField] float xRange;
        [SerializeField] float yRange;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start() { StartCoroutine(SpawnEnemy(spawnTime)); }

        //Version simplificada del spawner
        //Normalmente no lo haria con corrutina de esta manera
        //Como la lista nunca se vacia, despues del max se sigue repitiendo la corrutina inutilmente
        //En un juego de verdad habrian mas chequeos y logica
        IEnumerator SpawnEnemy(float spawnDelay)
        {
            //Solo spawnea si la cantidad de enemigos en pantalla no supera el limite
            if (aliveEnemies.Count < maxEnemies)
            {
                //Randomiza el numero de enemigos de los posibles
                int randomEnemy = Random.Range(0, enemyTypes);

                //Aprovechamos la interfaz comun
                Enemy nextEnemy;

                //Igualmente tenemos que llamar a funciones distintas
                switch (randomEnemy)
                {
                    case 0:
                        nextEnemy = factory.CreateEnemy(EnemyType.Melee);
                        break;
                    case 1:
                        nextEnemy = factory.CreateEnemy(EnemyType.Ranged);
                        break;
                    default:
                        nextEnemy = factory.CreateEnemy(EnemyType.Flying);
                        break;
                }

                float randomX = Random.Range(-xRange, xRange);
                float randomY = Random.Range(-yRange, yRange);

                nextEnemy.transform.position = new Vector3(randomX, randomY, 0);
                aliveEnemies.Add(nextEnemy);
            }

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(SpawnEnemy(spawnDelay));
        }
    }
}
