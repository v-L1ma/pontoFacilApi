export interface usuario {
    id:string;
    nome:string;
    email:string;
}

export interface cadastrarUsuarioDTO {
    username:string;
    email:string;
    password:string;
    rePassword:string;
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