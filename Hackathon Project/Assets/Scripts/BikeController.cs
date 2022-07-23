using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header ("Variables")]
    public float speed;
    public Vector3 direction;
    [Header ("Components")]
    public GameObject frontWheel;
    public GameObject backWheel;
    public GameObject driveTrain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(speed > 0)
        {
            frontWheel.transform.Rotate(direction * speed);
            backWheel.transform.Rotate(direction * speed);
            driveTrain.transform.Rotate(-direction * (speed/50));
        }
    }
}
