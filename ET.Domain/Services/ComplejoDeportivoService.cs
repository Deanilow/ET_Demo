using ET.Domain.Interface.IRepository;
using ET.Domain.Interface.IService;

namespace ET.Domain.Services
{
    public class ComplejoDeportivoService : IComplejoDeportivoService
    {
        private readonly IComplejoDeportivoRepository _IComplejoDeportivoRepository;

        public ComplejoDeportivoService(IComplejoDeportivoRepository ComplejoDeportivoRepository)
        {
            _IComplejoDeportivoRepository = ComplejoDeportivoRepository;
        }

        public async Task<Response<ComplejoDeportivo>> Delete(Guid Id)
        {
            await _IComplejoDeportivoRepository.Delete(Id);

            return new Response<ComplejoDeportivo>(new ComplejoDeportivo());
        }

        public async Task<Response<ComplejoDeportivo>> Find(Guid Id)
        {
            var ComplejoDeportivo = await _IComplejoDeportivoRepository.FindById(Id);

            return new Response<ComplejoDeportivo>(ComplejoDeportivo);
        }

        public async Task<Response<IEnumerable<ComplejoDeportivo>>> GetAll()
        {
            var SedesOlimpicas = await _IComplejoDeportivoRepository.GetAll();

            return new Response<IEnumerable<ComplejoDeportivo>>(SedesOlimpicas);
        }

        public async Task<Response<ComplejoDeportivo>> Insert(ComplejoDeportivo ComplejoDeportivo)
        {
            try
            {
                var InsertResult = await _IComplejoDeportivoRepository.Insert(ComplejoDeportivo);

                return new Response<ComplejoDeportivo>(InsertResult);
            }
            catch (Exception ex)
            {
                return new Response<ComplejoDeportivo>(ex.Message.ToString());
            }
        }

        public async Task<Response<ComplejoDeportivo>> Update(ComplejoDeportivo ComplejoDeportivo)
        {
            try
            {
                await _IComplejoDeportivoRepository.Update(ComplejoDeportivo);

                return new Response<ComplejoDeportivo>(new ComplejoDeportivo());
            }
            catch (Exception ex)
            {
                return new Response<ComplejoDeportivo>(ex.Message.ToString());
            }
        }
    }
}
