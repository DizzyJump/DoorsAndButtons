using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour
{
    [Inject] GameLogicEngine engine;
    [Inject] LevelConfig levelConfig;

    private void Start()
    {
        //engine = new GameLogicEngine();
        //engine.Init(levelConfig, false);
    }

    private void Update()
    {
        CheckUserInput();
        engine?.Update(Time.deltaTime);
    }

    void CheckUserInput()
    {
        if(engine != null && Input.GetMouseButtonUp(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance = 0;
            if(plane.Raycast(ray, out distance))
            {
                var targetPoint = ray.GetPoint(distance);
                targetPoint.y = 0;
                engine.SetInput(targetPoint);
            }

        }
    }

    private void OnDestroy()
    {
        engine?.Shootdown();
    }
}
