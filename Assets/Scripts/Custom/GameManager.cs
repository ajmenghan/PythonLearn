using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;
    void Awake()
    {
        Instance = this;
    }

    private MapControl _mapControl = null;
    public MapControl mapControl
    {
        get
        {
            if (_mapControl == null)
                _mapControl = GetComponentInChildren<MapControl>();
            return _mapControl;
        }
    }   
    
}
