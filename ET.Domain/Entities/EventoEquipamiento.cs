namespace ET.Domain.Entities;
public class EventoEquipamiento : Auditoría
{
    public Guid Id { get; set; }
    public Evento Evento { get; set; } = new Evento();
    public Equipamiento Equipamiento { get; set; } = new Equipamiento();
}
