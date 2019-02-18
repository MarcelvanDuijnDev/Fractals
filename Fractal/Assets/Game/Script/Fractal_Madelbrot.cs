using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal_Madelbrot : MonoBehaviour
{
    [SerializeField] private float _UpdateSpeed;

    [SerializeField] private MandelbrotOptions[] _Mandelbrot;

    [SerializeField] private GameObject _PrefabObject;
    [SerializeField] private GameObject _ParentObject;

    private float _CheckIncrease;

    void Start()
    {
        for (int i = 0; i < _Mandelbrot.Length; i++)
        {
            CreateNewIteration(i);
        }
    }

    void Update()
    {
        if(_CheckIncrease != _Mandelbrot[0]._P_Increase)
        {
            for (int i = 0; i < _Mandelbrot.Length; i++)
            {
                StartCoroutine(SetPoints(i));
            }
            _CheckIncrease = _Mandelbrot[0]._P_Increase;
        }
    }

    private void CreateNewIteration(int iterationID)
    {
        _Mandelbrot[iterationID]._P_CirclePoints = GetPoints(iterationID);
        _Mandelbrot[iterationID]._P_LineRenderer = new LineRenderer[_Mandelbrot[iterationID]._P_IterationSize];

        GameObject parentObj = Instantiate(_ParentObject);
        for (int i = 0; i < _Mandelbrot[iterationID]._P_IterationSize; i++)
        {
            GameObject obj = Instantiate(_PrefabObject);
            obj.transform.parent = parentObj.transform;
            _Mandelbrot[iterationID]._P_LineRenderer[i] = obj.GetComponent<LineRenderer>();
        }
        GetPoints(iterationID);
    }


    //Circle Points
    private Vector3[] GetPoints(int iterationID)
    {
        _Mandelbrot[iterationID]._P_CirclePoints = new Vector3[Mathf.RoundToInt(_Mandelbrot[iterationID]._P_IterationSize)];
        for (int i = 0; i < _Mandelbrot[iterationID]._P_IterationSize; i++)
        {
            float angle = i * Mathf.PI * 2 / _Mandelbrot[iterationID]._P_IterationSize;
            _Mandelbrot[iterationID]._P_CirclePoints[i] = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * _Mandelbrot[iterationID]._P_CircleSize;
        }
        return _Mandelbrot[iterationID]._P_CirclePoints;
    }

    private IEnumerator SetPoints(int iterationID)
    {
        for (int i = 0; i < _Mandelbrot[iterationID]._P_LineRenderer.Length; i++)
        {
            Vector3[] points = new Vector3[2];
            points[0] = _Mandelbrot[iterationID]._P_CirclePoints[Mathf.RoundToInt(i)];

            if (i * _Mandelbrot[iterationID]._P_Increase >= _Mandelbrot[iterationID]._P_IterationSize - 1)
            {
                points[1] = _Mandelbrot[iterationID]._P_CirclePoints[Mathf.RoundToInt((i * _Mandelbrot[iterationID]._P_Increase) - ((Mathf.FloorToInt(i * _Mandelbrot[iterationID]._P_Increase / _Mandelbrot[iterationID]._P_IterationSize)) * _Mandelbrot[iterationID]._P_IterationSize))];
            }
            else
            {
                points[1] = _Mandelbrot[iterationID]._P_CirclePoints[Mathf.RoundToInt(i * _Mandelbrot[iterationID]._P_Increase)];
            }
            _Mandelbrot[iterationID]._P_LineRenderer[i].SetPositions(points);
            yield return new WaitForSeconds(_UpdateSpeed);
        }
    }

    private Vector3[] GetPointsUsing(int iterationID, int lineRendereID)
    {
        Vector3[] points = new Vector3[2];
        points[0] = _Mandelbrot[iterationID]._P_CirclePoints[Mathf.RoundToInt(lineRendereID)];

        if (lineRendereID * _Mandelbrot[iterationID]._P_Increase >= _Mandelbrot[iterationID]._P_IterationSize - 1)
        {
            points[1] = _Mandelbrot[iterationID]._P_CirclePoints[Mathf.RoundToInt((lineRendereID * _Mandelbrot[iterationID]._P_Increase) - ((Mathf.FloorToInt(lineRendereID * _Mandelbrot[iterationID]._P_Increase / _Mandelbrot[iterationID]._P_IterationSize)) * _Mandelbrot[iterationID]._P_IterationSize))];
        }
        else
        {
            points[1] = _Mandelbrot[iterationID]._P_CirclePoints[Mathf.RoundToInt(lineRendereID * _Mandelbrot[iterationID]._P_Increase)];
        }

        return points;
    }
}


[System.Serializable]
public class MandelbrotOptions
{
    public float _P_CircleSize;
    public int _P_IterationSize;
    public int _P_Increase;
    public Vector3[] _P_CirclePoints;
    public LineRenderer[] _P_LineRenderer;
}
