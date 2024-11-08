using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : PlayerModule
{
    [Header("Movement")]
    [SerializeField] private Transform followTransform;

    [Header("Rotation")]
    [SerializeField] private Vector2 sensetivity = new(100f, 100f);
    [SerializeField] private float minHorizontalAngle = -90f;
    [SerializeField] private float maxHorizontalAngle = 90f;
    [Space(10)]
    [SerializeField] private Transform orientationTransform;

    private Vector3 rotation;

    public Transform DirectionXZTransform => orientationTransform;

    public override void Init(PlayerInput input)
    {
        base.Init(input);

        input.OnRotate += Rotate;
    }

    private void Update()
    {
        Move();
        //Rotate();
    }
    
    private void Move()
    {
        transform.position = followTransform.position;
    }

    private void Rotate(Vector2 inputLook)
    {
        //Vector2 inputLook = input.Look;

        if (inputLook == Vector2.zero)
            return;

        rotation.y += inputLook.x * sensetivity.x * Time.deltaTime;

        rotation.x += inputLook.y * sensetivity.y * Time.deltaTime;
        rotation.x = Mathf.Clamp(rotation.x, minHorizontalAngle, maxHorizontalAngle);

        transform.rotation = Quaternion.Euler(rotation);
        orientationTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
}