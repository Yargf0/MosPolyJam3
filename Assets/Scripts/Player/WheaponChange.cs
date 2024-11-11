using UnityEngine;

public class WheaponChange : MonoBehaviour
{
    [SerializeField] private BaseWeapon[] weapons; // Массив доступного оружия
    private int currentWeaponIndex = 0; // Индекс текущего оружия

    void Start()
    {
        // Активируем первое оружие
        if (weapons.Length > 0)
        {
            ActivateWeapon(currentWeaponIndex);
        }
    }

    void Update()
    {
        // Проверяем нажатие клавиш 1 и 2 для переключения
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchToWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchToWeapon(1);
        }

        // Проверяем прокрутку колесика мыши
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            int direction = scroll > 0 ? 1 : -1;
            SwitchToNextWeapon(direction);
        }
    }

    // Метод для активации выбранного оружия
    private void ActivateWeapon(int weaponIndex)
    {
        // Отключаем все оружия
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }
    }

    // Переключение на следующее оружие по направлению (1 для вперед, -1 для назад)
    private void SwitchToNextWeapon(int direction)
    {
        currentWeaponIndex = (currentWeaponIndex + direction + weapons.Length) % weapons.Length;
        ActivateWeapon(currentWeaponIndex);
    }

    // Переключение на конкретное оружие по индексу
    private void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < weapons.Length && weaponIndex != currentWeaponIndex)
        {
            currentWeaponIndex = weaponIndex;
            ActivateWeapon(currentWeaponIndex);
        }
    }
}


