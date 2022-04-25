using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private int _pileSize;
    [SerializeField] private Coin _silverTemplate;
    [SerializeField] private Coin _goldTemplate;
    [SerializeField] private Transform _buildPoint;

    private List<Coin> _coins;
    private int _coinSilver;
    private int _goldSilver;

    private void Start()
    {
        _coinSilver = 0;
        _goldSilver = 0;

        _coins = new List<Coin>();

        Transform currentPoint = _buildPoint;

        for (int i = 0; i < _pileSize; i++)
        {
            Coin newCoin = BuildCoin(currentPoint);
            _coins.Add(newCoin);
            currentPoint = newCoin.transform;
        }
    }
    private Coin BuildCoin(Transform currentBuildPoint)
    {
        return Instantiate(GetRandomCoin(), GetBuildPoint(currentBuildPoint), Quaternion.identity, _buildPoint);
    }

    private Vector3 GetBuildPoint(Transform currentCoin)
    {
        return new Vector3(transform.position.x, currentCoin.position.y + currentCoin.localScale.y / 2 + _silverTemplate.transform.localScale.y / 2, transform.position.z);
    }

    private Coin GetRandomCoin()
    {
        int random = Random.Range(0, 100);
        if (random < 50 && _coinSilver < _pileSize / 2)
        {
            _coinSilver++;
            return _silverTemplate;
        }
        else if (_goldSilver < _pileSize / 2)
        {
            _goldSilver++;
            return _goldTemplate;
        }
        else
            return _silverTemplate;
    }
}
