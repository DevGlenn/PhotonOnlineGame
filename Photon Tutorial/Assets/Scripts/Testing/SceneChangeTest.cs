using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneChangeTest : MonoBehaviour
{

    public Text valueText;
    private void Start()
    {
        valueText.text = MasterManager.Instance.value.ToString();
    }

    // Update is called once per frame
   public void ChangeScene01()
   {
        SceneManager.LoadScene("Scene_01");
        MasterManager.Instance.value++;
   }

    public void ChangeScene02()
    {
        SceneManager.LoadScene("Scene_02");
        MasterManager.Instance.value++;
    }
}
