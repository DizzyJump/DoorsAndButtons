using Unity.Mathematics;
using UnityEngine;

namespace CodeBase.Services.InputService
{
    public class InputService : IInputService
    {
        public bool CheckUserInput(out float3 moveToPosition)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // TODO: избавится
                float distance = 0;
                if (plane.Raycast(ray, out distance))
                {
                    var targetPoint = ray.GetPoint(distance);
                    targetPoint.y = 0;
                    moveToPosition = targetPoint;
                    return true;
                }
            }

            moveToPosition = float3.zero;
            return false;
        }
    }
}