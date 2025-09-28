using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MysteryCoin : MonoBehaviour
{

    public Animator coinAnimator;
    public Animator selfAnimator;
    public AudioSource Audio;

    bool hit;
    //Vector2 movement = new Vector2(moveHorizontal, 0);


    SpringJoint2D spring;
    private void Start()
    {
        hit = false;
        spring = GetComponent<SpringJoint2D>();
        spring.frequency = 3f;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (!hit){
            // update animator state
            coinAnimator.SetTrigger("coinAnimation");
            selfAnimator.SetTrigger("used");
            print("HIT");
            hit = true;

            
        }
    }

    public void RestartButtonCallback(int input)
    {
        hit = false;
        spring.frequency = 3f;
        coinAnimator.SetTrigger("reset");
        selfAnimator.SetTrigger("reset");
        print("Restttinggggg");
    }

    public void frozen()
    {
        spring.frequency = 0f;
    }

    public void PlayJumpSound()
    {
        // play jump sound
        Audio.PlayOneShot(Audio.clip);
    }


}