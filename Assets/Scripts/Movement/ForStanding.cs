using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForStanding : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(playerTag))
        {
            other.gameObject.transform.parent = null;
        }
    }
}
