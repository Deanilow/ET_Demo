using ET.Domain.Interface.IRepository;
using ET.Domain.Interface.IService;

namespace ET.Domain.Services
{
    public class SedeOlimpicoService : ISedeOlimpicaService
    {
        private readonly ISedeOlimpicaRepository _ISedeOlimpicoRepository;

        public SedeOlimpicoService(ISedeOlimpicaRepository SedeOlimpicoRepository)
        {
            _ISedeOlimpicoRepository = SedeOlimpicoRepository;
        }

        public async Task<Response<SedeOlimpica>> Delete(Guid Id)
        {
            await _ISedeOlimpicoRepository.Delete(Id);

            return new Response<SedeOlimpica>(new SedeOlimpica());
        }

        public async Task<Response<SedeOlimpica>> Find(Guid Id)
        {
            var SedeOlimpica = await _ISedeOlimpicoRepository.FindById(Id);

            return new Response<SedeOlimpica>(SedeOlimpica);
        }

        public async Task<Response<IEnumerable<SedeOlimpica>>> GetAll()
        {
            var SedesOlimpicas = await _ISedeOlimpicoRepository.GetAll();

            return new Response<IEnumerable<SedeOlimpica>>(SedesOlimpicas);
        }

        public async Task<Response<SedeOlimpica>> Insert(SedeOlimpica SedeOlimpica)
        {
            try
            {
                var InsertResult = await _ISedeOlimpicoRepository.Insert(SedeOlimpica);

                return new Response<SedeOlimpica>(InsertResult);
            }
            catch (Exception ex)
            {
                return new Response<SedeOlimpica>(ex.Message.ToString());
            }
        }

        public async Task<Response<SedeOlimpica>> Update(SedeOlimpica SedeOlimpica)
        {
            try
            {
                await _ISedeOlimpicoRepository.Update(SedeOlimpica);

                return new Response<SedeOlimpica>(new SedeOlimpica());
            }
            catch (Exception ex)
            {
                return new Response<SedeOlimpica>(ex.Message.ToString());
            }
        }
    }
}
