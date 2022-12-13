using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportToSceneComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Collided");
        SceneManager.LoadScene(1);
    }
}
