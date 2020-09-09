using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoysctickController : MonoBehaviour
{
    public Transform touch_marker;
    Vector3 target_vector;
    Vector3 init_pos;
    public delegate void Move(Vector3 target_move);
    public Move characterMovement;

    public delegate void StopMoving();
    public StopMoving Stop;
    public static JoysctickController instance;
    // Start is called before the first frame update
    private void Awake()
    {

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void OnDestroy()
    {
        instance = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.position;
        touch_marker.position = init_pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 touch_pos = Input.mousePosition;
            target_vector = touch_pos - init_pos;
            if (target_vector.magnitude <= 100)
            {
                touch_marker.position = touch_pos;
                characterMovement(new Vector3(target_vector.x, 0, target_vector.y));
            }
        }
        else
        {
            touch_marker.position = init_pos;
            Stop();
        }
    }
}
