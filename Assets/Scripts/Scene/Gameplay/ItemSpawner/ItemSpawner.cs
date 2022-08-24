using PaintAstic.Scene.Gameplay.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.ItemSpawner
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private ItemCollectPoint _collectPointPrefab;
        [SerializeField] private ItemBomb _bombPrefab;

        [SerializeField] private int _spawnRadiusX = 8;
        [SerializeField] private int _spawnRadiusZ = 8;
        [SerializeField] private float _spawnDelay = 3f;

        private bool _isRunning;
        private float _spawnDelayTimer;

        private List<ItemCollectPoint> _collectPointPool = new List<ItemCollectPoint>();
        private List<ItemBomb> _bombPool = new List<ItemBomb>();

        private void Update()
        {
            _spawnDelayTimer += Time.deltaTime;
            if (_spawnDelayTimer > _spawnDelay)
            {
                SpawnRandomItem();
                Debug.Log("Spawn!");
                _spawnDelayTimer = 0;
            }
        }

        private void SpawnRandomItem()
        {
            int random = Random.Range(0, 2);
            switch (random)
            {
                case 0: SpawnCollectPoint(); break;
                case 1: SpawnBombItem(); break;
            }
        }

        private void SpawnCollectPoint()
        {
            ItemCollectPoint collectPoint = _collectPointPool.Find(i => !i.gameObject.activeSelf);
            if (collectPoint == null)
            {
                collectPoint = InstantiateItem(_collectPointPrefab, _collectPointPool);
            }

            ConfigSpawnedItem(collectPoint);
        }

        private void SpawnBombItem()
        {
            ItemBomb bomb = _bombPool.Find(i => !i.gameObject.activeSelf);
            if (bomb == null)
            {
                bomb = InstantiateItem(_bombPrefab, _bombPool);
            }

            ConfigSpawnedItem(bomb);
        }

        private T InstantiateItem<T>(T prefab, List<T> pool) where T : BaseItem
        {
            T baseItem = Instantiate(prefab, transform);
            pool.Add(baseItem);

            return baseItem;
        }

        private void ConfigSpawnedItem(BaseItem baseItem)
        {
            baseItem.transform.position = new Vector3(Random.Range(0, _spawnRadiusX), 0, Random.Range(0, _spawnRadiusZ));
            baseItem.gameObject.SetActive(true);
        }
    }
}

