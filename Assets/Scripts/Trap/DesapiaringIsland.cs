using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesapiaringIsland : MonoBehaviour
{
    public float minTime = 1f; // Минимальное время
    public float maxTime = 5f; // Максимальное время

    private IEnumerator Start()
    {
        while (true)
        {
            float randomTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(randomTime);

            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.GetComponent<MeshRenderer>().enabled = false;

            yield return new WaitForSeconds(5f);

            gameObject.GetComponent<Collider>().enabled = true;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}


