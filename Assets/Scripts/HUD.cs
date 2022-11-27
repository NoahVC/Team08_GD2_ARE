using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Text _currentBalloonsFound = null;
    [SerializeField] Text _totalBalloonsInScene = null;
    private int _currentBalloons;

    public int currentBalloons
    {
        get { return _currentBalloons; }
        set { _currentBalloons = value; }
    }
    private int _totalBalloons = 5;


    // Update is called once per frame
    void Update()
    {
        SyncData();
    }
    void SyncData()
    {
        _totalBalloonsInScene.text = _totalBalloons.ToString();
        _currentBalloonsFound.text = _currentBalloons.ToString();
    }
}
