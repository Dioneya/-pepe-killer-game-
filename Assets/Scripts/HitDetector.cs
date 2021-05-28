using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "EnemyKick")
            return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
