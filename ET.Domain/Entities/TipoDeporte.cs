namespace ET.Domain.Entities;
public class TipoDeporte : Auditoría
{
    public Guid Id { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}
