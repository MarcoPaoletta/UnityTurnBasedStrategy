using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;

    private int turnNumber = 1;

    public void NextTurn()
    {
        turnNumber++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one ActionSystem!");
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public int GetTurnNumber()
    {
        return turnNumber;
    }
}
