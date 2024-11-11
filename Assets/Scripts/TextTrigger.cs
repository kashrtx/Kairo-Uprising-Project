using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class texttrigger : MonoBehaviour
{
    public GameObject TextObject;
    private void Start()
    {
        TextObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            TextObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TextObject.SetActive(false);
            Destroy(TextObject);
        }
    }
}
