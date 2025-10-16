using UnityEngine;

[CreateAssetMenu(fileName = "MarioLives", menuName = "Scriptable Objects/MarioLives")]
public class MarioLives : ScriptableObject
{
    public int maxLives = 10;
    public int remainingLives;

    public void ResetLives()
    {
        remainingLives = maxLives;
    }

    public void DecreaseLife()
    {
        remainingLives = Mathf.Max(0, remainingLives - 1);
    }
}