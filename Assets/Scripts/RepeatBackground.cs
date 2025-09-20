using System.Collections;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] float _scrollSpeed;
    [SerializeField] float _speedIncrement;
    [SerializeField] float _timeInterval;
    private float _offset;
    private Material _mat;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _mat = GetComponent<Renderer>().material;
        StartCoroutine(increaseScrollSpeed());
    }


    void Update()
    {
        if (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            _offset += (Time.deltaTime * _scrollSpeed) / 10;
            _mat.SetTextureOffset("_MainTex", new Vector2(_offset, 0));
        }
    }
    IEnumerator increaseScrollSpeed()
    {
        while (!_gameManager.isGameOver && !_gameManager.isGamePause)
        {
            yield return new WaitForSeconds(_timeInterval);
            _scrollSpeed += _speedIncrement;
        }

    }
}