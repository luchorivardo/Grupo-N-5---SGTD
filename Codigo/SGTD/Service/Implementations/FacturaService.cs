using Data.Contracts;
using Data.Repositorios.Contracts;
using Service.Contracts;
using Service.Mappers;
using Shared.DTOs.FacturaDTOs;
using Shared.Entidades;

namespace Service.Implementations
{
    public class FacturaService : IFacturaService
    {
        private IFacturaRepository _facturaRepository;
        private readonly FacturaMapper _mapper = new FacturaMapper();

        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<List<FacturaReadDTO>> ObtenerTodosAsync()
        {
            var factura = await _facturaRepository.FindAllAsync();
            return _mapper.ToReadDtoList(factura);
        }

        public async Task<FacturaReadDTO> ObtenerPorIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var factura = await _facturaRepository.ObtenerPorId(id);
            if (factura == null)
                throw new KeyNotFoundException($"No se encontró ninguna factura con ID {id}.");

            return _mapper.ToReadDto(factura);
        }

        public async Task<FacturaReadDTO> CrearAsync(FacturaCreateDTO dto)
        {
            var factura = _mapper.ToEntity(dto);
            await _facturaRepository.Create(factura);
            return _mapper.ToReadDto(factura);
        }

        public async Task<FacturaReadDTO> Editar(int id, FacturaUpdateDTO dto)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var factura = await _facturaRepository.ObtenerPorId(id);
            if (factura == null)
                throw new KeyNotFoundException($"No se encontró ninguna factura con ID {id}.");

            _mapper.UpdateEntity(dto, factura);
            await _facturaRepository.Update(factura);

            return _mapper.ToReadDto(factura);
        }

        public async Task Eliminar(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser mayor a cero.");

            var estado = await _facturaRepository.ObtenerPorId(id);
            if (estado == null)
                throw new KeyNotFoundException($"No se encontró ninguna factura con ID {id}.");

            await _facturaRepository.Delete(estado);
        }
    }
}
