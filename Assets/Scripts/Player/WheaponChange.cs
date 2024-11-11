using UnityEngine;

public class WheaponChange : MonoBehaviour
{
    [SerializeField] private BaseWeapon[] weapons; // ������ ���������� ������
    private int currentWeaponIndex = 0; // ������ �������� ������

    void Start()
    {
        // ���������� ������ ������
        if (weapons.Length > 0)
        {
            ActivateWeapon(currentWeaponIndex);
        }
    }

    void Update()
    {
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
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
    }

    // ������������ �� ��������� ������ �� ����������� (1 ��� ������, -1 ��� �����)
    private void SwitchToNextWeapon(int direction)
    {
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


