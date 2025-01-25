using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    public Vector3 originalPosition;
    public Vector3 originalScale;
    public float originalTime;
    public float driftScale = 0.1f;
    public float pulseScale = 0.02f;
    public Vector3 fauxlocity;
    public float waterDrag;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;    
        originalScale = transform.localScale;
        originalTime = Time.time;

    }

    void Awake() {

    }

    // Update is called once per frame
    void Update()
    {
        float age = Time.time - originalTime;
        transform.Translate((fauxlocity/(age*waterDrag)) * Time.deltaTime);
        
        transform.Translate(Vector3.up * Time.deltaTime);
        // transform.position = new Vector3(originalPosition.x + Mathf.Sin(age) * driftScale,
        //  transform.position.y, transform.position.z);
        // transform.localScale = new Vector3(originalScale.x + Mathf.Sin(age) * pulseScale,
        //  originalScale.y + Mathf.Cos(age) * pulseScale,
        //  originalScale.z);
    }
}
