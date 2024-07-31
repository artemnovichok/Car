using UnityEngine;

public class LevelEvent : MonoBehaviour
{
    public static LevelEvent Instance { get; private set; }

    private EventType typeEvent;
    private int currentLevel;
    private int currentTypeLevel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public EventType Init(int level = 0, int type = 999)
    {
        if (type == 999)
        {
            currentLevel = PlayerPrefs.GetInt("numLevelWasSelected") + (PlayerPrefs.GetInt("LvlTypeWasSelected") *30);
            currentTypeLevel = PlayerPrefs.GetInt("LvlTypeWasSelected");
        }
        else
        {
            currentLevel = level;
            currentTypeLevel = type;
        }

        SetLevelEventType();

        switch (typeEvent)
        {
            case EventType.UsualEvent:
                return EventType.UsualEvent;
            case EventType.BonusLevel:
                return EventType.BonusLevel;
            case EventType.FallingObjects:
                return EventType.FallingObjects;
            case EventType.Tornado:
                return EventType.Tornado;
            case EventType.Delivery:
                return EventType.Delivery;
            case EventType.LevelUp:
                return EventType.LevelUp;
        }
        return EventType.UsualEvent;
    }

    private void SetLevelEventType()
    {
        if (currentTypeLevel == 0)
        {
            if (currentLevel == 9)
            {
                typeEvent = EventType.BonusLevel;
            }
            else if (currentLevel == 13)
            {
                typeEvent = EventType.Delivery;
            }
            else if (currentLevel == 19)
            {
                typeEvent = EventType.BonusLevel;
            }
            else if (currentLevel == 22)
            {
                typeEvent = EventType.Delivery;
            }
            else if (currentLevel == 29)
            {
                typeEvent = EventType.BonusLevel;
            }
            else
            {
                typeEvent = EventType.UsualEvent;
            }
        }
        else if (currentTypeLevel == 1)
        {
            if (currentLevel == 32)
            {
                typeEvent = EventType.FallingObjects;
            }
            else if (currentLevel == 39)
            {
                typeEvent = EventType.BonusLevel;
            }
            else if (currentLevel == 42)
            {
                typeEvent = EventType.FallingObjects;
            }
            else if (currentLevel == 47)
            {
                typeEvent = EventType.Delivery;
            }
            else if (currentLevel == 48)
            {
                typeEvent = EventType.Tornado;
            }
            else if (currentLevel == 49)
            {
                typeEvent = EventType.BonusLevel;
            }
            else if (currentLevel == 53)
            {
                typeEvent = EventType.Delivery;
            }
            else if (currentLevel == 57)
            {
                typeEvent = EventType.FallingObjects;
            }
            else if (currentLevel == 59)
            {
                typeEvent = EventType.BonusLevel;
            }
            else
            {
                typeEvent = EventType.UsualEvent;
            }
        }
        else
        {
            if (currentLevel == 67)
            {
                typeEvent = EventType.Delivery;
            }
            else if (currentLevel == 69)
            {
                typeEvent = EventType.BonusLevel;
            }
            else if (currentLevel == 72)
            {
                typeEvent = EventType.FallingObjects;
            }
            else if (currentLevel == 79)
            {
                typeEvent = EventType.BonusLevel;
            }
            else if (currentLevel == 82)
            {
                typeEvent = EventType.Tornado;
            }
            else if (currentLevel == 86)
            {
                typeEvent = EventType.FallingObjects;
            }
            else if (currentLevel == 87)
            {
                typeEvent = EventType.Delivery;
            }
            else if (currentLevel == 89)
            {
                typeEvent = EventType.BonusLevel;
            }
            else
            {
                typeEvent = EventType.UsualEvent;
            }
        }

    }
}
