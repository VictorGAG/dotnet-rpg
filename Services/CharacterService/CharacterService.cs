namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> characters = new List<Character>();

    public async Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter)
    {
      var servicerResponse = new ServiceResponse<List<Character>>();
      
      characters.Add(newCharacter);
      servicerResponse.Data = characters;

      return servicerResponse;
    }

    public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
    {
      var servicerResponse = new ServiceResponse<List<Character>>();
      servicerResponse.Data = characters;

      return servicerResponse;
    }

    public async Task<ServiceResponse<Character>> GetCharacterById(int id)
    {
      var servicerResponse = new ServiceResponse<Character>();

      var character = characters.FirstOrDefault(c => c.Id == id);
      servicerResponse.Data = character;

      return servicerResponse;
    }
  }
}