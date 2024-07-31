using UnityEngine;

public class HPblock : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public int minSpeedToGetDMG;

    private static float damageCoefficient = 1f; //коэффицент урона
    private bool isWheel = false;
    private bool needTakeDamage = true;
    private bool needCheck;
    private Rigidbody rb;
    private int ourID;

    private bool isBonusLevel = false;
   

    private void Start()
    {
        isBonusLevel = LevelEvent.Instance.Init() == EventType.BonusLevel ? true : false;
        currentHp = maxHp;
        rb = GetComponent<Rigidbody>();
        ourID = GetComponent<CarBlocksSettings>().blockSettings.id;
        if (ourID == 7 || ourID == 3)
        {
            isWheel = true;
        }
    }

    private void Update()
    {
        if(!isWheel || !needCheck)
        {
            return;
        }
        float distance = 2f;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, distance);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject.CompareTag("ground"))
            {
                needTakeDamage = false;
                break;
            }
            else
            {
                needTakeDamage = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground")) //тег объекта с которым будет работать получение урона
        {
            needCheck = false;
            if(isWheel)
            {
                if (needTakeDamage)
                {
                    float speed = rb.velocity.magnitude;
                    if (speed >= minSpeedToGetDMG)
                    {
                        takeDamage((int)(speed * damageCoefficient));
                    }
                }
            }
            else
            {
                float speed = rb.velocity.magnitude;
                if (speed >= minSpeedToGetDMG)
                {
                    takeDamage((int)(speed * damageCoefficient));
                }
            }
            
        }
        if (collision.gameObject.CompareTag("DroppingBlock"))
        {
            Destroy(collision.gameObject);
            takeDamage(100000);
        }
        if (collision.gameObject.CompareTag("Tornado"))
        {
            takeDamage(100000);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            needCheck = true;
        }
    }

    


    private void takeDamage(int damage)
    {
        if (isBonusLevel)
        {
            return;
        }
        currentHp -= damage;
        if (currentHp <= 0)
        {
            if (gameObject.GetComponent<CarBlocksSettings>().blockSettings.id == 0 || gameObject.GetComponent<CarBlocksSettings>().blockSettings.id == 123123123)
            {
                EventManager.Instance.CantMove();
                Time.timeScale = 0f;
                EventManager.Instance.GameOver();
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
