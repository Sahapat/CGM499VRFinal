using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float yFloating;
    [SerializeField] private Transform[] respawnPoints;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Boat"))
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = new Vector3(other.transform.position.x, yFloating, other.transform.position.z);
        }
        else if(other.CompareTag("Player"))
        {
            if(other.transform.position.z < transform.position.z)
            {
                other.transform.position = respawnPoints[0].position;
            }
            else
            {
                other.transform.position = respawnPoints[1].position;
            }
        }
    }
}
