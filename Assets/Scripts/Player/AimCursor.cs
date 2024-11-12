using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimCursor : MonoBehaviour
{
    public float cursor = 15f;

    private void Start()
    {
        SetCursor(cursor);
    }
    public void SetCursor(float value)
    {
        cursor = value;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(value, value);
    }
}
