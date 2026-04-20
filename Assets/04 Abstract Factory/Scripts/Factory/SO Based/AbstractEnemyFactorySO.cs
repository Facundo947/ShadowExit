using UnityEngine;
using System.Collections.Generic;

namespace AbstractFactory
{
    [CreateAssetMenu(fileName = "Enemy Factory SO", menuName = "ScriptableObjects/Abstract Factory/Enemy Factory")]
    public class AbstractEnemyFactorySO : ScriptableObject
    {
        [SerializeField] EnemyEntrySO[] enemyEntries;
        Dictionary<EnemyType, GameObject> enemyLookup;

        //OnEnable se llama en varios momentos para los ScriptableObjects
        void OnEnable() { VerifyDictionary(); }

        //Cada vez que actualicemos el SO, se borra el diccionario para forzar a que este actualizado
        void OnValidate() { enemyLookup = null; }

        //Crea el diccionario si no existe
        void VerifyDictionary() { if(enemyLookup == null) BuildDictionary(); }

        void BuildDictionary()
        {
            enemyLookup = new Dictionary<EnemyType, GameObject>();
            foreach(EnemyEntrySO entry in enemyEntries)
                enemyLookup.TryAdd(entry.EnemyType, entry.EnemyPrefab);
        }

        //Funcion que finalmente crea el Enemy
        public Enemy CreateEnemy(EnemyType enemyType)
        {
            VerifyDictionary();
            return Instantiate(enemyLookup[enemyType]).GetComponent<Enemy>();
        }
    }
}
