using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private int level;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerHat"))
        {
            other.transform.parent.parent.GetComponent<CalculateHatCount>().ObstaclesHit(level);
        }
    }
}
