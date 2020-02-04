using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public List<GameObject> Players;
    public List<InputDevice> Devices;
    public Transform SpawnPoint;
    
    private void Awake() {
        if(Instance != null)
        {
            Instance = this;
        }    
    }

    void Start()
    {
        Devices = new List<InputDevice>();
    }

    void Update()
    {
        if (!IsAllDevicesRegistered())
        {
            foreach (var device in InputManager.Devices) {
                if (!IsDeviceAlreadyRegistered(device))
                {
                    if (device.Action1.WasPressed)
                    {
                        CreatePlayer(device);
                    }
                }
            }
        }
    }

    private bool IsAllDevicesRegistered()
    {
        return Devices.Count >= InputManager.Devices.Count;
    }

    private bool IsDeviceAlreadyRegistered(InputDevice device)
    {
        var guid = device.GUID;
        int index = Devices.FindIndex(d => d.GUID == guid);
        return index >= 0;
    }

    private void CreatePlayer(InputDevice device)
    {
        var newPlayer = GetFreePlayer();
        
        var playerControllerHandler = newPlayer.GetComponent<ControllerHandler>();
        if (playerControllerHandler)
        {
            playerControllerHandler.SetDevice(device);
        }

        Devices.Add(device);
        playerControllerHandler.Initialize();
    }

    private GameObject GetFreePlayer()
    {
        GameObject freePlayer = null;

        foreach(GameObject player in Players)
        {
            var playerControllerHandler = player.GetComponent<ControllerHandler>();
            if (playerControllerHandler && playerControllerHandler.GetDevice() == null)
            {
                freePlayer = player;
                break;
            }
        }

        return freePlayer;
    }
}
