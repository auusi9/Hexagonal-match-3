using System;
using UnityEngine;
using Utils;

public class Tile : MonoBehaviour
{    
    public Transform FocusReference;
    public CustomPoint position;

    private Block _currentBlock;

    public void Initialize(int x, int y)
    {
        position.x = x;
        position.y = y;
    }

    public Block CurrentBlock
    {
        get { return _currentBlock; }
        set
        {
            _currentBlock = value;
            if (_currentBlock != null)
            {
                _currentBlock.transform.position = transform.position;
            }
        }
    }
    
    public bool Neighbour(Tile tile)
    {
        int odd = position.x % 2;

        CustomPoint[] customPoints = Neighbours[odd];

        CustomPoint offset = tile.position - position;
        
        return Array.Exists(customPoints, x => x.Equals(offset));
    }
    
    private static readonly CustomPoint[][] Neighbours =
    {
        new []{
            new CustomPoint(1, 0),
            new CustomPoint(1, -1),
            new CustomPoint(0, -1),
            new CustomPoint(-1, -1),
            new CustomPoint(-1, 0),
            new CustomPoint(0, 1) 
        }, 
        new []{
            new CustomPoint(1, 1),
            new CustomPoint(1, 0),
            new CustomPoint(0, -1),
            new CustomPoint(-1, 0),
            new CustomPoint(-1, 1),
            new CustomPoint(0, 1) 
        }
    };
}