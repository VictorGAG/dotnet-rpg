namespace dotnet_rpg.Services.CharacterService
{
  public class CharacterService : ICharacterService
  {
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CharacterService(IMapper mapper, DataContext context)
    {
      _mapper = mapper;
      _context = context;
    }

    public async Task<ServiceResponse<List<CharacterResponse>>> AddCharacter(CharacterRequest newCharacter)
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      var character = _mapper.Map<Character>(newCharacter);

      await _context.Characters.AddAsync(character);
      await _context.SaveChangesAsync();
      var characters = await _context.Characters.ToListAsync();

      servicerResponse.Data = characters.Select(c => _mapper.Map<CharacterResponse>(c)).ToList();

      return servicerResponse;
    }

    public async Task<ServiceResponse<List<CharacterResponse>>> DeleteCharacter(int id)
    {
      var servicerResponse = new ServiceResponse<List<CharacterResponse>>();
      try {
        var character = _context.Characters.First(c => c.Id == id);
        if (character == null)
          throw new Exception($"Character with Id '{id}' not found.");

        _context.Characters.Remove(character);
        await _context.SaveChangesAsync();

        servicerResponse.Data = _context.Characters.Select(c => _mapper.Map<CharacterResponse>(c)).ToList();
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
      var dbCharacters = await _context.Characters.ToListAsync();

      servicerResponse.Data = dbCharacters.Select(c => _mapper.Map<CharacterResponse>(c)).ToList();
      return servicerResponse;
    }

    public async Task<ServiceResponse<CharacterResponse>> GetCharacterById(int id)
    {
      var servicerResponse = new ServiceResponse<CharacterResponse>();
      try {
        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        if (dbCharacter == null)
          throw new Exception($"Character with Id '{id}' not found.");
          
        servicerResponse.Data = _mapper.Map<CharacterResponse>(dbCharacter);
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
        var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
        if (dbCharacter == null)
          throw new Exception($"Character with Id '{updateCharacter.Id}' not found.");

        dbCharacter = _mapper.Map(updateCharacter, dbCharacter);
        _context.Characters.Update(dbCharacter);
        await _context.SaveChangesAsync();

        servicerResponse.Data = _mapper.Map<CharacterResponse>(dbCharacter);
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