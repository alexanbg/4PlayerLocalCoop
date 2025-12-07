using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using static UnityEngine.InputSystem.InputAction;
using static UnityEngine.UI.Image;

public class PlayerSplitting : MonoBehaviour
{
    private PlayerInput playerInput;
    private Player player;

    

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<Player>();
    }


    public void Split(CallbackContext ctx)
    {
        /*
        if (ctx.phase == InputActionPhase.Performed)
        {
            PlayerInput copy = Instantiate(gameObject).GetComponent<PlayerInput>();

            copy.SwitchCurrentControlScheme(playerInput.currentControlScheme);
            
            copy.user.UnpairDevices();
            InputUser.PerformPairingWithDevice(playerInput.devices[0], copy.user);
            

            //Debug.Log($"Player paired with {copy.devices[0]}");

            if (playerInput.devices[0].device is Gamepad)
            {
                playerInput.SwitchCurrentActionMap("SplitGamepadLeft");
                copy.SwitchCurrentActionMap("SplitGamepadRight");
            }
            else
            {
                playerInput.SwitchCurrentActionMap("SplitKeyboardLeft");
                copy.SwitchCurrentActionMap("SplitKeyboardRight");
            }
        }*/
        if (ctx.performed)
        {
            if(player.attachedPlayer == null)
                player.playerManager.SplitPlayer(ctx, player);
        }

        
    }

    public void Remove(CallbackContext ctx)
    {
        
        if (ctx.performed)
        {
            player.playerManager.RemovePlayer(ctx, player);
        }


    }
}
