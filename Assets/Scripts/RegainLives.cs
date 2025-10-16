using UnityEngine;

public class RegainLives : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameConstants gameConstants;

    void Start()
    {
        gameConstants.currentLives = gameConstants.maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
