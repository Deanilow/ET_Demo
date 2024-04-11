namespace ET.Domain.Entities;
public class EventoComisario : Auditoría
{
    public Guid Id { get; set; }
    public Evento Evento { get; set; } = new Evento();
    public TipoComisario TipoComisario { get; set; } = new TipoComisario();
    public string Nombre { get; set; } = string.Empty;
}
