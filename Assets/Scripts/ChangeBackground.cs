using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class ChangeBackground : MonoBehaviour
{
    private List<GameObject> turnedOnBackgroundObject=new List<GameObject>();
    private GameObject turnedOnGlobalVolume;

    public void ChangeLevel(GameObject sun, Material toChangeSunMaterial, Material toChangeSkyMaterial, List<GameObject> toChangeBacgroundObjects, GameObject toChangeGlobalVolume)
    {

        //заменяем цвет солнца
        if (sun!=null)
            sun.GetComponent<SpriteRenderer>().material = toChangeSunMaterial;
        //заменяем скайбокс
        RenderSettings.skybox=toChangeSkyMaterial;
        //выключаем фоновые обьекты
        foreach (GameObject obj in turnedOnBackgroundObject)
        { 
           obj.SetActive(false);
        }
        turnedOnBackgroundObject.Clear();
        //включаем фоновые обьекты
        foreach (GameObject obj in toChangeBacgroundObjects)
        {
            obj.SetActive(true);
            turnedOnBackgroundObject.Add(obj);
        }
        //заменяем global volume
        if (turnedOnGlobalVolume != null)
        { 
            Destroy(turnedOnGlobalVolume);
        }
        turnedOnGlobalVolume = Instantiate(toChangeGlobalVolume);
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.renderPostProcessing = true;
    }
}
