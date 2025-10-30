import { Component, inject, input, OnInit, output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { MatDialog } from '@angular/material/dialog';
import { MatIcon } from "@angular/material/icon";
import {MatInputModule} from '@angular/material/input';

@Component({
  selector: 'app-tabela',
  imports: [
    MatButtonModule,
    MatIconModule,
    MatPaginatorModule,
    MatProgressSpinnerModule,
    MatIcon, 
    MatInputModule,
    MatButtonModule
],
  templateUrl: './tabela.component.html',
  styleUrl: './tabela.component.scss'
})
export class TabelaProdutos implements OnInit{
  paginacao= input.required<PageEvent>();
  // ({
  //   length:100,
  //   pageIndex:0,
  //   pageSize:10
  // });

  readonly dialog = inject(MatDialog);
  itens = input.required<any[]>();
  excluir = output<string>();
  editar = output<any>();
  mudancaPagina = output<PageEvent>();

  // constructor(private produtosService:ProdutoService){}

  mudarPagina(event:PageEvent){
    console.log(event);
    // this.paginaAtual.set(event);
    this.mudancaPagina.emit(event);
  }

  ngOnInit(): void {
  }
}
