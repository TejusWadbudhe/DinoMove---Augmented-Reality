using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Drive : MonoBehaviour
{
    float speed = 1.0f;
    float rotSpeed = 50.0f;
    Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        float translation = CrossPlatformInputManager.GetAxis("Vertical") * speed;
        float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * rotSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);  //for z movement
        transform.Rotate(0, rotation, 0);    //for y movement

        if (translation > 0)  //w is pressed
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", 1.0f);
        }
        else if (translation < 0)   //s is pressed
        {
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", -1.0f);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");
        }

    }
}
