using UnityEngine;

public class AnimationEventIntTool : MonoBehaviour
{
    public int parameter;

    public void TriggerIntEvent()
    {
        // Call the GameManager singleton directly
        GameManager.instance.IncreaseScore(parameter);
    }
}
