using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

public class DialogSystem : UISystemBase
{
    public DialogUI dialogUI;
    private List<DialogData> dialogData = new();

    private int currentDialogIndex = 0;
    private bool isTypingEffect = false;
    private float typingSpeed = 0.1f;

    [Header("이미지 연출 수치")]
    public float ImageMoveValue = 100f;
    public float ImageMoveDuration = 1f;

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    UpdateDioalog();
        //}
    }

    public override void Init()
    {

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
            if (isTypingEffect)
            {
                isTypingEffect = false;

                StopCoroutine("OnTypingText");

                LanguageManager.Instance.SetText(dialogUI.textDialogue, dialogData[currentDialogIndex].dec);
                dialogUI.objectArrow.SetActive(true);

                currentDialogIndex++;
                return;
            }
            else
            {
                if (dialogData.Count > currentDialogIndex) 
                {
                    CharacterInfoData _characterData = CharacterManager.Instance.GetCharacterInfoData(dialogData[currentDialogIndex].charIndex);
                    int _illustIndex = (int)dialogData[currentDialogIndex].illustLocation;
                    string _characterName = _characterData.Name;

                    dialogUI.textName.text = _characterName;
                    dialogUI.objectArrow.SetActive(false);
                    AddressbleManager.Instance.SetSprite(dialogUI.images[_illustIndex], "Char/" + _characterData.illustFileName);
                    StartCoroutine("OnTypingText");

                    switch (dialogData[currentDialogIndex].illustAppear)
                    {
                        case IllustAppear.FadeIn:
                            DOTweenManager.Instance.FadeIn(dialogUI.images[_illustIndex]);
                            break;
                        case IllustAppear.FadeOut:
                            DOTweenManager.Instance.FadeOut(dialogUI.images[_illustIndex]);            
                            break;
                    }
                }
                else
                {
                    currentDialogIndex = 0;
                    dialogData.Clear();

                    CloseUISystem();
                }
            }
        }
    }

    private IEnumerator OnTypingText()
    {
        int _index = 0;

        isTypingEffect = true;
        string _dialog = LanguageManager.Instance.GetString(dialogData[currentDialogIndex].dec);

        while (_index <= _dialog.Length)
        {
            dialogUI.textDialogue.text = _dialog.Substring(0, _index);
            _index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        currentDialogIndex++;
        isTypingEffect = false;
        dialogUI.objectArrow.SetActive(true);
    }
}
