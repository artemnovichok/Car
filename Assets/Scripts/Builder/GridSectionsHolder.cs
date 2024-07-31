using System.Collections.Generic;
using UnityEngine;

public class GridSectionsHolder : MonoBehaviour
{
    public List<GameObject> cells = new ();

    [SerializeField] private Transform _creatArea;
    private bool needCheck = true;

    private void Update()
    {
        if (needCheck == false)
        {
            return;
        }
        if(_creatArea.childCount != 0)
        {
            needCheck = false;
            cells.Clear();
            for (int i = 0; i < _creatArea.childCount; i++)
            {
                cells.Add(_creatArea.GetChild(i).gameObject);
            }
        }
    }

    public void SaveBlocksInGrid()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            SaveData.Instance.Save("BlockInGrid" + i, cells[i].GetComponent<CellSetup>().cellSettingHolder);
        }
        
    }
}
