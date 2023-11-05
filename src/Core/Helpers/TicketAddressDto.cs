namespace Core.Helpers;
public class TicketAddressDto
{
    private static readonly List<string> _governorates = new List<string> { "Cairo", "Alex", "Portsaid" };
    private static readonly List<string> _cities = new List<string> { "Cairo", "Alex", "Portsaid" };
    private static readonly List<string> _districts = new List<string> { "Maadi", "Elshrouk", "Elgomhoria", "miami" };
    public IReadOnlyList<string> Governorates => _governorates.AsReadOnly();
    public IReadOnlyList<string> Cities => _cities.AsReadOnly();
    public IReadOnlyList<string> Districts => _districts.AsReadOnly();   
}
