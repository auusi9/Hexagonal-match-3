using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _hexagonalTilePrefabs;
    [SerializeField] private GameObject[] _blockPrefabs;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private int _width;
    [SerializeField] private int _height;
    [SerializeField] private Sprite _tileSample;
    [SerializeField] private ScoreSystem _scoreSystem;

    private ComponentPool<Block>[] _componentPools;
    private Tile[][] _tileMap;
    
    void Start()
    {
        InitializePools();
        
        _tileMap = new Tile[_width][];

        for (int i = 0; i < _width; i++)
        {
            _tileMap[i] = new Tile[_height];
        }
        
        float tileWidth = _tileSample.rect.width / _tileSample.pixelsPerUnit;
        float tileHeight = (3*(_tileSample.rect.height / _tileSample.pixelsPerUnit))/ 4f;
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 30);
        
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                float nextTileY = j * tileWidth;

                if (i % 2 == 1)
                {
                    nextTileY += tileWidth / 2f;
                }
                
                GameObject gameObject = Instantiate(GetRandomTilePrefab(), new Vector3(i * tileHeight, nextTileY, 0), defaultRotation, transform);

                _tileMap[i][j] = gameObject.GetComponent<Tile>();
                _tileMap[i][j].Initialize(i, j);
            }
        }

        GenerateBlocks();
    }

    private void InitializePools()
    {
        _componentPools = new ComponentPool<Block>[_blockPrefabs.Length];
        for (int i = 0; i < _blockPrefabs.Length; i++)
        {
            Block.BlockType type = _blockPrefabs[i].GetComponent<Block>().Type;
            _componentPools[(int)type] = new ComponentPool<Block>(12, _blockPrefabs[i], _poolContainer, Quaternion.identity);
        }
    }

    private void GenerateBlocks()
    {
        for (uint i = 0; i < _width; i++)
        {
            for (uint j = 0; j < _height; j++)
            {
                _tileMap[i][j].CurrentBlock = GetRandomBlockPrefab();
            }
        }
    }

    private GameObject GetRandomTilePrefab()
    {
        return _hexagonalTilePrefabs[Random.Range(0, _hexagonalTilePrefabs.Length)];
    }
    
    private Block GetRandomBlockPrefab()
    {
        Block newBlock = _componentPools[Random.Range(0, _componentPools.Length)].GetComponent();
        newBlock.gameObject.SetActive(true);
        return newBlock;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        for (uint i = 0; i < _width; i++)
        {
            for (uint j = 0; j < _height; j++)
            {
                if (_tileMap[i][j].CurrentBlock != null)
                {
                    ReturnBlockToPool(_tileMap[i][j]);
                }

                _tileMap[i][j].CurrentBlock = GetRandomBlockPrefab();
                _scoreSystem.RestartScore();
            }
        }
    }

    private void ReturnBlockToPool(Tile tile)
    {
        _componentPools[(int) tile.CurrentBlock.Type].ReturnMono(tile.CurrentBlock);
        tile.CurrentBlock = null;
    }

    public void Match(List<Tile> matchingTiles)
    {
        _scoreSystem.NewMatch(matchingTiles.Count);
        foreach (Tile tile in matchingTiles)
        {
            ReturnBlockToPool(tile);
        }
        
        foreach (Tile tile in matchingTiles)
        {
            FallDownBlocks(tile);
        }
    }

    private void FallDownBlocks(Tile tile)
    {
        if (tile.CurrentBlock != null)
        {
            return;
        }
        
        List<Block> blocksAvailable = new List<Block>();
        for (int i = 0; i < _tileMap[tile.position.x].Length; i++)
        {
            if (_tileMap[tile.position.x][i].CurrentBlock != null)
            {
                blocksAvailable.Add(_tileMap[tile.position.x][i].CurrentBlock);
            }
        }

        int blocksToAdd = _tileMap[tile.position.x].Length - blocksAvailable.Count;

        for (int i = 0; i < blocksToAdd; i++)
        {
            blocksAvailable.Add(GetRandomBlockPrefab());
        }

        for (int i = 0; i < _tileMap[tile.position.x].Length; i++)
        {
            _tileMap[tile.position.x][i].CurrentBlock = blocksAvailable[i];
        }
    }
}

