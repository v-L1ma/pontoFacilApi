using pontoFacilApi.source.Domain.Models;

public class BuscarUsuariosPaginadoUsecase : IBuscarUsuariosPaginadoUsecase
{

    private readonly IUsuariosRepository _usuariosRepository;

    public BuscarUsuariosPaginadoUsecase(IUsuariosRepository usuariosRepository) {
        _usuariosRepository = usuariosRepository;
    }
    public ResponseBase<List<Usuario>> Executar(int pageSize, int pageNumber)
    {
        if(pageSize<0 || pageNumber<0){
            throw new ApplicationException("O tamanho e o numero da pagina devem ser maior que zero!");
        }

        List<Usuario> usuarios = _usuariosRepository.BuscarUsuariosPaginado(pageSize, pageNumber);

        return new ResponseBase<List<Usuario>>
        {
            Dados = usuarios,
            Message = "Usuários listados com sucesso!"
        };
    }
}