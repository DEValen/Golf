using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Cloud : MonoBehaviour
{
    private float Speed;
    public int RanInt;
    private int RanY;

    public bool randomPos;

    public Sprite[] Clouds;

    // Start is called before the first frame update
    void Start()
    {
        Speed = Random.Range(.0035f, 0.01f);
        RanInt = Random.Range(0,2);
        RanY = Random.Range(-20, 20);
        if (randomPos == true)
        {
            if (RanInt == 0)
            {
                transform.position = new Vector2(Random.Range(-45, -42), RanY);
            }
            else
            {
                transform.position = new Vector2(Random.Range(42, 45), RanY);
            }
        }


        float scale = Random.Range(0.7f, 1);
        transform.localScale = new Vector2(scale, scale);

        GetComponent<SpriteRenderer>().sprite = Clouds[Random.Range(0, Clouds.Length)];
        

    }

    // Update is called once per frame
    void Update()
    {
        if (RanInt == 0)
        {
            transform.position += Vector3.right * Speed;

            if (transform.position.x > 45)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position -= Vector3.right * Speed;
            if (transform.position.x < -45)
            {
                Destroy(gameObject);
            }
        }
    }

}
