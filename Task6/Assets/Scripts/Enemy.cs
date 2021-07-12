using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
