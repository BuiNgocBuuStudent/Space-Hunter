using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private BoostManager boostManager;
    private Animator animator;

    [SerializeField] private float distance;
    [SerializeField] private float limitPosY;
    // Start is called before the first frame update
    void Start()
    {
        boostManager = GameObject.Find("Boost Manager").GetComponent<BoostManager>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver && !GameManager.Instance.isGamePause)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < limitPosY)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -limitPosY)
            {
                MoveDown();
            }
        }
    }
    private void MoveUp()
    {
        Vector3 upPos = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);

        transform.position = upPos;
    }
    private void MoveDown()
    {
        Vector3 downPos = new Vector3(transform.position.x, transform.position.y - distance, transform.position.z);

        transform.position = downPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over!!!");

            GameManager.Instance.isGameOver = true;
            if(animator != null)
            {
                animator.SetTrigger("DieTrigger");
            }
            GameManager.Instance.SetGameOverUI();
        }
        if (other.gameObject.CompareTag("Boost"))
        {
            GameManager.Instance.isGamePause = true;
            boostManager.showBoostPopup();
            Destroy(other.gameObject);
        }
    }
}
