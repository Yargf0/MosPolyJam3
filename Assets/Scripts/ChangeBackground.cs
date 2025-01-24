using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class ChangeBackground : MonoBehaviour
{
    private List<GameObject> turnedOnBackgroundObject=new List<GameObject>();
    private GameObject turnedOnGlobalVolume;

    public void ChangeLevel(GameObject sun, Material toChangeSunMaterial, Material toChangeSkyMaterial, List<GameObject> toChangeBacgroundObjects, GameObject toChangeGlobalVolume)
    {

        //�������� ���� ������
        if (sun!=null)
            sun.GetComponent<SpriteRenderer>().material = toChangeSunMaterial;
        //�������� ��������
        RenderSettings.skybox=toChangeSkyMaterial;
        //��������� ������� �������
        foreach (GameObject obj in turnedOnBackgroundObject)
        { 
           obj.SetActive(false);
        }
        turnedOnBackgroundObject.Clear();
        //�������� ������� �������
        foreach (GameObject obj in toChangeBacgroundObjects)
        {
            obj.SetActive(true);
            turnedOnBackgroundObject.Add(obj);
        }
        //�������� global volume
        if (turnedOnGlobalVolume != null)
        { 
            Destroy(turnedOnGlobalVolume);
        }
        turnedOnGlobalVolume = Instantiate(toChangeGlobalVolume);
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        cameraData.renderPostProcessing = true;
    }
}
