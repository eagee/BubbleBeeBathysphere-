using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontWing : MonoBehaviour
{

    public GameObject pivot;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public BathController bathController;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (bathController.fauxlocity != Vector3.zero) {
            transform.RotateAround(pivot.transform.position, Vector3.forward, 200 * Mathf.Sin(Time.time * 100) * Time.deltaTime);
        } else {
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
        }
    }
}
