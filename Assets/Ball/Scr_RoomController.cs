using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_RoomController : MonoBehaviour
{
    public GameObject RandomPosCloud;
    public GameObject cloud;
    public GameObject wind;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWind",5f, 3f);

        // First 3 clouds
        for (int i = 0; i < 3; i++)
        {
            GameObject Cloud = Instantiate(cloud);
            Cloud.transform.position = new Vector2(Random.Range(-35, 35), Random.Range(-20, 20));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Cloud").Length < 4)
        {
            Instantiate(RandomPosCloud);
        }
    }

    private void SpawnWind()
    {
        Instantiate(wind);
    }
}
