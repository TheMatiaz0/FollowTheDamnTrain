using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 12f;


    /// <summary>
    /// A little bit wonky movement due to lack of rigidbody, need to be improved
    /// </summary>
    protected void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        transform.position += (move * speed * Time.deltaTime);
    }
}
