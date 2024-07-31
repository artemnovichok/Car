using UnityEngine;

public class DeviceType : MonoBehaviour
{
    public static DeviceType Instance { get; private set; }

    private Devices currentDeviceType;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        currentDeviceType = DetermineDeviceType();
    }

    private Devices DetermineDeviceType()
    {
        // Реализация определения типа устройства

        return Devices.Computer;
    }

    public Devices GetCurrentDevice()
    {
        return currentDeviceType;
    }
}
