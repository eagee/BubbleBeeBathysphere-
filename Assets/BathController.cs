using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class BathController : MonoBehaviour
{

    public float smooth = 5.0f;
    public float tiltAngle = 0f;
    public float tiltSpeed = 100f;
    public float vertSpeed = 10f;
    public float bubbleTime;
    public float bubbleCooldown = 1f;
    Nozzle nozzle;
    Light2D nozzleLight;
    public GameObject bubblePrefab;
    public float charging;
    public float initialCharge = 0f;

    public Vector3 fauxlocity;

    public new Rigidbody2D rigidbody;
    public float accelerationSmoothTime;
    public float decelerationSmoothTime;

    Engine engine;
    float engineSpeed = 5f;

    private Vector2 _smoothVelocity;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLISION!");
    }

    void Update()
    {
        // reset stuff when first hitting fire button
        if (Input.GetButtonDown("Fire1")) {
            // Debug.Log("Bathysphere: FireDown");
            charging = initialCharge;
            if (nozzleLight) {
                nozzleLight.intensity = 0f;
            }
        }

        // increase charge while fire button held down
        if (Input.GetButton("Fire1")) {
            // Debug.Log("Bathysphere: Fire");
            charging += Time.deltaTime;
            if (nozzleLight) {
                nozzleLight.intensity = charging * 5f;
            }
        }

        if (Input.GetButtonUp("Fire1")) {
            // Debug.Log("Bathysphere: FireUp");
            charging = 0f;
            if (nozzleLight) {
                nozzleLight.intensity = 0f;
            }
            SpawnBubble(0.5f + charging * 2);
        }

    }

    void FixedUpdate()
    {

        tiltAngle += Input.GetAxis("Horizontal") * tiltSpeed * Time.deltaTime;

        // Rotate by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, 0, tiltAngle);

        // Dampen towards the target rotation
        rigidbody.SetRotation(Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth));
        
        // Replacing with tank controls:
        // transform.position += Vector3.up * Input.GetAxis("Vertical") * vertSpeed * Time.deltaTime;

        bool isMoving = Input.GetButton("Jump");
        
        
        rigidbody.velocity = Vector2.SmoothDamp(rigidbody.velocity, fauxlocity * engineSpeed, ref _smoothVelocity, isMoving? accelerationSmoothTime : decelerationSmoothTime);
        
        if (isMoving) {
            if (engine) {
                Vector3 engineDirection = engine.transform.position - transform.position;
                Debug.Log("Engine direction is " + engineDirection);
                fauxlocity -= engineDirection.normalized * Time.deltaTime;
                Debug.Log("fauxlocity is " + fauxlocity);
            } else {
                Debug.LogWarning("No engine?");
            }
        } else {
            fauxlocity = Vector3.zero;
        }

    }

    void SpawnBubble(float force) {
        Debug.Log("Bathysphere has rotation " + transform.rotation);
        if (nozzle) {
            Debug.Log("Nozzle position is " + nozzle.transform.position);
            Vector3 nozzleDirection = nozzle.transform.position - transform.position;
            Debug.Log("Nozzle direction is " + nozzleDirection);
            Debug.Log("Normalized nozzle direction is " + nozzleDirection.normalized);
            Debug.Log("Nozzle force is " + force);
            GameObject bubble = Instantiate(
                bubblePrefab, 
                nozzle.transform.position, 
                Quaternion.identity);
            if (bubble) {
                Bubble b = bubble.GetComponent<Bubble>();
                b.fauxlocity = nozzleDirection.normalized * force;
            } else {
                Debug.LogWarning("Failed to instantiate bubble!");
            }
        } else {
            Debug.LogWarning("No nozzle?");
        }
    }

    void Start()
    {
        charging = 0f;
        bubbleTime = Time.time;

        nozzle = GetComponentInChildren<Nozzle>(true);   
        nozzleLight = GetComponentInChildren<Light2D>(true); 
        if (nozzleLight == null) {
            Debug.LogWarning("No nozzle light found?");
        }

        engine = GetComponentInChildren<Engine>(true);   

        fauxlocity = new Vector3(0,0,0);
    }

}
