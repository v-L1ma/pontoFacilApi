import {Component, inject, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { Setor } from '../../shared/types/types';
import { MatDialog } from '@angular/material/dialog';
import { ModalFormComponent } from '../../shared/components/modal-form-layout/modal-form.component'; 
import { FormsModule } from '@angular/forms';
import { TabelaProdutos } from "../../shared/components/tabela/tabela.component"; 
import { SetoresService } from '../../shared/services/setores/setores.service';
import { CadastroSetorFormComponent } from '../../shared/components/cadastro-setor-form/cadastro-setor-form.component';

@Component({
  selector: 'app-gerenciar-setores',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    FormsModule,
    TabelaProdutos
  ],
  templateUrl: './gerenciar-setores.component.html',
  styleUrl: './gerenciar-setores.component.scss',
})
export class GerenciarSetoresComponent implements OnInit {
  paginacao:PageEvent={
    length : 500,
    pageSize : 10,
    pageIndex : 0,
  }
  pageSizeOptions = [5, 10, 25];
  showFirstLastButtons = true;

  setores:Setor[] = [];
  setoresFiltrados:Setor[] = [];

  readonly dialog = inject(MatDialog);
  pesquisar:string = '';
  isLoading = signal<boolean>(false);
  private timeout:any;

  constructor(private setoresService: SetoresService){}

  ngOnInit(): void {
    this.buscarSetores();
  }

  buscarSetores(){
    this.setoresService.buscarPaginado(this.paginacao.pageSize, this.paginacao.pageIndex+1).subscribe(response=>{
        this.setores = response.dados.itens;
        this.paginacao.length = response.dados.total;
        console.log("TOTAL ",response.dados.total)
        this.setoresFiltrados=this.setores
  })
  }

  mudarPagina(event:PageEvent){
    this.paginacao.length = event.length;
    this.paginacao.pageSize = event.pageSize;
    this.paginacao.pageIndex = event.pageIndex;
    this.buscarSetores()
  }

  openCreateDialog(): void {
    const dialogRef = this.dialog.open(ModalFormComponent, {
      data: {
        tituloModal: 'Criar setor', 
        descricaoModal:'Preencha os dados do setor que deseja criar',
        formComponent: CadastroSetorFormComponent
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
         console.log(result);
      }
    });
  }

  openEditDialog(setor:Setor): void {
    const dialogRef = this.dialog.open(ModalFormComponent, {
      data: {
        tituloModal: 'Criar setor', 
        descricaoModal:'Preencha os dados do setor que deseja criar',
        formComponent: CadastroSetorFormComponent
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
         console.log(result);
      }
    });
  }

  buscarPeloNome(){
    clearTimeout(this.timeout)
    this.timeout = setTimeout(()=>{
      const pesquisa = this.pesquisar.toLocaleLowerCase().trim();
      this.setoresFiltrados = this.setores.filter(c=> c.nome.toLocaleLowerCase().includes(pesquisa));  
    },1000)
  }
}
