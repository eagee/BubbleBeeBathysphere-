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
    Nozzle nozzle;
    public GameObject bubblePrefab;

    void Update()
    {

        tiltAngle += Input.GetAxis("Horizontal") * tiltSpeed * Time.deltaTime;

        // Rotate by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, 0, tiltAngle);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
        transform.position += Vector3.up * Input.GetAxis("Vertical") * vertSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && Time.time > bubbleTime) {
            bubbleTime = Time.time + bubbleCooldown;
            SpawnBubble();
        }
    }

    void SpawnBubble() {
        Debug.Log("Bathysphere has rotation " + transform.rotation);
        if (nozzle) {
            Debug.Log("Nozzle position is " + nozzle.transform.position);
            Instantiate(bubblePrefab, nozzle.transform.position, Quaternion.identity);
        } else {
            Debug.LogWarning("No nozzle?");
        }
    }

    void Start()
    {
        bubbleTime = Time.time;
        nozzle = GetComponentInChildren<Nozzle>(true);    
    }

}
