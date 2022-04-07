using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    [SerializeField] Button _menuBTN;
    [SerializeField] string _levelTXT;

    void Start()
    {
        _menuBTN.onClick.AddListener(() => GetComponent<StartMenu>().OpenLevel(_levelTXT));
        StartCoroutine(FindObjectOfType<StartMenu>().ButtonDance(_menuBTN));
    }
}
