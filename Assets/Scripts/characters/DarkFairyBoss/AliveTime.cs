using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveTime : MonoBehaviour
{
    public float destroyAfter;

    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}
