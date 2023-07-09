namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<CharacterResponse>>> GetAllCharacters();
        Task<ServiceResponse<CharacterResponse>> GetCharacterById(int id);
        Task<ServiceResponse<List<CharacterResponse>>> AddCharacter(CharacterRequest newCharacter);
        Task<ServiceResponse<CharacterResponse>> UpdateCharacter(UpdateCharacterRequest updateCharacter);
        Task<ServiceResponse<List<CharacterResponse>>> DeleteCharacter(int id);
    }
}