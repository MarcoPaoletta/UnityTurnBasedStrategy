using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public event EventHandler OnAnyUnitMovedGridPosition;

    [SerializeField] private Transform gridDebugObjectPrefab;

    private GridSystem gridSystem;

    public static LevelGrid Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one Level grid!");
            Destroy(gameObject);
            return;
        }
        
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f);
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }

    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }

    public List<Unit> GetUnitListGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitAtGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);

        OnAnyUnitMovedGridPosition?.Invoke(this, EventArgs.Empty);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return gridSystem.GetWorldPosition(gridPosition);
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);      
        return gridObject.HasAnyUnit();
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);      
        return gridObject.GetUnit();
    }

    public int GetWidth()
    {
        return gridSystem.GetWidth();
    }

    public int GetHeight()
    {
        return gridSystem.GetHeight();
    }
}
