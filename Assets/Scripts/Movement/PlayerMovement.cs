using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementType
    {
        RigidbodyVelocity,
        RigidbodyAddForce,
        VectorMoveTowards,
        TransformTranslate,
        DirectPostitionChange
    }

    [SerializeField] private float speed = 3f;

    private Rigidbody2D body;

    private Vector2 axisMovement;

    [SerializeField] private MovementType movementType = MovementType.RigidbodyVelocity;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisMovement.x = Input.GetAxisRaw("Horizontal");
        axisMovement.y = Input.GetAxisRaw("Vertical");

        if(movementType == MovementType.VectorMoveTowards)
        {
            MoveTowards();
        }

        if (movementType == MovementType.TransformTranslate)
        {
            Translate();
        }

        if (movementType == MovementType.DirectPostitionChange)
        {
            PositionChange();
        }
    }

    private void FixedUpdate()
    {
        if(movementType == MovementType.RigidbodyVelocity)
        {
            RigidbodyVelocity();
        }

        if(movementType == MovementType.RigidbodyAddForce)
        {
            RigidbodyAddForce();
        }
    }


    #region Movement 1: Rigidbody Velocity

    private void RigidbodyVelocity()
    {
        body.velocity = axisMovement.normalized * speed;
    }

    #endregion

    #region Movement 2: Rigidbody Force

    private void RigidbodyAddForce()
    {
        body.AddForce(axisMovement * speed, ForceMode2D.Impulse);
    }

    #endregion


    private void MoveTowards()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            transform.position + (Vector3)axisMovement, speed * Time.deltaTime);
    }


    private void Translate()
    {
        transform.Translate(axisMovement * speed * Time.deltaTime);
    }


    private void PositionChange()
    {
        transform.position += (Vector3)axisMovement * Time.deltaTime * speed;
    }

}
