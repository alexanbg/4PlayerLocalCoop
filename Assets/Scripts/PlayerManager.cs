using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.InputSystem.InputAction;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public List<Player> players;

    private PlayerInputManager playerInputManager;
    private void Start()
    {
        players = new List<Player>();
        playerInputManager = GetComponent<PlayerInputManager>();
    }
    /*
    public void FixJoinedPlayer(PlayerInput playerInput) {
        if (playerInput.devices.Count == 0)
        {
            Debug.LogWarning("No devices found for player input.");
        }
        else if (playerInput.devices[0] is Gamepad)
        {
            playerInput.SwitchCurrentActionMap("SoloGamepad");
        }
    }*/
    public void JoinPlayer(PlayerInput playerInput)
    {
        Debug.Log($"Joining player with device {string.Join("//", playerInput.devices)}");
        if (playerInput.devices.Count == 0)
        {
            Debug.LogWarning("No devices found for player input.");
        }
        else if (playerInput.devices[0].device is Gamepad)
        {
            playerInput.SwitchCurrentActionMap("SoloGamepad");
        }

        Player player = playerInput.gameObject.GetComponent<Player>();
        player.playerManager = this;
        player.transform.parent = transform;
        players.Add(player);
        //UpdateSectors();
        Debug.Log($"Player {playerInput.playerIndex} joined.");
    }

    public void SplitPlayer(CallbackContext ctx, Player origin)
    {
        Debug.Log("Splitting player input.");
        Player copy = Instantiate(playerInputManager.playerPrefab).GetComponent<Player>();
        copy.playerInput.user.UnpairDevices();
        InputUser.PerformPairingWithDevice(origin.playerInput.devices[0], copy.playerInput.user);
        Debug.Log($"New player paired with devices: {string.Join(", ", copy.playerInput.devices)}");
        if (origin.playerInput.devices[0].device is Gamepad)
        {
            origin.playerInput.SwitchCurrentActionMap("SplitGamepadLeft");
            copy.playerInput.SwitchCurrentActionMap("SplitGamepadRight");

        }
        else
        {
            origin.playerInput.SwitchCurrentActionMap("SplitKeyboardLeft");
            copy.playerInput.SwitchCurrentActionMap("SplitKeyboardRight");
        }

        origin.attachedPlayer = copy;
        copy.attachedPlayer = origin;
        //UpdateSectors();
    }

    public void RemovePlayer(CallbackContext ctx, Player origin)
    {
        Debug.Log("Removing player.");
        if (origin.playerInput.devices[0].device is Gamepad)
        {
            origin.attachedPlayer.playerInput.SwitchCurrentActionMap("SoloGamepad");

        }
        else
        {
            origin.attachedPlayer.playerInput.SwitchCurrentActionMap("SoloKeyboard");
        }
        Destroy(origin.gameObject);
        
        //UpdateSectors();
    }
}
