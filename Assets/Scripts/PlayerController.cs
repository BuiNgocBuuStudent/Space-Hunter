using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float distance = 4.0f;
    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < distance)
            {
                transform.localPosition = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -distance)
            {
                transform.localPosition = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);
            }
    }
}
