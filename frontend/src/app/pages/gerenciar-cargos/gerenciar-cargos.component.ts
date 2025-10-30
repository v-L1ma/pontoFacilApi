import {Component, inject, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { ColaboradoresService } from '../../shared/services/colaboradores/colaboradores.service'; 
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { cadastrarColaboradorDTO, Cargo, colaborador, responseBase, usuario } from '../../shared/types/types';
import { usuarioLogadoService } from '../../shared/services/usuario-logado/usuario-logado.service'; 
import { MatDialog } from '@angular/material/dialog';
import { ModalFormComponent } from '../../shared/components/modal-form-layout/modal-form.component'; 
import { FormsModule } from '@angular/forms';
import { TabelaProdutos } from "../../shared/components/tabela/tabela.component"; 
import { CargoService } from '../../shared/services/cargos/cargo.service';
import { CadastroColaboradorFormComponent } from '../../shared/components/cadastro-colaborador-form/cadastro-colaborador-form.component';
import { CadastroCargoFormComponent } from '../../shared/components/cadastro-cargo-form/cadastro-cargo-form.component';


@Component({
  selector: 'app-gerenciar-cargos',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    FormsModule,
    TabelaProdutos
],
  templateUrl: './gerenciar-cargos.component.html',
  styleUrl: './gerenciar-cargos.component.scss',
})
export class GerenciarCargosComponent {
  paginacao:PageEvent={
    length : 500,
    pageSize : 10,
    pageIndex : 0,
  }
  pageSizeOptions = [5, 10, 25];
  showFirstLastButtons = true;
  cargos:colaborador[] = [];
  cargosFiltrados:colaborador[] = [];
  userLogado : usuario | null = null;
  readonly dialog = inject(MatDialog);
  pesquisar:string = '';
  isLoading = signal<boolean>(false);
  private timeout:any;

  mudarPagina(event:PageEvent){
    this.paginacao.length = event.length;
    this.paginacao.pageSize = event.pageSize;
    this.paginacao.pageIndex = event.pageIndex;
    this.BuscarCargos()
  }

  constructor(private cargosService:CargoService){}
  
  ngOnInit(): void {
    this.BuscarCargos();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(ModalFormComponent, {
      data: {
        tituloModal: 'Criar cargo', 
        descricaoModal:'Preencha os dados do cargo que deseja criar',
        formComponent: CadastroCargoFormComponent
      },
    });

    dialogRef.afterClosed().subscribe((result:Cargo) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
         console.log(result);
        //  this.cadastrar(result)
        this.cargosService.cadastrar(result).subscribe((response:responseBase)=>{
          this.BuscarCargos();
        })
      }
    });
  }

  openEditDialog(cargo:Cargo): void {
    const dialogRef = this.dialog.open(ModalFormComponent, {
      data: {
        tituloModal: 'Editar cargo', 
        descricaoModal:'Preencha os dados do cargo que deseja editar',
        formComponent: CadastroCargoFormComponent,
        formData:{
          nome: cargo.nome,
          setorId: cargo.setorId
        }
      },
    });

    dialogRef.afterClosed().subscribe((result:Cargo) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
         console.log(result);
        //  this.cadastrar(result)
        this.cargosService.editar(cargo.id,result).subscribe((response:responseBase)=>{
          this.BuscarCargos();
        })
      }
    });
  }

  excluir(id:string){
    const idConvertido = Number(id);
    this.cargosService.excluir(idConvertido).subscribe((response:responseBase)=>{
      this.BuscarCargos();
    })
  }

  // cadastrar(form:any){
  //   const cadastrarColaboradorDTO:cadastrarColaboradorDTO = {
  //     nome: form.nome,
  //     cpf: form.CPF,
  //     cargoId: form.cargoId
  //   }

  //   this.colaboradoresService.cadastrar(cadastrarColaboradorDTO).subscribe({
  //     next:(response)=>{
  //       console.log(response);
  //       this.buscarColaboradores();
  //     },
  //     error:(error)=>{
  //       console.log(error);
  //     },
  //   });

  // }

  buscarPeloNome(){
    clearTimeout(this.timeout)
    this.timeout = setTimeout(()=>{
      const pesquisa = this.pesquisar.toLocaleLowerCase().trim();
      this.cargosFiltrados = this.cargos.filter(c=> c.nome.toLocaleLowerCase().includes(pesquisa));  
    },1000)
  }

  BuscarCargos(){
      this.cargosService.buscarPaginado(this.paginacao.pageSize,this.paginacao.pageIndex+1).subscribe(response=>{
        this.cargos = response.dados.itens;
        this.paginacao.length=response.dados.total;
        this.cargosFiltrados = this.cargos;
      })
  }
}
