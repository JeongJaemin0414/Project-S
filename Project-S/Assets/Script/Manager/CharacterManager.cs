using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterData
{
    public string Name;
    public string illustFileName;
}

public class CharacterManager : Singleton<CharacterManager>
{
    private List<CharacterData> characterData = new();

    private void Awake()
    {
        InitCharacterData();
    }

    private void InitCharacterData()
    {
        List<CharacterTableEntity> _characterTableEntities = ExcelManager.Instance.GetExcelData<CharacterTable>().character;

        foreach (CharacterTableEntity _characterTableEntity in _characterTableEntities)
        {
            CharacterData _characterData = new()
            {
                Name = LanguageManager.Instance.GetString(_characterTableEntity.charName),
                illustFileName = _characterTableEntity.illustFileName
            };

            characterData.Add(_characterData);
        }
    }

    public CharacterData GetCharacterData(int index)
    {
        return characterData[index];
    }

    public string GetCharacterName(int index)
    {
        return characterData[index].Name;
    }

    public string GetCharacterIllustFileName(int index)
    {
        return characterData[index].illustFileName;
    }
}
