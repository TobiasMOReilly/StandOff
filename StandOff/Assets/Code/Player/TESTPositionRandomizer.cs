using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTPositionRandomizer : MonoBehaviour
{
    private float MinPos;
    private float MaxPos;


    // Start is called before the first frame update
    void Start()
    {
        MinPos = -4f;
        MaxPos = 4f;

        RandomizePos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RandomizePos()
    {
        Vector3 RandPos = new Vector3(RandomFloat(MinPos, MaxPos),0f , RandomFloat(MinPos, MaxPos));

        transform.position = RandPos;
    }

    private float RandomFloat(float min, float max)
    {
        return Random.Range(min,max);
    }
}
