namespace ET.Domain.Interface.IService
{
    public interface IComplejoDeportivoService
    {
		Task<Response<ComplejoDeportivo>> Insert(ComplejoDeportivo ComplejoDeportivo);
		Task<Response<ComplejoDeportivo>> Update(ComplejoDeportivo ComplejoDeportivo);
		Task<Response<IEnumerable<ComplejoDeportivo>>> GetAll();
		Task<Response<ComplejoDeportivo>> Find(Guid Id);
		Task<Response<ComplejoDeportivo>> Delete(Guid Id);
	}
}
