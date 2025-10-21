export interface usuario {
    Id:string;
    Username:string;
    Email:string;
}

export interface cadastrarUsuarioDTO {
    Username:string;
    Email:string;
    Password:string;
    RePassword:string;
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