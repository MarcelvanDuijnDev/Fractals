using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mandelbrot : MonoBehaviour {
    [SerializeField] private GameObject _Parentobject;
    [SerializeField] private GameObject _PrefabObject;
    [SerializeField] private Vector2 _Grid;

    [SerializeField] private GameObject[] _GridObjects;
    [SerializeField] private float[] _Values;

    private Vector2 _GridPositions;

    void Start() {
        _Values = new float[Mathf.RoundToInt(_Grid.x * _Grid.y)];
        _GridObjects = new GameObject[Mathf.RoundToInt(_Grid.x * _Grid.y)];

        _GridPositions = _Grid;
        _GridPositions = new Vector2(_GridPositions.x = -_GridPositions.x * 0.5f, _GridPositions.y = -_GridPositions.y * 0.5f);
        Debug.Log(_GridPositions);
        CreateGrid();
    }


    void CreateGrid() {
        int check = 0;
        for (int i = 0; i < _Grid.y; i++) {
            for (int o = 0; o < _Grid.x; o++) {
                GameObject obj = Instantiate(_PrefabObject);
                obj.transform.position = new Vector3(_GridPositions.x + o, _GridPositions.y + i, 0);
                obj.transform.parent = _Parentobject.transform;
                _GridObjects[Mathf.RoundToInt((i * _Grid.x) + o)] = obj;
                float value1 = Mathf.Pow((_GridPositions.x + o), 2) * 0.01f;
                float value2 = Mathf.Pow((_GridPositions.y + i), 2) * 0.01f;
                float value3 = 0;
                if(value1 < value2) {
                    value3 = value2;
                }
                else {
                    value3 = value1;
                }
                _Values[Mathf.RoundToInt((i * _Grid.x) + o)] = value3;
            }
        }
        Mandlebrot();
    }

    void Mandlebrot() {
        for (int i = 0; i < _Grid.y; i++) {
            for (int o = 0; o < _Grid.x; o++) {

                float calcSize = _GridPositions.x + o;
                for (int p = 0; p < 5; p++) 
                {
                    _Values[Mathf.RoundToInt((i * _Grid.x) + o)] = Mathf.Pow(_Values[Mathf.RoundToInt((i * _Grid.x) + o)], 2);
                }
            }
        }

        for (int i = 0; i < _Values.Length; i++) {
            if(_Values[i] > 50 || _Values[i] < -50) 
            {
                _GridObjects[i].SetActive(false);
            }
        }
    }

    void Calc() 
    {

    }

}
