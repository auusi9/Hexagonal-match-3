using System.Collections.Generic;
using UnityEngine;

public class ComponentPool<T> where T : Component
{
    private Queue<T> _pool = new Queue<T>();
    private GameObject _prefab;
    private Transform _parent;
    private Quaternion _defaultRotation;
    
    public ComponentPool(int size, GameObject prefab, Transform parent = null, Quaternion defaultRotation = default)
    {
        _prefab = prefab;
        _parent = parent;

        _defaultRotation = defaultRotation;
        
        if (_defaultRotation == default)
        {
            _defaultRotation = Quaternion.identity;
        }
        
        for (int i = 0; i < size; i++)
        {
            _pool.Enqueue(CreateNewInstance());
        }
    }

    public T GetComponent()
    {
        if (_pool.Count > 0)
        {
            return _pool.Dequeue();
        }

        return CreateNewInstance();
    }

    private T CreateNewInstance()
    {
        GameObject go = Object.Instantiate(_prefab, Vector3.zero, _defaultRotation, _parent);
        go.SetActive(false);
        
        return go.GetComponent<T>();
    }

    public void ReturnMono(T mono)
    {
        mono.gameObject.SetActive(false);
        _pool.Enqueue(mono);
    }

    public void ReturnMonos(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            ReturnMono(list[i]);
        }
    }
}