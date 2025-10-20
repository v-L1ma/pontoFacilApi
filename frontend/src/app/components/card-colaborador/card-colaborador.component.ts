import { Component, inject, input, OnInit, output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatAnchor } from "@angular/material/button";
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { colaborador } from '../../types/types';
import { ColaboradoresService } from '../../services/colaboradores/colaboradores.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from '../modal/modal.component';

@Component({
  selector: 'app-card-colaborador',
  imports: [
    MatIconModule,
    MatAnchor,
    MatMenuModule,
    MatButtonModule
],
  templateUrl: './card-colaborador.component.html',
  styleUrl: './card-colaborador.component.scss'
})
export class CardColaboradorComponent implements OnInit{
  colaborador = input.required<colaborador>()
  readonly dialog = inject(MatDialog);
  acaoExecutada = output();

  constructor(
    private colaboradoresService:ColaboradoresService
  ){}

  ngOnInit(): void {
  }

  pegarIniciais():string{
    let iniciais: string= '';

    iniciais+= this. colaborador().nome.charAt(0);

    // if(this.colaborador().nome.split(' ').length>=1){
    //   iniciais+=this.colaborador().nome.split(' ')[1].charAt(0)
    // }     

    return iniciais;
  }
  
  editar(colaboradorEditar:any){
    this.colaboradoresService.editarColaborador(this.colaborador().id, colaboradorEditar).subscribe({
      next:(response)=>{
        console.log(response)
        this.acaoExecutada.emit();
      },
      error:(error)=>{
        console.log(error)
      }
    });
  }

  excluir(id:string){
    this.colaboradoresService.excluirColaborador(id).subscribe({
      next:(response)=>{
        console.log(response)
        this.acaoExecutada.emit()
      },
      error:(error)=>{
        console.log(error)
      }
    });
  }

  openEditDialog(): void {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: {
        tituloModal: 'Editar informações', 
        descricaoModal:'Preencha os dados da conta do colaborador que deseja editar',
        colaborador: this.colaborador()
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
        console.log(result);

        this.editar(result);
      }
    });
  }
}
