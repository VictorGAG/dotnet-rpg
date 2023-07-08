namespace dotnet_rpg.Dtos.Character
{
    public class CharacterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int Health { get; set; } = 100;
        public int Strength { get; set; } = 5;
        public int Defense { get; set; } = 5;
        public int Intelligence { get; set; } = 5;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}