using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class EnemySpawner : MonoBehaviour
    {    
        [Header("Set in Inspector")]
        [SerializeField] private GameObject[] _prefabEnemies;
        
        [Header("Set spawn options:")]
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private float[] x,y,z;

        [Header("Set Dynamically")]
        public List<GameObject> enemies;
        private static Transform _enemyAnchor; // Пустой объект для корректного отображения снарядов иерархии 

        private void Start()
        {
            
            var go = new GameObject("EnemyAnchor");
            _enemyAnchor = go.transform;
        }

        private void Update()
        {
            if (enemies.Count < _maxEnemies)
            {
                SpawnEnemy();
            }
            else
            {
                CheckList();
            }

        }

        private void SpawnEnemy()
        {
            var pos = new Vector3((Random.Range(x[0],x[1])), y[0], (Random.Range(z[0],z[1])));
            
            var ndx = Random.Range(0, _prefabEnemies.Length);
            var go = Instantiate<GameObject>(_prefabEnemies[ndx], _enemyAnchor,true);
            go.transform.position = pos;
            
            enemies.Add(go);
        }

        private void CheckList()
        {
            enemies.RemoveAll( x => !x);
        }
    }
}
