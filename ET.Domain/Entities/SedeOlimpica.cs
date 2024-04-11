namespace ET.Domain.Entities;
public class SedeOlimpica : Auditoría
{
    public Guid Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int NumeroComplejos { get; set; } = 0;
    public decimal Presupuesto { get; set; } = 0;
}
