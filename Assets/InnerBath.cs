using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InnerBath : MonoBehaviour
{

    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    void Update()
    {
        float tiltAroundZ = 0f;
        float tiltAroundX = 0f;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }

}
