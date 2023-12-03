using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageManager : Singleton<LanguageManager>
{
    private LauguageType languageId = LauguageType.kor;

    public override void Init()
    {

    }

    public void SetLanguage(LauguageType lauguageType)
    {
        languageId = lauguageType;
    }

    public LauguageType GetLanugage()
    {
        return languageId;
    }

    public void SetText(TextMeshProUGUI textObj, int stringIndex, string[] values = null)
    {
        string str;

        if (values != null)
        {
            object[] args = Utilities.GetStringFormatData(values);
            str = string.Format(GetString(stringIndex), args);
        }
        else
            str = GetString(stringIndex);

        textObj.text = str;
    }

    public string GetString(int stringIndex)
    {
        string _lan = string.Empty;
        LanguageTableEntity languageTableEntity = ExcelManager.Instance.GetExcelData<LanguageTable>().language.Find(x => x.index == stringIndex);

        switch (languageId)
        {
            case LauguageType.kor:
                _lan = languageTableEntity.korLanguage;
                break;
            case LauguageType.eng:
                _lan = languageTableEntity.engLanguage;
                break;
        }

        return _lan;
    }

}
