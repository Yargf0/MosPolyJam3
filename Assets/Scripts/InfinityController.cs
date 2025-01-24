using System.Collections.Generic;
using UnityEngine;

public class InfinityController : MonoBehaviour
{
    public GameObject Sun;
    public float SpawnDistance;
    public int LevelPlatformNumber;
    private Vector3 playerCekpoint;

    [Header("NeonCity")]
    public List<GameObject> NeonCityPlatforms;
    public Material NeonCitySunMaterial;
    public Material NeonCitySkyMaterial;
    public List<GameObject> NeonCityBacgroundObjects;
    public List<GameObject> NeonCitySpawningBacgroundObjects;
    public float NeonCityBacgroundObjectDistance;
    public GameObject NeonCityGlobalVolume;

    [Header("Sunshine")]
    public List<GameObject> SunshinePlatforms;
    public Material SunshineSunMaterial;
    public Material SunshineSkyMaterial;
    public List<GameObject> SunshineBacgroundObjects;
    public List<GameObject> SunshineSpawningBacgroundObjects;
    public float SunshineBacgroundObjectDistance;
    public GameObject SunshineGlobalVolume;

    [Header("Dark")]
    public List<GameObject> DarkPlatforms;
    public Material DarkSunMaterial;
    public Material DarkSkyMaterial;
    public List<GameObject> DarkBacgroundObjects;
    public List<GameObject> DarkSpawningBacgroundObjects;
    public float DarkBacgroundObjectDistance;
    public GameObject DarkGlobalVolume;

    [Header("Scripts")]
    public InfinitySpawn infinitySpawn;
    public ChangeBackground changeBackground;

    public static InfinityController instance;

    public void Start()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        ChangeLevelToSunshine();
        infinitySpawn.SpawnNextPlatforms(SunshinePlatforms, SunshineSpawningBacgroundObjects, NeonCityBacgroundObjectDistance, Vector3.zero, SpawnDistance, LevelPlatformNumber);
    }
    public void ChangeLevelToDark()
    {
        changeBackground.ChangeLevel(Sun, DarkSunMaterial, DarkSkyMaterial, DarkBacgroundObjects, DarkGlobalVolume);
    }
    public void ChangeLevelToSunshine()
    {
        changeBackground.ChangeLevel(Sun, SunshineSunMaterial, SunshineSkyMaterial, SunshineBacgroundObjects, SunshineGlobalVolume);
    }
    public void ChangeLevelToNeonCity()
    {
        changeBackground.ChangeLevel(Sun, NeonCitySunMaterial, NeonCitySkyMaterial, NeonCityBacgroundObjects, NeonCityGlobalVolume);
    }
    public void Update()
    {
        if (playerCekpoint.x >= Player.Instance.gameObject.transform.position.x && playerCekpoint.y >= Player.Instance.gameObject.transform.position.y && playerCekpoint.z >= Player.Instance.gameObject.transform.position.z)
        {
            RandomLevel();
        }
    }
    public void RandomLevel()
    {

    }
}
