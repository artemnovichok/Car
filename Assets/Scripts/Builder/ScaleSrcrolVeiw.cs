using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleSrcrolVeiw : MonoBehaviour
{
    private bool wasCounted = false;

    private void Update()
    {
        if (wasCounted)
        {
            return;
        }
        if (gameObject.transform.childCount > 0)
        {
            wasCounted = true;
            float startWidthContent = gameObject.GetComponent<RectTransform>().rect.width;
            int countStartAvaliable = (int)(startWidthContent / 100);

            int AllBlocksAtCurrentLevel = gameObject.transform.childCount;

            if (countStartAvaliable < AllBlocksAtCurrentLevel)
            {
                int howMushWeNededAddBlocks = AllBlocksAtCurrentLevel - countStartAvaliable;
                int scaleSize = (int)startWidthContent + (howMushWeNededAddBlocks * 200);

                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(scaleSize, gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
        }

    }
}
