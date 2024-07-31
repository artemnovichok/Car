using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
[ExecuteAlways]

public class SetBoxCollider : MonoBehaviour
{
    private Image image;
    private BoxCollider2D boxCollider2D;

    private void Start()
    {
        image = GetComponent<Image>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        boxCollider2D.size = image.rectTransform.rect.size;
    }
}
