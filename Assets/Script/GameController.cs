using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;

    List<string> texts;
    int index = 0;


    // Start is called before the first frame update
    void Start()
    {
        texts = new List<string>()
        {
            "昔々あるところに",
            "お兄さんとお姉さんがいました",
            "お兄さんは森に体を鍛えに",
            "お姉さんは村にパンを買いに行きました"
        };
        message.text = texts[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            index++;
            if (index == texts.Count)
            {
                SceneManager.LoadScene("Scene2");
            }
            index %= texts.Count;
            message.text = texts[index];
        }
    }
}
