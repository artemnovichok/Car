using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel : MonoBehaviour
{
    public void finishLevel()
    {
        EventManager.Instance.FinishLevel();
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        EventManager.Instance.FinishLevel();
        if(PlayerPrefs.GetInt("numLevelWasSelected") != 29)
        {
            for (int i = 0; i < ItemHolder.Instance.GetAllBlocksLength(); i++)
            {

                for (int j = 0; j < ItemHolder.Instance.GetBlockInAllBlocks(i).Blocks.Length; j++)
                {
                    ItemHolder.Instance.GetBlockInAllBlocks(i).Blocks[j].CountBlocks = ItemHolder.Instance.GetBlockInAllBlocks(i).Blocks[j].CountBlocksMax;
                    ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocks = ItemHolder.Instance.blockDelivery.Blocks[0].CountBlocksMax;
                }
            }
            PlayerPrefs.SetInt("numLevelWasSelected", (PlayerPrefs.GetInt("numLevelWasSelected") + 1));
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
