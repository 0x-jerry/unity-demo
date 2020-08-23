using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AplicationCofnig : MonoBehaviour
{

    [Range(60, 240)]
    public int FPS = 60;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = FPS;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
