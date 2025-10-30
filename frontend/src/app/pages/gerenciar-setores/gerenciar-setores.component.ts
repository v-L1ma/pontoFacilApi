import {Component, inject, OnInit, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { responseBase, Setor } from '../../shared/types/types';
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

  pesquisar:string = '';
  isLoading = signal<boolean>(false);
  private timeout:any;

  constructor(
    private setoresService: SetoresService,
    private dialog: MatDialog
  ){}

  ngOnInit(): void {
    this.buscarSetores();
  }

  buscarSetores(){
    this.setoresService.buscarPaginado(this.paginacao.pageSize, this.paginacao.pageIndex+1).subscribe(response=>{
        this.setores = response.dados.itens;
        this.paginacao.length = response.dados.total;
        // console.log("TOTAL ",response.dados.total)
        this.setoresFiltrados=this.setores
  })
  }

  buscarPeloNome(){
    clearTimeout(this.timeout)
    this.timeout = setTimeout(()=>{
      const pesquisa = this.pesquisar.toLocaleLowerCase().trim();
      this.setoresFiltrados = this.setores.filter(c=> c.nome.toLocaleLowerCase().includes(pesquisa));  
    },1000)
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

    dialogRef.afterClosed().subscribe((result:Setor) => {
      // console.log('The dialog was closed');
      if (result !== undefined) {
        //  console.log(result);
        this.setoresService.cadastrar(result).subscribe((response:responseBase)=>{
          this.buscarSetores()
        });
      }
    });
  }

  openEditDialog(setor:Setor): void {
    const dialogRef = this.dialog.open(ModalFormComponent, {
      data: {
        tituloModal: 'Criar setor', 
        descricaoModal:'Preencha os dados do setor que deseja criar',
        formData: {
          nome:setor.nome,
        },
        formComponent: CadastroSetorFormComponent,
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
        this.setoresService.editar(setor.id,result).subscribe((response:responseBase)=>{
          this.buscarSetores()
        });
      }
    });
  }

  excluir(id:string){
    const idConvertido = Number(id)
    this.setoresService.excluir(idConvertido).subscribe((response:responseBase)=>{
      this.buscarSetores();
    });
  }

}