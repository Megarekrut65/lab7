﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_scene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}