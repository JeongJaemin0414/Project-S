using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum IllustLocation
{
    Left,
    Right
}

public enum IllustAppear
{
    FadeIn,
    FadeOut,
    Stay,
}

[System.Serializable]
public struct DialogData
{
    public int charIndex;
    public int dec;
    public IllustLocation illustLocation;
    public IllustAppear illustAppear;
}

[System.Serializable]
public struct DialogUI
{
    public List<Image> images;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDialogue;
    public GameObject objectArrow;
}

public class DialogSystem : MonoBehaviour
{
    public DialogUI dialogUI;
    public List<DialogData> dialogData;

    private int currentDialogIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateDioalog();
        }
    }

    public void SetDialogData(int groupIndex)
    {
        List<DialogTableEntity> _dialogTableEntities = ExcelManager.Instance.GetExcelData<DialogTable>().dialog.FindAll(x => x.group == groupIndex);
     
        foreach(DialogTableEntity _entitie in _dialogTableEntities)
        {
            DialogData _newDialogData = new()
            {
                charIndex = _entitie.charIndex,
                dec = _entitie.dec,
                illustLocation = (IllustLocation)_entitie.illustLocation,
                illustAppear = (IllustAppear)_entitie.illustAppear
            };

            dialogData.Add(_newDialogData);
        }
    }

    public void UpdateDioalog()
    {
        if(dialogData == null)
        {
            Debug.LogError("DialogData is NULL");
            return;
        }
        else
        {
            if (dialogData.Count > currentDialogIndex) //대사가 남아있을 경우
            {
                CharacterData _characterData = CharacterManager.Instance.GetCharacterData(dialogData[currentDialogIndex].charIndex);
                int _illustIndex = (int)dialogData[currentDialogIndex].illustLocation;
                string _characterName = _characterData.Name;
                string _characterDialog = LanguageManager.Instance.GetString(dialogData[currentDialogIndex].dec);
                Sprite _characterImage = Resources.Load<Sprite>("Char/" + _characterData.illustFileName);

                dialogUI.images[_illustIndex].sprite = _characterImage;
                dialogUI.textName.text = _characterName;
                dialogUI.textDialogue.text = _characterDialog;

                switch (dialogData[currentDialogIndex].illustAppear)
                {
                    case IllustAppear.FadeIn:
                        DOTweenManager.Instance.FadeIn(dialogUI.images[_illustIndex]);
                        break;
                    case IllustAppear.FadeOut:
                        DOTweenManager.Instance.FadeOut(dialogUI.images[_illustIndex]);
                        break;
                }

                currentDialogIndex++;
            }
            else
            {
                currentDialogIndex = 0;
                dialogData.Clear();

                CloseDialogPanel();
            }
        }
    }

    public void OpenDialogPanel()
    {
        if (gameObject.activeSelf) return;

        gameObject.SetActive( true );
    }

    public void CloseDialogPanel()
    {
        if (!gameObject.activeSelf) return;

        gameObject.SetActive( false );
    }
}
