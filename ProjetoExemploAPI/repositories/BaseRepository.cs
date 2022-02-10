using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjetoExemploAPI.Context;
using System;

namespace ProjetoExemploAPI.repositories
{
    public class BaseRepository
    {
        #region Propriedades

        public readonly MyContext _contexto;

        public readonly ILogger _logger;

        public readonly IMapper _mapper;

        public BaseRepository()
        {
        }

        #endregion Propriedades

        #region Construtores

        public BaseRepository(MyContext contexto, ILogger logger, IMapper mapper)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }



        #endregion Construtores
    }
}
