namespace ET.Domain.Common;
public class Auditoría
{
    public Guid? UsuarioCreador { get; set; } = null;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public Guid? UsuarioModificador { get; set; } = null;
    public DateTime? FechaModificacion { get; set; } = DateTime.Now;
    public bool Eliminado { get; set; } = false;
}
