using System.Collections.Generic;
using UnityEngine;

public class TileSelector : MonoBehaviour
{
    public const int MinimumMatchNumber = 3;
    
    [SerializeField] private MouseHandler _mouseHandler;
    [SerializeField] private GameFieldGenerator _gameFieldGenerator;
    [SerializeField] private GameObject _focusPrefab;
    
    private ComponentPool<Transform> _focusPool;
    private List<Tile> _currentTiles = new List<Tile>();
    
    private void Start()
    {
        _focusPool = new ComponentPool<Transform>(6, _focusPrefab, transform, Quaternion.Euler(0, 0, 30));

        _mouseHandler.MouseUp += TryMatchAndDeselect;
        _mouseHandler.MousePressedOverTile += MouseOnTile;
    }

    private void OnDestroy()
    {
        _mouseHandler.MouseUp -= TryMatchAndDeselect;
        _mouseHandler.MousePressedOverTile -= MouseOnTile;
    }

    private void TryMatchAndDeselect()
    {
        if (_currentTiles.Count >= MinimumMatchNumber)
        {
            _gameFieldGenerator.Match(_currentTiles);
        }

        for (int i = 0; i < _currentTiles.Count; i++)
        {
            _focusPool.ReturnMono(_currentTiles[i].FocusReference);
            _currentTiles[i].FocusReference = null;
        }

        _currentTiles.Clear();
    }

    private void MouseOnTile(Tile tile)
    {
        if (!IsTileAlreadySelected(tile))
        {
            if (_currentTiles.Count == 0)
            {
                SelectTile(tile);
            }
            else
            {
                if (IsBlockTheSameType(tile) && AreNeighboursWithLastSelected(tile))
                {
                    SelectTile(tile);
                }
            }
            
            return;
        }
        
        if (!IsLastSelectedTile(tile))
        {
            for (int i = _currentTiles.Count - 1; i > 0; i--)
            {
                if (_currentTiles[i] == tile)
                {
                    break;
                }

                DeselectTile(_currentTiles[i]);
            }
        }
    }

    private bool AreNeighboursWithLastSelected(Tile tile)
    {
        return _currentTiles[_currentTiles.Count - 1].Neighbour(tile);
    }

    private bool IsBlockTheSameType(Tile tile)
    {
        return _currentTiles[0].CurrentBlock.Type == tile.CurrentBlock.Type;
    }

    private bool IsTileAlreadySelected(Tile tile)
    {
        return _currentTiles.Contains(tile);
    }

    private bool IsLastSelectedTile(Tile tile)
    {
        return _currentTiles[_currentTiles.Count - 1] == tile;
    }

    private void SelectTile(Tile tile)
    {
        _currentTiles.Add(tile);
        Transform focusTransform = _focusPool.GetComponent();
        focusTransform.gameObject.SetActive(true);
        focusTransform.position = tile.transform.position;
        tile.FocusReference = focusTransform;
    }
    
    private void DeselectTile(Tile tile)
    {
        _focusPool.ReturnMono(tile.FocusReference);
        tile.FocusReference = null;
        _currentTiles.Remove(tile);
    }  
}