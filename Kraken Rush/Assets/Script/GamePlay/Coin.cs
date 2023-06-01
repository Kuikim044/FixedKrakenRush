using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform playerTrans;
    public float moveSpeed = 30f;
    public float rotateSpeed = 100f;
    public float maxYPosition = 1.2f; 
    public float minYPosition = 0.8f;
    private bool isMovingUp = true;
    CoinMove coinMoveScript;
    public bool isPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();
    }
    private void Update()
    {
        if (isPrefab)
            return;

        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);


        if (isMovingUp)
        {
            transform.Translate(Vector3.up * 1f * Time.deltaTime);
            if (transform.position.y >= maxYPosition)
            {
                isMovingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * 1f * Time.deltaTime);
            if (transform.position.y <= minYPosition)
            {
                isMovingUp = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CoinDetector"))
        {
            coinMoveScript.enabled = true;
        }
    }
}
