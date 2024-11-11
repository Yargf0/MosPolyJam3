using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float yPostionTester=-30;
    void Update()
    {
        if (transform.position.y <= yPostionTester)
        {
            Player.Health.Damage(Player.Health.MaxHealth);
        }
    }
}
