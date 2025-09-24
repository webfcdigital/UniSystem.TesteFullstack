using MediatR;
using UniSystem.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UniSystem.Application.Users.Queries.GetUsers
{
    public class ObterUsuariosQueryHandler : IRequestHandler<ObterUsuariosQuery, List<UsuarioDto>>
    {
        private readonly IUserRepository _userRepository;

        public ObterUsuariosQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UsuarioDto>> Handle(ObterUsuariosQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);

            return users.Select(user => new UsuarioDto
            {
                Id = user.Id,
                Nome = user.Name,
                Email = user.Email
            }).ToList();
        }
    }
}
