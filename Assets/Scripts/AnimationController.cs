using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject pressAnyKey;
    [SerializeField] private GameObject sidePanel;
    [SerializeField] private GameObject tooltip;
    [SerializeField] private GameObject fadeIn;

    private readonly int continueTrigger = Animator.StringToHash("continue");
    private readonly int showTrigger = Animator.StringToHash("show");
    private readonly int hideTrigger = Animator.StringToHash("hide");

    private readonly List<Animator> splashScreenAnimators = new List<Animator>();
    private Animator sidePanelAnimator;
    private Animator tooltipAnimator;

    private bool sidePanelHidden = true;
    private bool splashScreen = true;
    private bool showTooltip;

    private void Start()
    {
        GetAnimators();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !splashScreen) ToggleSidePanel();
        if (Input.anyKeyDown && splashScreen) Continue();
    }

    private void GetAnimators()
    {
        splashScreenAnimators.Add(mainCamera.GetComponent<Animator>());
        splashScreenAnimators.Add(title.GetComponent<Animator>());
        splashScreenAnimators.Add(pressAnyKey.GetComponent<Animator>());
        sidePanelAnimator = sidePanel.GetComponent<Animator>();
        tooltipAnimator = tooltip.GetComponent<Animator>();
    }

    private void Continue()
    {
        showTooltip = true;

        foreach (var animator in splashScreenAnimators)
        {
            animator.SetTrigger(continueTrigger);
        }

        ToggleSidePanel();
        splashScreen = false;
        fadeIn.SetActive(false);
    }

    private void ToggleSidePanel()
    {
        sidePanelAnimator.SetTrigger(sidePanelHidden ? showTrigger : hideTrigger);
        tooltipAnimator.SetTrigger(showTooltip ? showTrigger : hideTrigger);
        sidePanelHidden = !sidePanelHidden;
        showTooltip = false;
    }
}