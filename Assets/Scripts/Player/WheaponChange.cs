using UnityEngine;

public class WheaponChange : MonoBehaviour
{
    [SerializeField] private BaseWeapon[] weapons; // ������ ���������� ������
    private int currentWeaponIndex = 0; // ������ �������� ������

    private bool isEnabled;

    void Start()
    {
        isEnabled = true;
        // ���������� ������ ������
        if (weapons.Length > 0)
        {
            ActivateWeapon(currentWeaponIndex);
        }
    }

    public void Enable()
    {
        isEnabled = true;
    }

    public void Disable()
    {
        isEnabled = false;
    }

    void Update()
    {
        if (!isEnabled)
            return;

        // ��������� ������� ������ 1 � 2 ��� ������������
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToWeapon(1);
        }

        // ��������� ��������� �������� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            int direction = scroll > 0 ? 1 : -1;
            SwitchToNextWeapon(direction);
        }
    }

    // ����� ��� ��������� ���������� ������
    private void ActivateWeapon(int weaponIndex)
    {
        // ��������� ��� ������
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == currentWeaponIndex)
                weapons[i].Enable();
            else
                weapons[i].Disable();
        }
    }

    // ������������ �� ��������� ������ �� ����������� (1 ��� ������, -1 ��� �����)
    private void SwitchToNextWeapon(int direction)
    {
        Debug.LogWarning(isEnabled);
        currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
        ActivateWeapon(currentWeaponIndex);
    }

    // ������������ �� ���������� ������ �� �������
    private void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Length && weaponIndex != currentWeaponIndex)
        {
            currentWeaponIndex = weaponIndex;
            ActivateWeapon(currentWeaponIndex);
        }
    }
}


