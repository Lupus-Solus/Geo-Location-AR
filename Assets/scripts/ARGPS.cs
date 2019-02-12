using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ARGPS : MonoBehaviour
{
    public static ARGPS Instance { get; set; }

    public bool Initialised = false;

    public Text TextDevicePosition;
    public Text TextInitialPosition;
    public Text TextOrientation;
    public Text TextARCameraPosition;

    public Vector3 InitialDevicePosition = Vector3.zero;
    public float Orientation;
    public Camera ARCamera;

    [HideInInspector]
    public float Initial_Lat;
    [HideInInspector]
    public float Initial_Lon;
    [HideInInspector]
    public float Initial_Alt;

    [HideInInspector]
    public float Current_Lat;
    [HideInInspector]
    public float Current_Lon;
    [HideInInspector]
    public float Current_Alt;

    float lowestCompass, highestCompass;


    public float StabliseTime = 4;

    bool initialising = true;
    bool stabiliseStarted = false;
    

    void Start()
    {
        Instance = this;
        Input.location.Start();
        Input.compass.enabled = true;
    }

    void Update()
    {
        if(Input.location.status == LocationServiceStatus.Initializing)
        {
            TextDevicePosition.text = "Waiting for service to start.";
        }

        if (!stabiliseStarted && Input.location.status == LocationServiceStatus.Running)
        {

            TextDevicePosition.text = "Stablising...";
            TextInitialPosition.text = "Stablising...";
            StartCoroutine(InitialiseGPSCoords());
            stabiliseStarted = true;

        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            SetGPSCoords();
            
        }
    }

    private IEnumerator InitialiseGPSCoords()
    {
        yield return new WaitForSeconds(StabliseTime);
        initialising = false;
    }

    void SetGPSCoords()
    {

        if (Input.location.status == LocationServiceStatus.Initializing)
        {
            TextDevicePosition.text = "Location Service is Initialising";
        }

        if (Input.location.status == LocationServiceStatus.Running)
        {
            Current_Lat = Input.location.lastData.latitude;
            Current_Lon = Input.location.lastData.longitude;
            Current_Alt = Input.location.lastData.altitude;

            if (!initialising)
            {
                if (!Initialised)
                {
                    Initial_Lat = Input.location.lastData.latitude;
                    Initial_Lon = Input.location.lastData.longitude;
                    Initial_Alt = Input.location.lastData.altitude;
                                        
                    InitialDevicePosition = new Vector3(Initial_Lat, 0, Initial_Lon);
                   

                    TextInitialPosition.text = "Saved pos lat: " + Initial_Lat + " lon: " + Initial_Lon + " alt: " + Initial_Alt;

                    Orientation = Input.compass.magneticHeading;
                    ARCamera.transform.rotation = Quaternion.Euler(0, Orientation, 0);

                    lowestCompass = Input.compass.magneticHeading;
                    highestCompass = Orientation = Input.compass.magneticHeading;

                    Initialised = true;
                }
            }

            var tempOrientation = Input.compass.magneticHeading;

            if(tempOrientation < lowestCompass)
            {
                lowestCompass = tempOrientation;
            }

            if(tempOrientation > highestCompass)
            {
                highestCompass = tempOrientation;
            }

            TextOrientation.text = "Compass: Low:" + lowestCompass + "High: " + highestCompass + " Current:" + tempOrientation.ToString();
            TextDevicePosition.text = "Current pos lat: " + Current_Lat + " lon: " + Current_Lon + " alt: " + Current_Alt;
            TextARCameraPosition.text = string.Format("Cam Pos: {0} ", ARCamera.transform.position.ToString());
        }

    }




}
