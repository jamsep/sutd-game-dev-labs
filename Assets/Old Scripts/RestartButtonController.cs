using UnityEngine;
using UnityEngine.Events;

public class RestartButtonController : MonoBehaviour, IInteractiveButton
{

    public UnityEvent gameRestart;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ButtonClick()
    {
        gameRestart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
