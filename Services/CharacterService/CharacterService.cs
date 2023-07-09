namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> characters = new List<Character>();
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<CharacterResponse>>> AddCharacter(CharacterRequest newCharacter)
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      var character = _mapper.Map<Character>(newCharacter);
      
      if (characters.Count > 0)
        character.Id = characters.Max(c => c.Id) + 1;

      characters.Add(character);
      servicerResponse.Data = characters.Select(c => _mapper.Map<CharacterResponse>(c)).ToList();

      return servicerResponse;
    }

    public async Task<ServiceResponse<List<CharacterResponse>>> DeleteCharacter(int id)
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      try {
        var character = characters.First(c => c.Id == id);
        if (character == null)
          throw new Exception($"Character with Id '{id}' not found.");

        characters.Remove(character);
        servicerResponse.Data = characters.Select(c => _mapper.Map<CharacterResponse>(c)).ToList();
      }
      catch (Exception ex)
      {
        servicerResponse.Success = false;
        servicerResponse.Message = ex.Message;
      }
      return servicerResponse;
    }

    public async Task<ServiceResponse<List<CharacterResponse>>> GetAllCharacters()
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      servicerResponse.Data = characters.Select(c => _mapper.Map<CharacterResponse>(c)).ToList();

      return servicerResponse;
    }

    public async Task<ServiceResponse<CharacterResponse>> GetCharacterById(int id)
    {
      var servicerResponse = new ServiceResponse<CharacterResponse>();
      try {
        var character = characters.FirstOrDefault(c => c.Id == id);
        if (character == null)
          throw new Exception($"Character with Id '{id}' not found.");
          
        servicerResponse.Data = _mapper.Map<CharacterResponse>(character);
      }
      catch (Exception ex) {
        servicerResponse.Message = ex.Message;
        servicerResponse.Success = false;
      }

      return servicerResponse;
    }

    public async Task<ServiceResponse<CharacterResponse>> UpdateCharacter(UpdateCharacterRequest updateCharacter)
    {
      var servicerResponse = new ServiceResponse<CharacterResponse>();
      try {
        var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
        if (character == null)
          throw new Exception($"Character with Id '{updateCharacter.Id}' not found.");

        character = _mapper.Map(updateCharacter, character);
        servicerResponse.Data = _mapper.Map<CharacterResponse>(character);
      }
      catch (Exception ex)
      {
        servicerResponse.Success = false;
        servicerResponse.Message = ex.Message;
      }
      return servicerResponse;
    }
  }
}