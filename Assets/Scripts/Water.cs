using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float yFloating;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Boat"))
        {
            other.GetComponent<Rigidbody>().useGravity = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = new Vector3(other.transform.position.x, yFloating, other.transform.position.z);
        }
    }
}
