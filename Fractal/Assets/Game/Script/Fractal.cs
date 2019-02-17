using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fractal : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _IncreaseText;
    [SerializeField] private GameObject _ParentObject;
    [SerializeField] private GameObject _LRPrefab;
    [SerializeField] private float _CircleSize;
    [SerializeField] private int _Iterations;
    [Header("Settings")]
    [SerializeField] private float _Increase;
    [SerializeField] private float _UpdateSpeed;
    [SerializeField] private bool _AutoIncrease;
    [SerializeField] private float _XIncrease;
    [SerializeField] private float _YIncrease;


    private Vector3[] _CirclePoints;
    private float _CheckIncreaseAmount = -1;
    private LineRenderer[] _LineR;

    void Start()
    {
        _LineR = new LineRenderer[_Iterations];
        for (int i = 0; i < _Iterations; i++)
        {
            GameObject obj = Instantiate(_LRPrefab);
            obj.transform.parent = _ParentObject.transform;
            _LineR[i] = obj.GetComponent<LineRenderer>();
        }
        _CirclePoints = GetPoints();
    }

    void Update()
    {
        if(_CheckIncreaseAmount != _Increase)
        {
            _IncreaseText.text = _Increase.ToString();
            StartCoroutine(UpdateNewPoints());
            _CheckIncreaseAmount = _Increase;
            _CirclePoints = GetPoints();
        }
    }

    private IEnumerator UpdateNewPoints()
    {
        for (int i = 0; i < _Iterations; i++)
        {
            _LineR[i].SetPositions(GetPointsUsing(i));
           yield return new WaitForSeconds(_UpdateSpeed);
        }
        //yield return new WaitForSeconds(_UpdateSpeed);
        if (_AutoIncrease)
        _Increase += 1;
    }

    private Vector3[] GetPoints()
    {
        _CirclePoints = new Vector3[Mathf.RoundToInt(_Iterations)];
        for (int i = 0; i < _Iterations; i++)
        {
            float angle = i * Mathf.PI * 2f / _Iterations;
            _CirclePoints[i] = new Vector3(Mathf.Cos(angle * _XIncrease), 0, Mathf.Sin(angle * _YIncrease)) * _CircleSize;
        }
        return _CirclePoints;
    }

    private Vector3[] GetPointsUsing(float current)
    {
        Vector3[] points = new Vector3[2];
        points[0] = _CirclePoints[Mathf.RoundToInt(current)];

        if (current * _Increase >= _Iterations - 1)
        {
            points[1] = _CirclePoints[Mathf.RoundToInt((current * _Increase) - ((Mathf.FloorToInt(current * _Increase / _Iterations)) * _Iterations))];
        }
        else
        {
            points[1] = _CirclePoints[Mathf.RoundToInt(current * _Increase)];
        }

        return points;
    }
}
