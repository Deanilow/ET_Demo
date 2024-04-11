namespace ET.Domain.Interface.IService
{
    public interface ISedeOlimpicaService
	{
		Task<Response<SedeOlimpica>> Insert(SedeOlimpica SedeOlimpica);
		Task<Response<SedeOlimpica>> Update(SedeOlimpica SedeOlimpica);
		Task<Response<IEnumerable<SedeOlimpica>>> GetAll();
		Task<Response<SedeOlimpica>> Find(Guid Id);
		Task<Response<SedeOlimpica>> Delete(Guid Id);
	}
}
