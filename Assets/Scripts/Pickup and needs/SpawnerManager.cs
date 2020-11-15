using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public int pickupAmount;
    PickupSpawner[] spawners;
    // Start is called before the first frame update
    void Start()
    {
        spawners = GetComponentsInChildren<PickupSpawner>();
    }

    [ContextMenu("Do Something")]
    void SpawnPickups()
    {
        int singleSpawnAmount = pickupAmount / spawners.Length;
        foreach (PickupSpawner ps in spawners)
        {
            ps.CircleSpawn(singleSpawnAmount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
