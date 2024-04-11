namespace ET.Domain.Entities;
public class TipoComisario : Auditoría
{
    public Guid Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
