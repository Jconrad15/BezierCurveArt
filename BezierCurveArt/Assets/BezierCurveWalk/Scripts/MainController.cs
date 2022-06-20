using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BezierCurveWalk
{
    public class MainController : MonoBehaviour
    {
        [SerializeField]
        private CurveCreator curveCreator;

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                curveCreator.CreateNewCurves();
            }
        }
    }
}