namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> characters = new List<Character>();

    public async Task<ServiceResponse<List<CharacterResponse>>> AddCharacter(CharacterRequest newCharacter)
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      
      characters.Add(newCharacter);
      servicerResponse.Data = characters;

      return servicerResponse;
    }

    public async Task<ServiceResponse<List<CharacterResponse>>> GetAllCharacters()
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      servicerResponse.Data = characters;

      return servicerResponse;
    }

    public async Task<ServiceResponse<CharacterResponse>> GetCharacterById(int id)
    {
      var servicerResponse = new ServiceResponse<CharacterResponse>();

      var character = characters.FirstOrDefault(c => c.Id == id);
      servicerResponse.Data = character;
      return servicerResponse;
    }
  }
}