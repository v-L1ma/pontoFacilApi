import {ChangeDetectionStrategy, Component, inject, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { CardColaboradorComponent } from "../../components/card-colaborador/card-colaborador.component";
import { HeaderComponent } from "../../components/header/header.component";
import { ColaboradoresService } from '../../services/colaboradores/colaboradores.service';
import { MatPaginatorModule } from '@angular/material/paginator';
import { cadastrarColaboradorDTO, colaborador, usuario } from '../../types/types';
import { usuarioLogadoService } from '../../services/usuario-logado/usuario-logado.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../../components/modal/modal.component';
import { AuthService } from '../../services/auth/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-gerenciar-colaboradores',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CardColaboradorComponent,
    HeaderComponent,
    MatPaginatorModule,
    FormsModule
],
  templateUrl: './gerenciar-colaboradores.component.html',
  styleUrl: './gerenciar-colaboradores.component.scss'
})
export class GerenciarColaboradoresComponent implements OnInit{

  constructor(
    private colaboradoresService:ColaboradoresService,
    private usuarioService: usuarioLogadoService,
  ){}

  length = 500;
  pageSize = 10;
  pageNumber = 0;
  pageSizeOptions = [5, 10, 25];
  showFirstLastButtons = true;
  colaboradores:colaborador[] = [];
  userLogado : usuario | null = null;
  readonly dialog = inject(MatDialog);
  pesquisar:string = '';
  handlePageEvent(event:any) {
    this.length = event.length;
    this.pageSize = event.pageSize;
    this.pageNumber = event.pageIndex;
    console.log(event);
    this.buscarColaboradores();
  }
  
  ngOnInit(): void {
    this.usuarioService.retornarUser().subscribe(usuario => {
      this.userLogado = usuario
    });

    this.buscarColaboradores();
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: {
        tituloModal: 'Cadastrar colaborador', 
        descricaoModal:'Preencha os dados da conta do colaborador que deseja cadastrar',
        colaborador:null
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
         console.log(result);
         this.cadastrar(result)
      }
    });
  }

  cadastrar(form:any){
    const cadastrarColaboradorDTO:cadastrarColaboradorDTO = {
      nome: form.nome,
      cpf: form.CPF,
      cargoId: form.cargoId
    }

    this.colaboradoresService.cadastrar(cadastrarColaboradorDTO).subscribe({
      next:(response)=>{
        console.log(response);
        this.buscarColaboradores();
      },
      error:(error)=>{
        console.log(error);
      },
    });

  }

  // buscarPeloNome(){
  //   const pesquisa = this.pesquisar.toLocaleLowerCase().trimStart();

  //   this.colaboradoresFiltrados.set(
  //     this.colaboradores.filter(c=> c.nome.toLocaleLowerCase().includes(pesquisa))
  //   )
  // }

  buscarColaboradores(){
    this.colaboradoresService.buscarColaboradores(this.pageSize, this.pageNumber+1)
    .subscribe({
      next:(response)=>{
        console.log(response)
        this.colaboradores = response.dados.itens;
        this.length = response.dados.total;
      },
      error:(error)=>{
        console.log(error)
      }
    })
  }

}
