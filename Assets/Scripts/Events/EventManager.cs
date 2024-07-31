using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent updateMoney;

    public UnityEvent updateDimond;

    public UnityEvent<int> updateCounterBlock;

    public UnityEvent updateBlocks;

    public UnityEvent updateBlocksInGrid;

    public UnityEvent startMoveCar;

    public UnityEvent stopMoveCar;

    public UnityEvent updateCoinTextOnLevel;

    public UnityEvent finishLevel;

    public UnityEvent cantMove;

    public UnityEvent gameOver;

    public UnityEvent updateSkin;

    public UnityEvent updateFreez;
    public static EventManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateMoney()
    {
        updateMoney?.Invoke();
    }

    public void UpdateDimond()
    {
        updateDimond?.Invoke();
    }

    public void UpdateCounterBlock(int value)
    {
        updateCounterBlock?.Invoke(value);
    }

    public void UpdateBlocks()
    {
        updateBlocks?.Invoke();
    }

    public void UpdateBlocksInGrid()
    {
        updateBlocksInGrid?.Invoke();
    }

    public void FreezBLock()
    {
        updateFreez?.Invoke();
    }

    public void StartMoveCar()
    {
        startMoveCar?.Invoke();
    }

    public void StopMoveCar()
    {
        stopMoveCar?.Invoke();
    }

    public void UpdateCoinTextOnLevel()
    {
        updateCoinTextOnLevel?.Invoke();
    }

    public void FinishLevel()
    {
        finishLevel?.Invoke();
    }

    public void CantMove()
    {
        cantMove?.Invoke();
    }

    public void GameOver()
    {
        gameOver?.Invoke();
    }

    public void UpdateSkin()
    {
        updateSkin?.Invoke();
    }
}
