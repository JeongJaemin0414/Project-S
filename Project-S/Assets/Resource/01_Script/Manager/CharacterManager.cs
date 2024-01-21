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
    private Dictionary<int, CharacterInfoData> characterinfoData = new();

    public override void Init()
    {
        InitCharacterData();
    }

    private void InitCharacterData()
    {
        List<NpcTableEntity> _characterTableEntities = ExcelManager.Instance.GetExcelData<NpcTable>().npc;

        foreach (NpcTableEntity _characterTableEntity in _characterTableEntities)
        {
            CharacterInfoData _characterInfoData = new()
            {
                Name = LanguageManager.Instance.GetString(_characterTableEntity.charName),
                illustFileName = _characterTableEntity.illustFileName
            };

            characterinfoData.Add(_characterTableEntity.index, _characterInfoData);
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
