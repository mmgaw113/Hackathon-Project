using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeController : MonoBehaviour
{
    [Header ("Variables")]
    public float speed;
    public float rpm;
    public float power;
    private float incline;
    public float riderWeight;
    private float bikeMass = 26.0f;
    const float gravity = 9.81f;
    public Vector3 direction;
    public int i = 0;
    [Header("Components")]
    private Rigidbody rb;
    public FeatureLayerQuery query;
    public Transform handleBars;
    public GameObject frontWheel;
    public GameObject backWheel;
    public GameObject driveTrain;
    [Header("StopWatch")]
    const float startTime = 0;
    public float currentTime;
    public float finalTime;
    public bool done;
    public bool countingUp;
    // Start is called before the first frame update
    void Start()
    {
        query = GameObject.Find("FeatureLayerRequest").GetComponent<FeatureLayerQuery>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Calculate Power and Speed
        CalculatePowerAndSpeed();
        //StopWatch
        if (countingUp)
        {
            StopWatch();
        }
        else
        {
            done = true;
            currentTime = startTime;
        }
        if (done)
        {
            power = 0;
        }
    }
    private void FixedUpdate()
    {
        //Movement
        UpdatePosition();
    }
    public void UpdatePosition()
    {
        if (speed > 0)
        {
            //rb.MovePosition(transform.position + (speed * Time.deltaTime * handleBars.up));
            float distance = Vector3.Distance(transform.position, query.waypoints[i].transform.position);
            if(i <= query.waypoints.Length - 1)
            {
                if (distance < 10)
                {
                    i++;
                }
                if(i == query.waypoints.Length)
                {
                    i = 0;
                }
            }
            else if(i == query.waypoints.Length)
            {
                i = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, query.waypoints[i].transform.position, speed * Time.deltaTime);
            Vector3 relativePos = query.waypoints[i].transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos);
        }

    }
    public void StopWatch()
    {
        Mathf.Round(currentTime += Time.deltaTime);
        finalTime = 5 - currentTime;
    }
    public void CalculatePowerAndSpeed()
    {
        done = false;
        power = finalTime * 50;
        if (speed > 0)
        {
            frontWheel.transform.Rotate(direction * rpm);
            backWheel.transform.Rotate(direction * rpm);
            driveTrain.transform.Rotate(-direction * (rpm / 50));
        }
    }
}
