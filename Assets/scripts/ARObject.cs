using OpenTK;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ARObject : MonoBehaviour
{
    public Vector3 ObjectGPSCoords;

    public Text TextARPosition;

    private float degreesLatitudeInMeters = 111132;
    private float degreesLongitudeInMetersAtEquator = 111319.9f;

    private bool transformSet = false;

    private ARGPS arGPS;

    private Vector3d arObjectPosition;

    void Start()
    {
        arGPS = ARGPS.Instance;

        arObjectPosition = new Vector3d(ObjectGPSCoords);
    }

    // Update is called once per frame
    void Update()
    {
        if(arGPS == null)
        {
            arGPS = ARGPS.Instance;
        }

        if(arGPS == null)
        {
            return;
        }

        //TextARPosition.text = "arGPS initialised: " + arGPS.Initialised;
        if (arGPS.Initialised)
        {
            
            PlaceObjectInARWorld();
        }
    }

    private double GetLongitudeDegreeDistance(double latitude)
    {
        return degreesLongitudeInMetersAtEquator * Math.Cos(latitude * (Math.PI / 180));
    }

    void PlaceObjectInARWorld()
    {
        if (!transformSet)
        {
            arGPS.ARCamera.transform.rotation = Quaternion.Euler(0, arGPS.Orientation, 0);

            var deviceGpsPosition = new Vector3d(arGPS.InitialDevicePosition);
            
            var offset = (arObjectPosition - deviceGpsPosition) * new Vector3d(degreesLatitudeInMeters, 1, GetLongitudeDegreeDistance(deviceGpsPosition.X));
            var heading = MathHelper.DegreesToRadians(arGPS.Orientation);

            var t = Quaterniond.FromEulerAngles(0, -heading, 0);

            var rotatedOffset = t * offset;

            var floatPosition = new Vector3((float)rotatedOffset.X, (float)rotatedOffset.Y, (float)rotatedOffset.Z);
            transform.position = new Vector3(floatPosition.x, 0, floatPosition.z);
            transform.localScale = new Vector3(1, 1, 1);

            if(TextARPosition != null)
            {
                TextARPosition.text = "AR Object placed at: x: " + transform.position.x + ", y: 0, z: " + transform.position.z;
            }

            transformSet = true;
        }

    }
}
