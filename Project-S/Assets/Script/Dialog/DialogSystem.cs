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
    private bool isTypingEffect = false;
    private float typingSpeed = 0.1f;

    [Header("이미지 연출 값 수치")]
    public float ImageMoveValue = 100f;
    public float ImageMoveDuration = 1f;

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
            if (isTypingEffect)
            {
                isTypingEffect = false;

                string _dialog = LanguageManager.Instance.GetString(dialogData[currentDialogIndex].dec);

                StopCoroutine("OnTypingText");
                dialogUI.textDialogue.text = _dialog;
                dialogUI.objectArrow.gameObject.SetActive(true);

                currentDialogIndex++;
                return;
            }
            else
            {
                if (dialogData.Count > currentDialogIndex) //대사가 남아있을 경우
                {
                    CharacterData _characterData = CharacterManager.Instance.GetCharacterData(dialogData[currentDialogIndex].charIndex);
                    int _illustIndex = (int)dialogData[currentDialogIndex].illustLocation;
                    string _characterName = _characterData.Name;
                    Sprite _characterImage = Resources.Load<Sprite>("Char/" + _characterData.illustFileName);
                    float _imageMoveValue;

                    dialogUI.images[_illustIndex].sprite = _characterImage;
                    dialogUI.textName.text = _characterName;
                    dialogUI.objectArrow.gameObject.SetActive(false);
                    StartCoroutine("OnTypingText");
                    //dialogUI.textDialogue.text = _dialog;

                    switch (dialogData[currentDialogIndex].illustAppear)
                    {
                        case IllustAppear.FadeIn:
                            _imageMoveValue = (dialogData[currentDialogIndex].illustLocation == IllustLocation.Left) ? ImageMoveValue : -ImageMoveValue;

                            DOTweenManager.Instance.FadeIn(dialogUI.images[_illustIndex]);
                            DOTweenManager.Instance.MoveRectTransformX(dialogUI.images[_illustIndex].rectTransform, _imageMoveValue, 1f);
                            break;
                        case IllustAppear.FadeOut:
                            _imageMoveValue = (dialogData[currentDialogIndex].illustLocation == IllustLocation.Left) ? -ImageMoveValue : ImageMoveValue;

                            DOTweenManager.Instance.FadeOut(dialogUI.images[_illustIndex]);            
                            DOTweenManager.Instance.MoveRectTransformX(dialogUI.images[_illustIndex].rectTransform, _imageMoveValue, 1f);
                            break;
                    }
                }
                else
                {
                    currentDialogIndex = 0;
                    dialogData.Clear();

                    CloseDialogPanel();
                }
            }
        }
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;

        isTypingEffect = true;
        string _dialog = LanguageManager.Instance.GetString(dialogData[currentDialogIndex].dec);

        while (index <= _dialog.Length)
        {
            dialogUI.textDialogue.text = _dialog.Substring(0, index);
            index++;

            yield return new WaitForSeconds(typingSpeed);
        }

        currentDialogIndex++;
        isTypingEffect = false;
        dialogUI.objectArrow.SetActive(true);
    }


    public void OpenDialogPanel()
    {
        if (gameObject.activeSelf) return;

        GameManager.Instance.SetPlayerMoveStop(true);
        gameObject.SetActive( true );
    }

    public void CloseDialogPanel()
    {
        if (!gameObject.activeSelf) return;

        GameManager.Instance.SetPlayerMoveStop(false);
        gameObject.SetActive( false );
    }

    public bool IsDialoging()
    {
        return (dialogData.Count != 0);
    }
}
