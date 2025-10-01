using pontoFacilApi.source.Domain.Models;

public class BuscarUsuarioPorIdUsecase : IBuscarUsuarioPorIdUsecase
{
    private readonly IUsuariosRepository _usuariosRepository;

    public BuscarUsuarioPorIdUsecase(IUsuariosRepository usuariosRepository)
    {
        _usuariosRepository = usuariosRepository;
    }
    public ResponseBase<Usuario> Executar(string idUsuario)
    {
        if (string.IsNullOrEmpty(idUsuario))
        {
            throw new ApplicationException("O id não pode ser vazio.");
        }

        Usuario? usuario = _usuariosRepository.BuscarPorId(idUsuario);

        if (usuario is null)
        {
            throw new ApplicationException("Nenhum usuario encontrado");
        }

        return new ResponseBase<Usuario>
        {
            Dados = usuario,
            Message = "Informações do usuario encontradas com sucesso!"
        };
    }
}