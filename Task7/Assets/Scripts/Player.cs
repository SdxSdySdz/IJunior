using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int _coinAmount;

    public void CollectCoin()
    {
        _coinAmount++;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
