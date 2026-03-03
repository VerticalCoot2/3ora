using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float turnSpeed = 140;

    [SerializeField] bool grounded = false;


    private Material BodyColor;
    private Material white;
    bool blinking = false;

    Rigidbody rb;
    float jumpForce = 35;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        BodyColor = transform.GetChild(0).GetComponent<Renderer>().material;

        white = transform.GetChild(0).
            transform.GetChild(0).
            transform.GetChild(0).GetComponent<Renderer>().material;
    }

    void Start()
    {
        StartCoroutine(BlinkAction());
    }
    
    [SerializeField] void Blink()
    {
        if(!blinking)
        {
            blinking = true;
            StartCoroutine(BlinkAction());
        }
    }

    IEnumerator BlinkAction()
    {
        while(true)
        {
            GameObject eye1 = transform.GetChild(0).
            transform.GetChild(0).
            transform.GetChild(0).gameObject;

            GameObject eye2 = transform.GetChild(0).
                transform.GetChild(0).
                transform.GetChild(1).gameObject;

            GameObject innerEye1 = eye1.transform.GetChild(0).gameObject;
            GameObject innerEye2 = eye2.transform.GetChild(0).gameObject;

            eye1.GetComponent<Renderer>().material = BodyColor;
            eye2.GetComponent<Renderer>().material = BodyColor;

            innerEye1.SetActive(false);
            innerEye2.SetActive(false);

            yield return new WaitForSeconds(0.1f);

            eye1.GetComponent<Renderer>().material = white;
            eye2.GetComponent<Renderer>().material = white;

            innerEye1.SetActive(true);
            innerEye2.SetActive(true);

            yield return new WaitForSeconds(Random.Range(4f, 6f));
        }
    }

    void Update()
    {
        #region kevesbeJoMovement
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(transform.position, Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(transform.position, Vector3.up, -turnSpeed * Time.deltaTime);
        }
            #endregion

        if(Input.GetKey(KeyCode.Space))
        {
            if(grounded)
            {
                rb.AddForce(new Vector3(0, jumpForce*10, 0));
                grounded = false;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        //if(col.gameObject.tag == "Ground")
        //{
        //    grounded = true;
        //}

        switch (col.gameObject.tag)
        {
            case "Ground":
                grounded = true;
                break;
        }
    }


}
