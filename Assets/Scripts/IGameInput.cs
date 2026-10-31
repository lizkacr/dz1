using UnityEngine;
public interface IGameInput
{
    Vector2 Move { get; }
    Vector2 JoystickDirection { get; set; }

    Vector2 TiltDirection { get; set; }
}