namespace ET.Domain.Entities;
public class ComplejoDeportivo : Auditoría
{
    public Guid Id { get; set; }
    public SedeOlimpica SedeOlimpica { get; set; } = new SedeOlimpica();
    public string Nombre { get; set; } = string.Empty;
    public string Localizacion { get; set; } = string.Empty;
    public string JefeOrganizacion { get; set; } = string.Empty;
}
