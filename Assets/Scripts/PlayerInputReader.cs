using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputReader : MonoBehaviour, IGameInput
{
    public Vector2 JoystickDirection { get; set; }
    public Vector2 TiltDirection { get; set; }

    public Vector2 Move { get; private set; }

    private void Update()
    {
        Vector2 keyboard = ReadKeyboard();

        Vector2 result = keyboard;

        if (JoystickDirection.sqrMagnitude > result.sqrMagnitude)
        {
            result = JoystickDirection;
        }

        if (TiltDirection.sqrMagnitude > result.sqrMagnitude)
        {
            result = TiltDirection;
        }

        Move = result;
    }

    private Vector2 ReadKeyboard()
    {
        Keyboard keyboard = Keyboard.current;

        if (keyboard == null)
        {
            return Vector2.zero;
        }

        float x = 0f;
        float y = 0f;

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            x += 1f;
        }

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            x -= 1f;
        }

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            y += 1f;
        }

        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            y -= 1f;
        }

        return Vector2.ClampMagnitude(new Vector2(x, y), 1f);
    }
}