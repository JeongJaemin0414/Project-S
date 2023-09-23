using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterInfoData
{
    public string Name;
    public string illustFileName;
}

public class CharacterManager : Singleton<CharacterManager>
{
    private List<CharacterInfoData> characterinfoData = new();

    protected override void Awake()
    {
        InitCharacterData();
    }

    private void InitCharacterData()
    {
        List<CharacterTableEntity> _characterTableEntities = ExcelManager.Instance.GetExcelData<CharacterTable>().character;

        foreach (CharacterTableEntity _characterTableEntity in _characterTableEntities)
        {
            CharacterInfoData _characterInfoData = new()
            {
                Name = LanguageManager.Instance.GetString(_characterTableEntity.charName),
                illustFileName = _characterTableEntity.illustFileName
            };

            characterinfoData.Add(_characterInfoData);
        }
    }

    public CharacterInfoData GetCharacterInfoData(int index)
    {
        return characterinfoData[index];
    }

    public string GetCharacterName(int index)
    {
        return characterinfoData[index].Name;
    }

    public string GetCharacterIllustFileName(int index)
    {
        return characterinfoData[index].illustFileName;
    }
}
