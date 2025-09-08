using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private BoostManager _boostManager;
    private Animator _animator;

    [SerializeField] float _distance;
    [SerializeField] float _limitPosY;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;
        _boostManager = GameObject.Find("Boost Manager").GetComponent<BoostManager>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && transform.position.y < _limitPosY)
            {
                MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -_limitPosY)
            {
                MoveDown();
            }
        }
        if (_gameManager.isGamePause)
        {
            _animator.speed = 0f;
        }
        else
            _animator.speed = 1f;
    }
    private void MoveUp()
    {
        Vector3 upPos = new Vector3(transform.position.x, transform.position.y + _distance, transform.position.z);

        transform.position = upPos;
    }
    private void MoveDown()
    {
        Vector3 downPos = new Vector3(transform.position.x, transform.position.y - _distance, transform.position.z);

        transform.position = downPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            _gameManager.isGameOver = true;
            if (_animator != null)
            {
                _animator.SetTrigger("DieTrigger");
            }
            _gameManager.SetGameOverUI();
        }
        if (other.gameObject.CompareTag("Boost"))
        {
            _gameManager.isGamePause = true;
            Time.timeScale = 0f;
            _boostManager.showBoostPopup();
            other.gameObject.SetActive(false);
        }
    }
}
