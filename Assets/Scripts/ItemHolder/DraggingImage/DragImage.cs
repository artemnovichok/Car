using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [HideInInspector]
    public int ourId = 0;
    [HideInInspector]
    public BlockRarity ourRarity = BlockRarity.Grey;
    [HideInInspector]
    public bool NotInfinityBlock = false;
    private SpriteRenderer spriteRenderer;
    private RectTransform rectTransform;
    private bool isDragging = false;
    private Vector2 offset;
    private Vector2 ourStartSize;

    private void OnEnable() => EventManager.Instance.updateBlocks.AddListener(setObject);
    private void OnDisable() => EventManager.Instance.updateBlocks.RemoveListener(setObject);


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rectTransform = GetComponent<RectTransform>();
        ourStartSize = rectTransform.sizeDelta;
        StartDragging();
    }

    public void StartDragging()
    {
        Vector2 touchPosition;

        if (Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
        }
        else
        {
            touchPosition = Input.mousePosition;
        }
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 touchPosition;

            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }
            else
            {
                touchPosition = Input.mousePosition;
            }
            transform.position = touchPosition;
        }
    }

    private void setObject()
    {
        if (canPutBlockHere(out GameObject go))
        {
            if (gameObject.GetComponent<CheckWereCanConnect>().CheckConnection(out float rotation, out bool connectedUp, out bool connectedDown, out bool connectedLeft, out bool connectedRight))
            {
                rectTransform.rotation = Quaternion.Euler(0, 0, rotation);

                go.GetComponent<CellSetup>().cellSettingHolder.IsBlockHere = true;
                go.GetComponent<CellSetup>().cellSettingHolder.BlockID = ourId;
                go.GetComponent<CellSetup>().cellSettingHolder.BlocRarity = (int)ourRarity;
                go.GetComponent<CellSetup>().cellSettingHolder.Rotate = (int)rotation;
                go.GetComponent<CellSetup>().cellSettingHolder.ConnectUp = connectedUp;
                go.GetComponent<CellSetup>().cellSettingHolder.ConnectDown = connectedDown;
                go.GetComponent<CellSetup>().cellSettingHolder.ConnectLeft = connectedLeft;
                go.GetComponent<CellSetup>().cellSettingHolder.ConnectRight = connectedRight;

                rectTransform.sizeDelta = go.GetComponent<RectTransform>().sizeDelta;
                rectTransform.anchoredPosition = new Vector2(go.GetComponent<CellSetup>().cellSettingHolder.CellPosX, go.GetComponent<CellSetup>().cellSettingHolder.CellPosY);

                EventManager.Instance.UpdateCounterBlock(0);
            }
            else
            {
                Debug.Log("Не к чему присоединиться");
                destroyGO();
            }
        }
        else
        {
            destroyGO();
        }
    }

    private void destroyGO()
    {
        if (NotInfinityBlock == true)
        {
            if (ourId == 123123123)
            {
                if(ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocks + 1 <= ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocksMax)
                {
                    ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocks++;
                }
            }
            else
            {
                if (ItemHolder.Instance.GetBlockInAllBlocks(ourId).Blocks[(int)ourRarity].CountBlocks + 1 <= ItemHolder.Instance.GetBlockInAllBlocks(ourId).Blocks[(int)ourRarity].CountBlocksMax)
                {
                    ItemHolder.Instance.GetBlockInAllBlocks(ourId).Blocks[(int)ourRarity].CountBlocks++;
                }
            }
        }
        EventManager.Instance.UpdateCounterBlock(-1);
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransform.sizeDelta = ourStartSize;
        isDragging = true;
        offset = eventData.position - (Vector2)transform.position;
        RaycastHit2D hitCell = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Cells"));
        if (hitCell.collider != null)
        {
            GameObject go = hitCell.collider.gameObject;
            go.GetComponent<CellSetup>().cellSettingHolder.IsBlockHere = false;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        EventManager.Instance.UpdateBlocks();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            transform.position = eventData.position - offset;
        }
    }



    private bool canPutBlockHere(out GameObject cellGO)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("BlockImageLayer"));
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D ray in hits)
            {
                if (ray.collider.gameObject != gameObject)
                {
                    cellGO = null;
                    return false;
                }
            }
        }
        RaycastHit2D hitCell = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, LayerMask.GetMask("Cells"));
        if (hitCell.collider != null)
        {
            cellGO = hitCell.collider.gameObject;
            return true;
        }
        else
        {
            cellGO = null;
            return false;
        }
    }
}
