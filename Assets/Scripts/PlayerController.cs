using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < distance)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -distance)
            {
                MoveDown();
            }
    }
    private void MoveUp()
    {
        Vector3 upMoveDistance = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);

        transform.localPosition = upMoveDistance;
    }
    private void MoveDown()
    {
        Vector3 downMoveDistance = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);

        transform.localPosition = downMoveDistance;
    }
}
