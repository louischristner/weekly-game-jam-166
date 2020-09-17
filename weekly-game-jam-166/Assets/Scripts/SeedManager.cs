using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SeedManager : MonoBehaviour
{
    public int value = 0;
    public Text seedText;

    int maxSeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        int[] seedsPerLevel = {1, 1, 2, 3};

        maxSeed = seedsPerLevel[SceneManager.GetActiveScene().buildIndex];
        value = maxSeed;
    }

    // Update is called once per frame
    void Update()
    {
        seedText.text = value.ToString() + " / " + maxSeed.ToString();
    }
}
