using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
public class ConvertLanguage : MonoBehaviour
{
    public void Convert(int language)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[language];
        DataGame.Instance.dataSave.Language[0] = language;
        UIHomeController.Instance.ChooseLanguage.SetActive(false);
        UIHomeController.Instance.FillChoseLanguage.SetActive(false);
        UIHomeController.Instance.IsCheck = true;
        DataGame.Instance.SaveData();
    }

    private void Start()
    {
        StartCoroutine(SetLocates());
    }


    private IEnumerator SetLocates()
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[DataGame.Instance.dataSave.Language[0]];
    }
}
