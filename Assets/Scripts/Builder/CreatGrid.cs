using UnityEngine;
using UnityEngine.UI;

public class CreatGrid : MonoBehaviour
{
    [SerializeField] private GameObject _creatArea;
    [SerializeField] private int gridSize = 5;

    [SerializeField] private Sprite _rightSprite;
    [SerializeField] private Sprite _leftSprite;
    [SerializeField] private Sprite _upSprite;
    [SerializeField] private Sprite _downSprite;
    [SerializeField] private Sprite _centerSprite;
    [SerializeField] private Sprite _upLeftSprite;
    [SerializeField] private Sprite _downLeftSprite;
    [SerializeField] private Sprite _upRightSprite;
    [SerializeField] private Sprite _downRightSprite;

    void Start()
    {
        if (_creatArea == null)
        {
            Debug.LogError("Панель не найдена");
            return;
        }

        RectTransform panelRect = _creatArea.GetComponent<RectTransform>();
        float panelWidth = panelRect.rect.width;
        float panelHeight = panelRect.rect.height;

        float squareWidth = panelWidth / gridSize;
        float squareHeight = panelHeight / gridSize;

        for (int y = 0; y < gridSize; y++)
        {
            for (int x = 0; x < gridSize; x++)
            {
                GameObject square = new GameObject("Square_" + y + "_" + x);
                square.transform.SetParent(_creatArea.transform);
                square.layer = LayerMask.NameToLayer("Cells");

                Image squareImage = square.AddComponent<Image>();
                squareImage.color = new Color(1, 1, 1, 0); // Делает изображение прозрачным

                square.AddComponent<SetBoxCollider>();

                RectTransform squareRect = square.GetComponent<RectTransform>();
                squareRect.anchorMin = new Vector2(0, 1);
                squareRect.anchorMax = new Vector2(0, 1);
                squareRect.sizeDelta = new Vector2(squareWidth, squareHeight);
                squareRect.localScale = new Vector2(1, 1);
                squareRect.anchoredPosition = new Vector2(x * squareWidth + (squareWidth / 2), -y * squareHeight - (squareHeight / 2));

                square.AddComponent<CellSetup>();
                square.GetComponent<CellSetup>().WriteOurCoordinates();

                // Установка картинки в зависимости от положения квадрата, но компонент Image выключен
                if (x == 0 && y == 0)
                {
                    squareImage.sprite = _upLeftSprite;
                }
                else if (x == gridSize - 1 && y == 0)
                {
                    squareImage.sprite = _upRightSprite;
                }
                else if (x == 0 && y == gridSize - 1)
                {
                    squareImage.sprite = _downLeftSprite;
                }
                else if (x == gridSize - 1 && y == gridSize - 1)
                {
                    squareImage.sprite = _downRightSprite;
                }
                else if (x == 0)
                {
                    squareImage.sprite = _leftSprite;
                }
                else if (x == gridSize - 1)
                {
                    squareImage.sprite = _rightSprite;
                }
                else if (y == 0)
                {
                    squareImage.sprite = _upSprite;
                }
                else if (y == gridSize - 1)
                {
                    squareImage.sprite = _downSprite;
                }
                else
                {
                    squareImage.sprite = _centerSprite;
                }

                // Выключаем компонент Image
                squareImage.enabled = false;
            }
        }
    }
}
