using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickPowerupController : MonoBehaviour, IPowerupController
{
    public BasePowerup powerup; // reference to this question box's powerup

    public Animator coinAnimator;
    public Animator selfAnimator;
    public AudioSource Audio;

    bool hit;
    //Vector2 movement = new Vector2(moveHorizontal, 0);

    public bool breakable = false;
    SpringJoint2D spring;

    void Start()
    {
        hit = false;
        spring = GetComponent<SpringJoint2D>();
        spring.frequency = 3f;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!hit)
        {
            // update animator state
            if (!breakable)
            {
                selfAnimator.SetTrigger("used");
            } else
            {
                selfAnimator.SetTrigger("blank");
            }

                coinAnimator.SetTrigger("coinAnimation");
            print("HIT");
            hit = true;



        }
    }

    // used by animator
    public void frozen()
    {
        spring.frequency = 0f;
    }

    public void RestartButtonCallback(int input)
    {
        hit = false;
        spring.frequency = 3f;
        coinAnimator.SetTrigger("reset");
        selfAnimator.SetTrigger("reset");
        print("Restttinggggg");
    }

    public void PlayJumpSound()
    {
        // play jump sound
        Audio.PlayOneShot(Audio.clip);
    }

    public void Disable()
    {
        this.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        transform.localPosition = new Vector3(0, 0, 0);
    }
}