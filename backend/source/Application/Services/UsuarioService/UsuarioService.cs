using Microsoft.AspNetCore.Identity;
using pontoFacilApi.source.Application.DTOs;
using pontoFacilApi.source.Domain.Models;

public class UsuarioService : IUsuarioService
{
    private readonly SignInManager<Usuario> _signInManager;
    private readonly UserManager<Usuario> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUsuariosRepository _usuariosRepository;
    public UsuarioService(
        SignInManager<Usuario> signInManager,
        UserManager<Usuario> userManager,
        ITokenService tokenService,
        IUsuariosRepository usuariosRepository
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenService = tokenService;
        _usuariosRepository = usuariosRepository;
    }

    public ResponseBase<List<UsuarioDto>> BuscarUsuarioPaginado(int pageSize, int pageNumber)
    {
        if(pageSize<0 || pageNumber<0){
            throw new ParametroInvalidoException("O tamanho e o numero da pagina devem ser maior que zero!");
        }

        List<UsuarioDto> usuarios = _usuariosRepository.BuscarUsuariosPaginado(pageSize, pageNumber);

        return new ResponseBase<List<UsuarioDto>>
        {
            Dados = usuarios,
            Message = "Usuários listados com sucesso!"
        };
    }

    public ResponseBase<Usuario> BuscarUsuarioPorId(string idUsuario)
    {
        if (string.IsNullOrEmpty(idUsuario))
        {
            throw new ParametroInvalidoException("O id não pode ser vazio.");
        }

        Usuario? usuario = _usuariosRepository.BuscarPorId(idUsuario);

        if (usuario is null)
        {
            throw new NaoEncontradoException("Nenhum usuario encontrado");
        }

        return new ResponseBase<Usuario>
        {
            Dados = usuario,
            Message = "Informações do usuario encontradas com sucesso!"
        };
    }

    public async Task<ResponseBase<Usuario>> CadastrarUsuario(CadastrarUsuarioDTO dto)
    {
        Usuario usuario = new Usuario
        {
            UserName = dto.Username,
            Email = dto.Email.ToLower(),
        };

        if (await _userManager.FindByEmailAsync(dto.Email.ToLower())!=null)
        {
            throw new EmUsoException("Email já está em uso.");
        }

        IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);

        Usuario? usuarioBanco = await _userManager.FindByEmailAsync(dto.Email.ToLower());

        if (!resultado.Succeeded || usuarioBanco == null)
        {
            throw new ApplicationException("Falha ao cadastrar usuário!");
        }

        return new ResponseBase<Usuario>()
        {
            Message = "Usuário cadastrado com sucesso!",
            Dados = usuarioBanco
        };
    }

    public async Task<ResponseBase<string>> EditarUsuario(string idUsuario, EditarUsuarioDTO dto)
    {
         if (string.IsNullOrEmpty(idUsuario))
        {
            throw new ParametroInvalidoException("O Id não pode ser vazio.");
        }

        Usuario? usuarioBanco = _usuariosRepository.BuscarPorId(idUsuario);

        if (usuarioBanco is null)
        {
            throw new NaoEncontradoException("Usuario não encontrado.");
        }
        
        if (!string.IsNullOrEmpty(dto.Email) && _usuariosRepository.BuscarPorEmail(dto.Email.ToLower()) != null && usuarioBanco.Email != dto.Email.ToLower())
        {
            throw new EmUsoException("O email fornecido já está em uso.");
        }

        await _usuariosRepository.EditarPerfil(idUsuario, dto);

        return new ResponseBase<string>
        {
            Message="Informações atualizadas com sucesso."
        };
    }

    public async Task<ResponseBase<string>> ExcluirUsuario(string idUsuario)
    {
        if (string.IsNullOrEmpty(idUsuario))
        {
            throw new ParametroInvalidoException("Forneça um id.");
        }

        var usuarioBanco = await _userManager.FindByIdAsync(idUsuario);

        if (usuarioBanco == null)
        {
            throw new NaoEncontradoException("Usuario não cadastrado.");
        }

        await _usuariosRepository.DesativarPerfil(idUsuario);

        return new ResponseBase<string>
        {
            Message="Conta excluida com sucesso."
        };
    }

    public async Task<ResponseBase<string>> LoginUsuario(LoginUsuarioDTO dto)
    {
        var usuarioBanco = await _userManager.FindByEmailAsync(dto.Email);

        if (usuarioBanco == null)
        {
            throw new NaoEncontradoException("Email e/ou senha inválidos.");
        }

        var resultado = await _signInManager.PasswordSignInAsync(usuarioBanco, dto.Password, false, false);

        if (!resultado.Succeeded)
        {
            throw new NaoEncontradoException("Email e/ou senha inválidos.");
        }

        string token = _tokenService.GerarToken(usuarioBanco);

        return new ResponseBase<string>()
        {
            Message = "Login efetuado com sucesso!",
            Dados = token
        };
    }
}