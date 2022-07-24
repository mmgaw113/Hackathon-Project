using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Esri.ArcGISMapsSDK.Components;
using Esri.GameEngine.Geometry;

[System.Serializable]
public class FeatureCollectionData
{
    public string type;
    public Feature[] features;
}

[System.Serializable]
public class Feature
{
    public string type;
    public Geometry geometry;
}

[System.Serializable]
public class Geometry
{
    public string type;
    public double[] coordinates;
}
public class FeatureLayerQuery : MonoBehaviour
{
    public string url = "https://services7.arcgis.com/33Tmvrm3G2UZLFK9/ArcGIS/rest/services/Demo_Points/FeatureServer/5/query?f=geojson&where=1=1";
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetFeatures());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator GetFeatures()
    {
        UnityWebRequest Request = UnityWebRequest.Get(url);
        yield return Request.SendWebRequest();
        if (Request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(Request.error);
        }
        else
        {
            CreateGameObjects(Request.downloadHandler.text);
        }
    }
    private void CreateGameObjects(string Response)
    {
        // Deserialize the JSON response from the query.
        var deserialized = JsonUtility.FromJson<FeatureCollectionData>(Response);

        foreach (Feature feature in deserialized.features)
        {
            double Longitude = feature.geometry.coordinates[0];
            double Latitude = feature.geometry.coordinates[1];
            Debug.Log(Longitude + "," + Latitude);
        }
    }
}
