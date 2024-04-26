using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulerAngle : MonoBehaviour
{
    public Vector3 eulerAngleInDeg;
    private Vector3 eulerAngleInRad;

    // Start is called before the first frame update
    void Start()
    {
        eulerAngleInRad = eulerAngleInDeg * Mathf.PI / 180f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(eulerAngleInRad);
    }
}
