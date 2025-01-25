using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BathController : MonoBehaviour
{

    public float smooth = 5.0f;
    public float tiltAngle = 0f;
    public float tiltSpeed = 100f;
    public float vertSpeed = 10f;
    public float bubbleTime;
    public float bubbleCooldown = 1f;
    public float bubbleOffset = 10f;

    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        // float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        // float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        tiltAngle += Input.GetAxis("Horizontal") * tiltSpeed * Time.deltaTime;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, 0, tiltAngle);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        transform.position += Vector3.up * Input.GetAxis("Vertical") * vertSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && Time.time > bubbleTime) {
            bubbleTime += bubbleCooldown;
            SpawnBubble(transform.forward * bubbleOffset + transform.right * 10f);
        }
    }

    void SpawnBubble(Vector3 origin) {
        Debug.Log("Bathysphere has rotation " + transform.rotation);
        Debug.Log("Spawning bubble at " + origin);
    }

    void Start()
    {
        bubbleTime = Time.time;    
    }

}
