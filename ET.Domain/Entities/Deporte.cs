namespace ET.Domain.Entities;
public class Deporte : Auditoría
{
    public Guid Id { get; set; }
    public ComplejoDeportivo ComplejoDeportivo { get; set; } = new ComplejoDeportivo();
    public TipoDeporte TipoDeporte { get; set; } = new TipoDeporte();
    public string Nombre { get; set; } = string.Empty;
    public string Ubicacion { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
}