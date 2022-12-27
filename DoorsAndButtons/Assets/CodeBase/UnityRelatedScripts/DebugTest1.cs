using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest1 : MonoBehaviour
{
    private class Enums
    {
        public enum TestEnum
        {
            E1,
            E2,
            E3
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            Test();
    }
    
    void Test()
    {
        Debug.Log($"test: {Enums.TestEnum.E1.ToString().ToLowerInvariant()}");
    }
}
