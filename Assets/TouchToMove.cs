using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToMove : MonoBehaviour
{
    public GameObject foodPrefab;
    GameObject food;
    Vector3 goalPos; //position of burger
    float speed = 0.5f, accuracy = 0.25f, rotSpeed = 2f;
    Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && food == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 1000.0f)){
                if (hit.collider.gameObject.tag == "ground")
                {
                    goalPos = hit.point;
                    food = Instantiate(foodPrefab, goalPos, Quaternion.identity);
                    Invoke("RemoveFood", 4.0f);
                }
            }
        }
    }

    void RemoveFood()
    {
        Destroy(food);
    }

    void LateUpdate()
    {
        Vector3 lookAtGoal = new Vector3(goalPos.x, this.transform.position.y, goalPos.z);
        Vector3 direction = llokAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                    Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);
        if(Vector3.Distance(transform.position, lookAtGoal) > accuracy)
        {
            this.transform.translate(0, 0, speed * Time.deltaTime);
            anim.SetBool("isRunning", true);
            anim.SetFloat("speed", 1.0f);
        }
        else
        {
            anim.SetBool("isRunning", false);
            if (food != null){
                anim.SetBool("isEating", true);
            }
            if(food == null)
            {
                anim.SetBool("isEating", false);
            }
        }

    }
}
