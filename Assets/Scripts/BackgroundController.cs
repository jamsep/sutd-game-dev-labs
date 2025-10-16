using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance the camera has moved
        float movement = (cam.transform.position.x * (1 - parallaxEffect));
        float distance = (cam.transform.position.x * parallaxEffect);

        // Move the background based on the camera's movement
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // If the camera has moved beyond the length of the background, reset the position
        if (movement > startPos + length) startPos += length;
        else if (movement < startPos - length) startPos -= length;

    }
}
