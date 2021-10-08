using UnityEngine;

namespace RPG.Core
{

    public class PersistentObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObject;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) return;
            SpawnPersistentObjects();
            hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentGameObejct = Instantiate(persistentObject);
            DontDestroyOnLoad(persistentGameObejct);
        }
    }
}
