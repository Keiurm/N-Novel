
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System;

public class GameControllerScene2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI message;

    [SerializeField] Transform buttonPanel;

    [SerializeField] Button optionButton;

    List<string> Texts;
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        Scenario scenario01 = new Scenario()
        {
            ScenarioID = "scenario01",
            Texts = new List<string>()
            {
                "「パンを買ったけれど、時間が余っちゃったな」"
            },
            NextScenarioID = "scenario02"
        };

        Scenario scenario02 = new Scenario()
        {
            ScenarioID = "scenario02",
            Texts = new List<string>()
            {
                "どうする?"
            },
            Options = new List<Option>()
            {
                new Option()
                {
                    Text = "ブラブラ散歩する",
                    Action=WalkAround,
                },
                new Option()
                {
                    Text = "家に帰る",
                    Action=GoHome,
                }
            }
        };
        scenarios.Add(scenario02);
        SetScenario(scenario01);
    }

    void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;
        message.text = currentScenario.Texts[0];
        if (currentScenario.Options.Count > 0)
        {
            SetNextMessage();
        }

    }

    void SetNextMessage()
    {
        if (currentScenario.Texts.Count > index + 1)
        {
            index++;
            message.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    void ExitScenario()
    {
        index = 0;
        if (currentScenario.Options.Count > 0)
        {
            SetButtons();
        }
        else
        {
            message.text = "";
            Scenario nextScenario = null;

            foreach (Scenario scenario in scenarios)
            {
                if (scenario.ScenarioID == currentScenario.NextScenarioID)
                {
                    nextScenario = scenario;
                }
            }
            if (nextScenario != null)
            {
                SetScenario(nextScenario);
            }
            else
            {
                currentScenario = null;
            }
        }
    }

    void SetButtons()
    {
        foreach (Option o in currentScenario.Options)
        {
            Button b = Instantiate(optionButton);
            TextMeshProUGUI text = b.GetComponentInChildren<TextMeshProUGUI>();
            text.text = o.Text;
            b.onClick.AddListener(() => o.Action());
            b.onClick.AddListener(() => ClearButtons());
            b.transform.SetParent(buttonPanel, false);
        }
    }

    void ClearButtons()
    {
        foreach (Transform t in buttonPanel)
        {
            Destroy(t.gameObject);
        }
    }

    public void WalkAround()
    {
        var scenario = new Scenario();
        scenario.NextScenarioID = "scenario02";
        scenario.Texts.Add("「良い天気だなぁ」");
        SetScenario(scenario);
    }

    public void GoHome()
    {
        var scenario = new Scenario();
        scenario.Texts.Add("「お家に帰らなきゃ」");
        scenario.Texts.Add("「姉は家族の松家に帰った」");
        SetScenario(scenario);

    }

    void Update()
    {
        if (currentScenario != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (buttonPanel.GetComponentsInChildren<Button>().Length < 1)
                {
                    SetNextMessage();
                }
            }
        }
    }
    class Scenario
    {
        public string ScenarioID;
        public List<string> Texts = new List<string>();
        public List<Option> Options = new List<Option>();

        public string NextScenarioID;

    }

    class Option
    {
        public string Text;
        public Action Action;
    }

    Scenario currentScenario;
    List<Scenario> scenarios = new List<Scenario>();
}
