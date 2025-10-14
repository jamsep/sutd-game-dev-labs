using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputTracer : MonoBehaviour
{
    PlayerInput pi;

    void OnEnable()
    {
        pi = GetComponent<PlayerInput>();
        pi.onActionTriggered += Trace;
        Debug.Log($"[Tracer] map={pi.currentActionMap?.name}, behavior={pi.notificationBehavior}");
    }

    void OnDisable() => pi.onActionTriggered -= Trace;

    void Trace(InputAction.CallbackContext ctx)
    {
        string val = "";
        var t = ctx.control?.valueType;
        if (t == typeof(float)) val = ctx.ReadValue<float>().ToString("0.###");
        else if (t == typeof(Vector2)) val = ctx.ReadValue<Vector2>().ToString();

        Debug.Log($"[Tracer] {ctx.action.name} phase={ctx.phase} value={val}");
    }
}
