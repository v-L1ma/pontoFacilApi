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

interface Dataset {
  label: string;
  data: number[];
}

interface ChartData {
  labels: string[];
  dataset: Dataset;
}

export interface ColaboradoresStats {
  colaboradoresAtivos: number;
  novosColaboradoresMes: number;
  colaboradoresDemitidos: number;
  colaboradorDepartamento: ChartData;
  colaboradoresTotalTempo: ChartData;
}

const dadosExemplo: ColaboradoresStats = {
  colaboradoresAtivos: 150,
  novosColaboradoresMes: 10,
  colaboradoresDemitidos: 5,
  colaboradorDepartamento: {
    labels: ["TI", "Marketing", "Vendas"],
    dataset: {
      label: "Colaboradores por Departamento",
      data: [60, 40, 50]
    }
  },
  colaboradoresTotalTempo: {
    labels: ["< 1 ano", "1-3 anos", "> 3 anos"],
    dataset: {
      label: "Tempo de Casa (Total)",
      data: [70, 50, 30]
    }
  }
};
*/