using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform targetPoint;

    private Animator animator;
    private Vector3 startPoint;
    private int[,] data;

    void Start()
    {
        JoysctickController.instance.characterMovement += Move;
        animator = GetComponent<Animator>();
        JoysctickController.instance.Stop += StopMoving;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void StopMoving()
    {
        animator.SetBool("walk", false);
    }
    private void Move(Vector3 targetMove)
    {
        targetPoint.position = transform.position + targetMove;
        transform.LookAt(targetPoint);
        transform.Translate(targetMove * speed * Time.deltaTime, Space.World);
        animator.SetBool("walk", true);
    }
}
