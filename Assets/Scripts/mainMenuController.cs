using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using RG.OrbitalElements;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainMenuController : MonoBehaviour
{
    public void loadRadsterSim()
    {
            SceneManager.LoadScene("roadster");
    }

    public void loadLaunchBrowser()
    {
        SceneManager.LoadScene("browser");
    }
}
