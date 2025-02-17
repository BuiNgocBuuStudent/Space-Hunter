using UnityEngine;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private float distance;
    public int bullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver)
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over");
            Destroy(other.gameObject);

            GameManager.Instance.isGameOver = true;
        }
    }
}
