using UnityEngine;
using UnityEngine.InputSystem;

public class UIControlSchemeSwitcher : MonoBehaviour
{
    public GameObject touchUI;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        UpdateUI(playerInput.currentControlScheme);
    }

    void OnEnable()
    {
        playerInput.onControlsChanged += OnControlsChanged;
    }

    void OnDisable()
    {
        playerInput.onControlsChanged -= OnControlsChanged;
    }

    void OnControlsChanged(PlayerInput input)
    {
        UpdateUI(input.currentControlScheme);
    }

    void UpdateUI(string scheme)
    {
        if (scheme == "Touch")
        {
            touchUI.SetActive(true);
        }
        else
        {
            touchUI.SetActive(false);
        }
    }
}
