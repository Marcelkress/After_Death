using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    public static bool isSorted = false;

    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints.Add(this);

        isSorted = false;

        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnDestroy()
    {
        spawnPoints.Remove(this);

        isSorted = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isSorted)
        {
            spawnPoints.Sort((x,y) => x.transform.position.x.CompareTo(y.transform.position.x));
            isSorted = true;
        }

    }
}
