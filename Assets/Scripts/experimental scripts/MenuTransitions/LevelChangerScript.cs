using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChangerScript : MonoBehaviour
{
    public Animator animator;

    private int levelToLoad;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel(int lvlIndex) 
    {
        levelToLoad = lvlIndex;
        animator.SetTrigger("Fade");
    }

    public void OnFadeComplete() {
        SceneManager.LoadScene(1);
    }
}
