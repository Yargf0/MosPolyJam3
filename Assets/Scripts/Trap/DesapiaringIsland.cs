using DG.Tweening;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class DesapiaringIsland : MonoBehaviour
{
    public float minTime = 1f; // Минимальное время
    public float maxTime = 5f; // Максимальное время

    [SerializeField] private List<Material> materialList;

    private IEnumerator Start()
    {
        Collider collider = GetComponent<Collider>();
        MeshRenderer mr = GetComponent<MeshRenderer>();


        while (true)
        {
            float randomTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(randomTime);
            foreach (Material m in materialList) 
            {
                m.DOFade(0, 1f).SetEase(Ease.InBounce).Play();
            }
            yield return new WaitForSeconds(1f);
            collider.enabled = false;
            mr.enabled = false;

            yield return new WaitForSeconds(5f);
            foreach (Material m in materialList)
            {
                m.DOFade(1, 1f).SetEase(Ease.InBounce).Play();
            }

            collider.enabled = true;
            mr.enabled = true;
        }
    }
}


