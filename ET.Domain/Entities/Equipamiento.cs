namespace ET.Domain.Entities;
public class Equipamiento : Auditoría
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}
