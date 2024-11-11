using UnityEngine;

public class Fall : MonoBehaviour
{
    public float yPostionTester=-30;
    void Update()
    {
        if (transform.position.y <= yPostionTester)
        {
            Player.Instance.Health.Damage(Player.Instance.Health.MaxHealth);
        }
    }
}
