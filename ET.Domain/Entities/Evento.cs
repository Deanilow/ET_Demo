namespace ET.Domain.Entities;
public class Evento : Auditoría
{
    public Guid Id { get; set; }
    public Deporte Deporte { get; set; } = new Deporte();
    public DateTime Fecha { get; set; } = new DateTime();
    public string Duracion { get; set; } = string.Empty;
    public int NumeroParticipantes { get; set; } = 0;
    public int NumeroComisarios { get; set; } = 0;
}
