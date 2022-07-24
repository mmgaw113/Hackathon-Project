using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;
using Esri.HPFramework;

public class PlaceOnGround : MonoBehaviour
{
    private double SpawnHeight = 10000;
    public double RayCastDistanceThreshold = 300000;
    private bool OnGround = false;
    public GameObject ArcGISCamera;
    private int Counter = 0;
    public int UpdatesPerRayCast = 200;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check each object every UpdatesPerRayCast updates to see if it was placed on the ground yet
        if (OnGround)
        {
            return;
        }

        Counter++;
        if (Counter >= UpdatesPerRayCast)
        {
            Counter = 0;
            SetOnGround();
        }
    }
    void SetOnGround()
    {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, (float)SpawnHeight))
            {
                // Modify the s altitude based off the raycast hit
                var FeatureLocationComponent = transform.GetComponent<ArcGISLocationComponent>();
                double NewHeight = FeatureLocationComponent.Position.Z - hitInfo.distance;
                double FeatureLongitude = FeatureLocationComponent.Position.X;
                double FeatureLatitude = FeatureLocationComponent.Position.Y;
                ArcGISPoint Position = new ArcGISPoint(FeatureLongitude, FeatureLatitude, NewHeight, FeatureLocationComponent.Position.SpatialReference);
                FeatureLocationComponent.Position = Position;

                OnGround = true;
            }
    }
}
