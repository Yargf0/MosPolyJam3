using System.Collections.Generic;
using UnityEngine;

public class InfinitySpawn : MonoBehaviour
{
    private List<GameObject> SpawnedObject=new List<GameObject>();
    public void SpawnNextPlatforms(List<GameObject> platforms,List<GameObject> spawningBacgroundObjects, float distanceBetweenBackground,  Vector3 spawnStartPosition, float SpawnDistance, int ObjectToSpawn)
    {
        // Поворот(паркур не должен идти прямо он должен постоянно вилять)
        // есть 2 вида паттернов, один может быть приминён в любой момент другой нет
        // Добавить разброс при спавне, в бок и вверх вниз
        // Добавить паттерны
        // 1) лестница, платформы спавняться каждая по наростающей, относительно предыдущей
        // 2) Противник, по бокам от основного пути спавниться платформа на которой находиться противник
        // 3) Падение, слатформа спавниться сильно ниже предыдущей
        // 4) Пружина для того что бы запрыгнуть на высокую платформу
        // 5) ???
        //от какой позиции спавнить
        for (int i=0; i < ObjectToSpawn; i++)
        {
            GameObject platform = Instantiate(platforms[Random.Range(0, platforms.Count)], new Vector3(spawnStartPosition.x, spawnStartPosition.y, spawnStartPosition.z+SpawnDistance*i), Quaternion.Euler(0, Random.Range(0f, 360f), 0));
            SpawnedObject.Add(platform);
        }


        //if (spawningBacgroundObjects.Count>0)
        //{
        //    for (float i = 0; i < (float)ObjectToSpawn / 4; i++)
        //    {
        //        GameObject platform = Instantiate(spawningBacgroundObjects[Random.Range(0, spawningBacgroundObjects.Count)], new Vector3(spawnStartPosition.x, spawnStartPosition.y - 7f, spawnStartPosition.z + distanceBetweenBackground * i), Quaternion.Euler(0, 90, 0));
        //        SpawnedObject.Add(platform);
        //    }
        //}       
    }
}
