namespace ET.Domain.Entities;
public class EventoComisario : Auditoría
{
    public Guid Id { get; set; }
    public Evento Evento { get; set; } = new Evento();
    public string Nombre { get; set; } = string.Empty;
    public string TipoComisario { get; set; } = string.Empty;
}
