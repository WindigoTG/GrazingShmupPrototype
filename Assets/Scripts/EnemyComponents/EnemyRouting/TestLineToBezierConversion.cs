using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    [ExecuteInEditMode]
    public class TestLineToBezierConversion : MonoBehaviour
    {
        [SerializeField] LineRenderer _controlLine;
        [SerializeField] int _segmentsCount = 100;
        [SerializeField] LineRenderer _resultLine;

        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _resultLine.positionCount = _segmentsCount;

            for (int i = 0; i < _segmentsCount; i++)
            {
                float t = (float)i / (float)(_segmentsCount - 1);
                Vector3 point = CalculateBezierPoint(t, _controlLine.GetPosition(0), _controlLine.GetPosition(1), _controlLine.GetPosition(2), _controlLine.GetPosition(3));
                _resultLine.SetPosition(i, point);
            }
        }

        Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;
            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;
            return p;
        }

        public Vector3[] GetRoute()
        {
            Vector3[] positions = new Vector3[_segmentsCount];
            _resultLine.GetPositions(positions);
            return positions;
        }
    }
}