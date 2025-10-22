export interface usuario {
    Id:string;
    Username:string;
    Email:string;
    exp:number;
}

export interface cadastrarUsuarioDTO {
    Username:string;
    Email:string;
    Password:string;
    RePassword:string;
}

export interface editarPerfilUsuarioDto{
    nome: string;
    email: string;
}

export interface alterarSenhaDto{
    senhaAtual: string;
    senhaNova: string;
    confirmarSenhaNova: string;
}

export interface colaborador {
    id:string;
    nome:string;
    cpf:string;
    cargo:string;
    setor:string;
}

export interface cadastrarColaboradorDTO {
  nome: string,
  cpf: string,
  cargoId: number
}

export interface responseBase {
    dados:any;
    message:string;
}