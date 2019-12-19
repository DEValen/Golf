using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Wind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float ranFloat = Random.Range(0.8f, 1.2f);
        transform.localScale = new Vector2(ranFloat, ranFloat);

        GameObject[] clouds = GameObject.FindGameObjectsWithTag("Cloud");
        if (clouds[0] != null)
        {
            int ranInt = Random.Range(0, clouds.Length);
            transform.position = clouds[ranInt].transform.position + ((Vector3) Random.insideUnitCircle * 8);

            if (clouds[ranInt].GetComponent<Scr_Cloud>().RanInt != 0)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            }
        }
        else
        {
            transform.position = new Vector2(Random.Range(-35, 35), Random.Range(-20, 20));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
