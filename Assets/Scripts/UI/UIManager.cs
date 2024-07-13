using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using System;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{

    [Header("Menu UI")]
    [SerializeField]
    private Button Menu_Button;
    [SerializeField]
    private GameObject Menu_Object;
    [SerializeField]
    private RectTransform Menu_RT;

    [SerializeField]
    private Button About_Button;
    [SerializeField]
    private GameObject About_Object;
    [SerializeField]
    private RectTransform About_RT;

    [Header("Settings UI")]
    [SerializeField]
    private Button Settings_Button;
    [SerializeField]
    private GameObject Settings_Object;
    [SerializeField]
    private RectTransform Settings_RT;
    [SerializeField]
    private Button Terms_Button;
    [SerializeField]
    private Button Privacy_Button;

    [SerializeField]
    private Button Exit_Button;
    [SerializeField]
    private GameObject Exit_Object;
    [SerializeField]
    private RectTransform Exit_RT;

    [SerializeField]
    private Button Paytable_Button;
    [SerializeField]
    private GameObject Paytable_Object;
    [SerializeField]
    private RectTransform Paytable_RT;

    [Header("Popus UI")]
    [SerializeField]
    private GameObject MainPopup_Object;

    [Header("About Popup")]
    [SerializeField]
    private GameObject AboutPopup_Object;
    [SerializeField]
    private Button AboutExit_Button;

    [Header("Paytable Popup")]
    [SerializeField]
    private GameObject PaytablePopup_Object;
    [SerializeField]
    private Button PaytableExit_Button;
    [SerializeField]
    private TMP_Text[] SymbolsText;
    [SerializeField] private TMP_Text FreeSpin_Text;
    [SerializeField] private TMP_Text Scatter_Text;
    [SerializeField] private TMP_Text Jackpot_Text;


    [Header("Info Popup")]
    [SerializeField]
    private Button Next_Button;
    [SerializeField]
    private Button Previous_Button;
    private int paginationCounter = 0;
    [SerializeField] private GameObject[] PageList;
    [SerializeField] private Button[] paginationButtonGrp;

    [Header("Settings Popup")]
    [SerializeField]
    private GameObject SettingsPopup_Object;
    [SerializeField]
    private Button SettingsExit_Button;
    [SerializeField]
    private Button Sound_Button;
    [SerializeField]
    private Button Music_Button;

    [SerializeField]
    private GameObject MusicOn_Object;
    [SerializeField]
    private GameObject MusicOff_Object;
    [SerializeField]
    private GameObject SoundOn_Object;
    [SerializeField]
    private GameObject SoundOff_Object;

    [Header("Win Popups")]
    [SerializeField] private Sprite BigWin_Sprite;
    [SerializeField] private Sprite MegaWin_Sprite;
    [SerializeField] private Sprite HugeWin_Sprite;
    [SerializeField] private Image Win_Image;
    [SerializeField] private GameObject WinPopup_Object;
    [SerializeField] private GameObject jackpot_Object;
    [SerializeField] private TMP_Text Win_Text;
    [SerializeField] private TMP_Text jackpot_Text;


    [SerializeField]
    private AudioController audioController;



    [Header("scripts")]
    [SerializeField] private SlotBehaviour slotManager;
    [SerializeField] private SocketIOManager socketManager;

    private bool isMusic = true;
    private bool isSound = true;


    [Header("Free Spins")]
    [SerializeField] private Image freeSpinBar;
    [SerializeField] private RectTransform freeSpinBarHandle;
    [SerializeField] private TMP_Text freeSpinCount;

    [Header("Splash Screen")]
    [SerializeField] private GameObject spalsh_screen;
    [SerializeField] private Image progressbar;
    [SerializeField] private RectTransform progressbarHandle;
    [SerializeField] private TMP_Text loadingText;


    [Header("Quit Popup")]
    [SerializeField] private GameObject QuitPopupObject;
    [SerializeField] private Button exit_Button;
    [SerializeField] private Button GameExit_Button;
    [SerializeField] private Button no_Button;
    [SerializeField] private Button cancel_Button;

    [Header("disconnection popup")]
    [SerializeField] private Button CloseDisconnect_Button;
    [SerializeField] private GameObject DisconnectPopup_Object;

    [Header("low balance popup")]
    [SerializeField] private Button Close_Button;
    [SerializeField] private GameObject LowBalancePopup_Object;

    private bool isExit = false;


    private void Awake()
    {
        if (spalsh_screen) spalsh_screen.SetActive(true);
        StartCoroutine(LoadingRoutine());
    }

    private void Start()
    {

        if (Menu_Button) Menu_Button.onClick.RemoveAllListeners();
        if (Menu_Button) Menu_Button.onClick.AddListener(OpenMenu);

        if (Exit_Button) Exit_Button.onClick.RemoveAllListeners();
        if (Exit_Button) Exit_Button.onClick.AddListener(CloseMenu);

        if (About_Button) About_Button.onClick.RemoveAllListeners();
        if (About_Button) About_Button.onClick.AddListener(delegate { OpenPopup(AboutPopup_Object); });

        if (AboutExit_Button) AboutExit_Button.onClick.RemoveAllListeners();
        if (AboutExit_Button) AboutExit_Button.onClick.AddListener(delegate { ClosePopup(AboutPopup_Object); });

        if (Next_Button) Next_Button.onClick.RemoveAllListeners();
        if (Next_Button) Next_Button.onClick.AddListener(delegate { TurnPage(true); });

        if (Previous_Button) Previous_Button.onClick.RemoveAllListeners();
        if (Previous_Button) Previous_Button.onClick.AddListener(delegate { TurnPage(false); });

        if (Previous_Button) Previous_Button.interactable = false;

        if (PaytablePopup_Object) PageList[0].SetActive(true);

        if (paginationButtonGrp[0]) paginationButtonGrp[0].onClick.RemoveAllListeners();
        if (paginationButtonGrp[0]) paginationButtonGrp[0].onClick.AddListener(delegate { GoToPage(0); });

        if (paginationButtonGrp[1]) paginationButtonGrp[1].onClick.RemoveAllListeners();
        if (paginationButtonGrp[1]) paginationButtonGrp[1].onClick.AddListener(delegate { GoToPage(1); });

        if (paginationButtonGrp[2]) paginationButtonGrp[2].onClick.RemoveAllListeners();
        if (paginationButtonGrp[2]) paginationButtonGrp[2].onClick.AddListener(delegate { GoToPage(2); });

        if (paginationButtonGrp[3]) paginationButtonGrp[3].onClick.RemoveAllListeners();
        if (paginationButtonGrp[3]) paginationButtonGrp[3].onClick.AddListener(delegate { GoToPage(3); });

        if (paginationButtonGrp[4]) paginationButtonGrp[4].onClick.RemoveAllListeners();
        if (paginationButtonGrp[4]) paginationButtonGrp[4].onClick.AddListener(delegate { GoToPage(4); });

        if (Paytable_Button) Paytable_Button.onClick.RemoveAllListeners();
        if (Paytable_Button) Paytable_Button.onClick.AddListener(delegate { OpenPopup(PaytablePopup_Object); });

        if (PaytableExit_Button) PaytableExit_Button.onClick.RemoveAllListeners();
        if (PaytableExit_Button) PaytableExit_Button.onClick.AddListener(delegate { ClosePopup(PaytablePopup_Object); });

        if (Settings_Button) Settings_Button.onClick.RemoveAllListeners();
        if (Settings_Button) Settings_Button.onClick.AddListener(delegate { OpenPopup(SettingsPopup_Object); });

        if (SettingsExit_Button) SettingsExit_Button.onClick.RemoveAllListeners();
        if (SettingsExit_Button) SettingsExit_Button.onClick.AddListener(delegate { ClosePopup(SettingsPopup_Object); });

        if (MusicOn_Object) MusicOn_Object.SetActive(true);
        if (MusicOff_Object) MusicOff_Object.SetActive(false);

        if (SoundOn_Object) SoundOn_Object.SetActive(true);
        if (SoundOff_Object) SoundOff_Object.SetActive(false);


        if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.RemoveAllListeners();
        if (CloseDisconnect_Button) CloseDisconnect_Button.onClick.AddListener(CallOnExitFunction);

        if (exit_Button) exit_Button.onClick.RemoveAllListeners();
        if (exit_Button) exit_Button.onClick.AddListener(delegate { OpenPopup(QuitPopupObject); });

        if (GameExit_Button) GameExit_Button.onClick.RemoveAllListeners();
        if (GameExit_Button) GameExit_Button.onClick.AddListener(CallOnExitFunction);

        if (no_Button) no_Button.onClick.RemoveAllListeners();
        if (no_Button) no_Button.onClick.AddListener(delegate { ClosePopup(QuitPopupObject); });

        if (cancel_Button) cancel_Button.onClick.RemoveAllListeners();
        if (cancel_Button) cancel_Button.onClick.AddListener(delegate { ClosePopup(QuitPopupObject); });

        if (Close_Button) Close_Button.onClick.AddListener(delegate { ClosePopup(LowBalancePopup_Object); });

        if (audioController) audioController.ToggleMute(false);

        isMusic = true;
        isSound = true;

        if (Sound_Button) Sound_Button.onClick.RemoveAllListeners();
        if (Sound_Button) Sound_Button.onClick.AddListener(ToggleSound);

        if (Music_Button) Music_Button.onClick.RemoveAllListeners();
        if (Music_Button) Music_Button.onClick.AddListener(ToggleMusic);

    }


    private IEnumerator LoadingRoutine()
    {
        StartCoroutine(LoadingTextAnimate());
        float fillAmount = 0.7f;
        progressbar.DOFillAmount(fillAmount, 2f).SetEase(Ease.Linear);
        progressbarHandle.DOAnchorPosX(20 + (fillAmount * (510 - 20)), 2f, true).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(2f);
        yield return new WaitUntil(() => !socketManager.isLoading);
        progressbar.DOFillAmount(1, 1f).SetEase(Ease.Linear);
        progressbarHandle.DOAnchorPosX(510, 1f, true).SetEase(Ease.Linear);
        yield return new WaitForSecondsRealtime(1f);
        if (spalsh_screen) spalsh_screen.SetActive(false);
        StopCoroutine(LoadingTextAnimate());
        audioController.playBgAudio();
    }

    private IEnumerator LoadingTextAnimate()
    {
        while (true)
        {
            if (loadingText) loadingText.text = "Loading.";
            yield return new WaitForSeconds(0.5f);
            if (loadingText) loadingText.text = "Loading..";
            yield return new WaitForSeconds(0.5f);
            if (loadingText) loadingText.text = "Loading...";
            yield return new WaitForSeconds(0.5f);
        }
    }


    internal void PopulateWin(int value, double amount)
    {
        switch (value)
        {
            case 1:
                if (Win_Image) Win_Image.sprite = BigWin_Sprite;
                break;
            case 2:
                if (Win_Image) Win_Image.sprite = HugeWin_Sprite;
                break;
            case 3:
                if (Win_Image) Win_Image.sprite = MegaWin_Sprite;
                break;

        }
        if (value == 4)
            StartPopupAnim(amount, true);
        else
            StartPopupAnim(amount, false);

    }



    internal void updateFreeSPinData(float fillAmount, int count)
    {
        if (fillAmount < 0)
            fillAmount = 0;
        if (fillAmount > 1)
            fillAmount = 1;

        freeSpinBar.DOFillAmount(fillAmount, 0.5f).SetEase(Ease.Linear);
        freeSpinBarHandle.DOAnchorPosX(20 + (fillAmount * (510 - 20)), 0.5f).SetEase(Ease.Linear);

        freeSpinCount.text = count.ToString();
    }

    internal void LowBalPopup() {

        OpenPopup(LowBalancePopup_Object);
    }

    internal void setFreeSpinData(int count)
    {

        freeSpinBar.DOFillAmount(1, 0.2f).SetEase(Ease.Linear);
        freeSpinBarHandle.DOAnchorPosX(510, 0.2f).SetEase(Ease.Linear);
        freeSpinCount.text = count.ToString();
    }

    private void StartPopupAnim(double amount, bool jackpot = false)
    {
        int initAmount = 0;
        if (jackpot)
        {
            if (jackpot_Object) jackpot_Object.SetActive(true);
        }
        else
        {
            if (WinPopup_Object) WinPopup_Object.SetActive(true);

        }

        if (MainPopup_Object) MainPopup_Object.SetActive(true);

        DOTween.To(() => initAmount, (val) => initAmount = val, (int)amount, 5f).OnUpdate(() =>
        {
            if (jackpot)
            {
                if (jackpot_Text) jackpot_Text.text = initAmount.ToString();

            }
            else
            {

                if (Win_Text) Win_Text.text = initAmount.ToString();

            }
        });

        DOVirtual.DelayedCall(6f, () =>
        {
            if (jackpot)
            {
                if (jackpot_Object) jackpot_Object.SetActive(true);

            }
            else
            {
                if (WinPopup_Object) WinPopup_Object.SetActive(false);

            }
            if (MainPopup_Object) MainPopup_Object.SetActive(false);
            slotManager.CheckPopups = false;
        });
    }

    internal void InitialiseUIData(string SupportUrl, string AbtImgUrl, string TermsUrl, string PrivacyUrl, Paylines symbolsText, List<string> Specialsymbols)
    {
        if (Terms_Button) Terms_Button.onClick.RemoveAllListeners();
        if (Terms_Button) Terms_Button.onClick.AddListener(delegate { UrlButtons(TermsUrl); });

        if (Privacy_Button) Privacy_Button.onClick.RemoveAllListeners();
        if (Privacy_Button) Privacy_Button.onClick.AddListener(delegate { UrlButtons(PrivacyUrl); });

        PopulateSymbolsPayout(symbolsText);
        //PopulateSpecialSymbols(Specialsymbols);
    }

    //private void PopulateSpecialSymbols(List<string> Specialtext)
    //{
    //    for (int i = 0; i < SpecialSymbolsText.Length; i++)
    //    {
    //        if (SpecialSymbolsText[i]) SpecialSymbolsText[i].text = Specialtext[i];
    //    }
    //}

    private void PopulateSymbolsPayout(Paylines paylines)
    {
        for (int i = 0; i < SymbolsText.Length; i++)
        {
            string text = null;
            if (paylines.symbols[i].Multiplier[0][0] != 0)
            {
                text += "5x - " + paylines.symbols[i].Multiplier[0][0];
            }
            if (paylines.symbols[i].Multiplier[1][0] != 0)
            {
                text += "\n4x - " + paylines.symbols[i].Multiplier[1][0];
            }
            if (paylines.symbols[i].Multiplier[2][0] != 0)
            {
                text += "\n3x - " + paylines.symbols[i].Multiplier[2][0];
            }
            if (SymbolsText[i]) SymbolsText[i].text = text;
        }

        for (int i = 0; i < paylines.symbols.Count; i++)
        {
            if (paylines.symbols[i].Name.ToUpper() == "FREESPIN")
            {
                if (FreeSpin_Text) FreeSpin_Text.text = "Free Spin: Activates " + paylines.symbols[i].Multiplier[0][1] + ", " + paylines.symbols[i].Multiplier[1][1] + ", or " + paylines.symbols[i].Multiplier[2][1] + " free spins when 3, 4, or 5 symbols appear on pay lines.";
            }
            if (paylines.symbols[i].Name.ToUpper() == "SCATTER")
            {
                if (Scatter_Text) Scatter_Text.text = "Scatter: Offers higher pay outs and awards <color=yellow>" + paylines.symbols[i].Multiplier[0][1] + "</color> free spins if 5 symbols align on the pay line with a multiplier.\nPayout: 5x - " + paylines.symbols[i].Multiplier[0][0] + ", 4x - " + paylines.symbols[i].Multiplier[1][0] + ", 3x - " + paylines.symbols[i].Multiplier[2][0];
            }
            if (paylines.symbols[i].Name.ToUpper() == "JACKPOT")
            {
                if (Jackpot_Text) Jackpot_Text.text = "Jackpot: Mega win triggered by 5 Jackpot symbols on a pay line.\nPayout: <color=yellow>" + paylines.symbols[i].defaultAmount;
            }

        }
    }

    private void CallOnExitFunction()
    {
        isExit = true;
        audioController.PlayButtonAudio();
        socketManager.CloseSocket();
        //slotManager.CallCloseSocket();
        Application.ExternalCall("window.parent.postMessage", "onExit", "*");
    }

    private void OpenMenu()
    {
        audioController.PlayButtonAudio();
        if (Menu_Object) Menu_Object.SetActive(false);
        if (Exit_Object) Exit_Object.SetActive(true);
        if (About_Object) About_Object.SetActive(true);
        if (Paytable_Object) Paytable_Object.SetActive(true);
        if (Settings_Object) Settings_Object.SetActive(true);

        DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y + 150), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
        });

        DOTween.To(() => Paytable_RT.anchoredPosition, (val) => Paytable_RT.anchoredPosition = val, new Vector2(Paytable_RT.anchoredPosition.x, Paytable_RT.anchoredPosition.y + 300), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Paytable_RT);
        });

        DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y + 450), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
        });
    }

    private void CloseMenu()
    {

        DOTween.To(() => About_RT.anchoredPosition, (val) => About_RT.anchoredPosition = val, new Vector2(About_RT.anchoredPosition.x, About_RT.anchoredPosition.y - 150), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(About_RT);
        });

        DOTween.To(() => Paytable_RT.anchoredPosition, (val) => Paytable_RT.anchoredPosition = val, new Vector2(Paytable_RT.anchoredPosition.x, Paytable_RT.anchoredPosition.y - 300), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Paytable_RT);
        });

        DOTween.To(() => Settings_RT.anchoredPosition, (val) => Settings_RT.anchoredPosition = val, new Vector2(Settings_RT.anchoredPosition.x, Settings_RT.anchoredPosition.y - 450), 0.1f).OnUpdate(() =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(Settings_RT);
        });

        DOVirtual.DelayedCall(0.1f, () =>
        {
            if (Menu_Object) Menu_Object.SetActive(true);
            if (Exit_Object) Exit_Object.SetActive(false);
            if (About_Object) About_Object.SetActive(false);
            if (Paytable_Object) Paytable_Object.SetActive(false);
            if (Settings_Object) Settings_Object.SetActive(false);
        });
    }

    private void OpenPopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(true);
        if (MainPopup_Object) MainPopup_Object.SetActive(true);
    }

    private void ClosePopup(GameObject Popup)
    {
        if (audioController) audioController.PlayButtonAudio();
        if (Popup) Popup.SetActive(false);
        if (MainPopup_Object) MainPopup_Object.SetActive(false);
    }

    private void TurnPage(bool type)
    {
        if (audioController) audioController.PlayButtonAudio();

        if (type)
            paginationCounter++;
        else
            paginationCounter--;


        GoToPage(paginationCounter - 1);


    }

    private void GoToPage(int index)
    {

        paginationCounter = index + 1;

        paginationCounter = Mathf.Clamp(paginationCounter, 1, 5);

        if (Next_Button) Next_Button.interactable = !(paginationCounter >= 5);

        if (Previous_Button) Previous_Button.interactable = !(paginationCounter <= 1);

        for (int i = 0; i < PageList.Length; i++)
        {
            PageList[i].SetActive(false);
        }

        for (int i = 0; i < paginationButtonGrp.Length; i++)
        {
            paginationButtonGrp[i].interactable = true;
            paginationButtonGrp[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        PageList[paginationCounter - 1].SetActive(true);
        paginationButtonGrp[paginationCounter - 1].interactable = false;
        paginationButtonGrp[paginationCounter - 1].transform.GetChild(0).gameObject.SetActive(true);
    }

    private void ToggleMusic()
    {
        isMusic = !isMusic;
        if (isMusic)
        {
            if (MusicOn_Object) MusicOn_Object.SetActive(true);
            if (MusicOff_Object) MusicOff_Object.SetActive(false);
            audioController.ToggleMute(false, "bg");
        }
        else
        {
            if (MusicOn_Object) MusicOn_Object.SetActive(false);
            if (MusicOff_Object) MusicOff_Object.SetActive(true);
            audioController.ToggleMute(true, "bg");
        }
    }

    private void UrlButtons(string url)
    {
        Application.OpenURL(url);
    }

    internal void DisconnectionPopup()
    {

        //ClosePopup(ReconnectPopup_Object);
        if (!isExit)
        {
            OpenPopup(DisconnectPopup_Object);
        }

    }

    private void ToggleSound()
    {
        isSound = !isSound;
        if (isSound)
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(true);
            if (SoundOff_Object) SoundOff_Object.SetActive(false);
            if (audioController) audioController.ToggleMute(false, "button");
            if (audioController) audioController.ToggleMute(false, "wl");
        }
        else
        {
            if (SoundOn_Object) SoundOn_Object.SetActive(false);
            if (SoundOff_Object) SoundOff_Object.SetActive(true);
            if (audioController) audioController.ToggleMute(true, "button");
            if (audioController) audioController.ToggleMute(true, "wl");
        }
    }

}
