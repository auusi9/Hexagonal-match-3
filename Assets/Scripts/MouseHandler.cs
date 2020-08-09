using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MouseHandler : MonoBehaviour
{    
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _collisionMask;

    public event UnityAction<Tile> MousePressedOverTile;
    public event UnityAction MouseUp;

    private void Update()
    {       
        if (Input.GetMouseButtonUp(0))
        {
            InvokeMouseUp();
            return;
        }
        
        if(EventSystem.current.IsPointerOverGameObject()) 
        {
            return;
        }
        
        if (Input.GetMouseButton(0))
        {
            MousePressed();
        }
    }
    
    private void InvokeMouseUp()
    {
        MouseUp?.Invoke();
    }
    
    private void InvokeMousePressedOverTile(Tile tile)
    {
        MousePressedOverTile?.Invoke(tile);
    }

    private void MousePressed()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hit =
            Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), -Vector2.up, 20, _collisionMask);

        if (hit.collider != null)
        {
            Tile tile = hit.collider.GetComponent<Tile>();

            if (IsValidTile(tile))
            {
                InvokeMousePressedOverTile(tile);
            }
        }
    }

    private static bool IsValidTile(Tile tile)
    {
        return tile != null && tile.CurrentBlock != null;
    }
}