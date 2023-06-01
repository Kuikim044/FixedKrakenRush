using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public PlayerController controller;
    public PlayerData playerData;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            return;
        }
        controller.OnCharactorColliderHit(collision.collider);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            return;

        if (other.gameObject.CompareTag("Coin"))
        {
            GamePlayManager.coin += 10 * (int)GamePlayManager.multiplierCoin;
        }

        if (other.gameObject.CompareTag("MultiplierCoin"))
        {
            Singleton.Instance.isMultiplierCoin = true;
            GamePlayManager.multiplierCoin = 2;
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("MultiplierScore"))
        {
            Singleton.Instance.isMultiplierScore = true;
            GamePlayManager.multiplierScore = 2f;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Obstrucle") )
        {
            controller.OnCharactorColliderHit(other);
        }
    }
}