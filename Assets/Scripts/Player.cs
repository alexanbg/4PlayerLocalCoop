using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

public class Player : MonoBehaviour
{
    private string name;

    [HideInInspector]
    public PlayerManager playerManager;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public Player attachedPlayer;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
    }

}
