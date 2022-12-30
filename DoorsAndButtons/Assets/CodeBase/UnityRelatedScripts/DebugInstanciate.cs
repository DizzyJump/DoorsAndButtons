using System.Collections;
using System.Collections.Generic;
using CodeBase.UnityRelatedScripts.ViewFactories;
using UnityEngine;
using Zenject;

public class DebugInstanciate : MonoBehaviour
{
    public IGameplayViewsFactory factory;

    public GameObject prefab;

    [Inject]
    void Construct(IGameplayViewsFactory factory)
    {
        this.factory = factory;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            factory.CreatePlayer(Vector3.zero);
        
        if (Input.GetKeyDown(KeyCode.F2))
            GameObject.Instantiate(prefab);
    }
}
