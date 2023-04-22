using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
     private Vector3 rotateAngle;
    private float randomRotationSpeed;
    private void Awake()
    {
        randomRotationSpeed = Random.Range(60, 180);
        rotateAngle = new Vector3(0,0,randomRotationSpeed);
    }
    private void Update()
    {
        transform.Rotate(rotateAngle*Time.deltaTime);
    }
}
