using System;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    private static GameConfig _instance;

    [SerializeField] GameConfigSO SO;
    public static GameConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<GameConfig>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GameConfig");
                    _instance = go.AddComponent<GameConfig>();
                }
            }
            return _instance;
        }
    }

    public void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameConfigSO GetGameConfigSO()
    {
        return SO;
    }

}
