using UnityEngine;
using UnityEngine.SceneManagement;

public class finished : MonoBehaviour
{
    public int LevelNum; 

    public void loadnextlevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + LevelNum);
    }
}
