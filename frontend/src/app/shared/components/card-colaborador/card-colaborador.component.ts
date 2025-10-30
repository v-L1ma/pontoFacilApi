import { Component, inject, input, OnInit, output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';
import { colaborador } from '../../types/types'; 
import { ColaboradoresService } from '../../services/colaboradores/colaboradores.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalFormComponent } from '../modal-form-layout/modal-form.component';
import { CpfPipe } from '../../pipes/Cpf.pipe';
import { CadastroColaboradorFormComponent } from '../cadastro-colaborador-form/cadastro-colaborador-form.component';

@Component({
  selector: 'app-card-colaborador',
  imports: [
    MatIconModule,
    MatMenuModule,
    MatButtonModule,
    CpfPipe
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
    console.log(this.colaborador())
    const dialogRef = this.dialog.open(ModalFormComponent, {
      data: {
        tituloModal: 'Editar informações', 
        descricaoModal:'Preencha os dados da conta do colaborador que deseja editar',
        formComponent: CadastroColaboradorFormComponent,
        formData: {
          nome: this.colaborador().nome,
          CPF: this.colaborador().cpf,
          setorId: this.colaborador().setorId,
          cargoId: this.colaborador().cargoId,
        }
      },
    });

    dialogRef.afterClosed().subscribe((result:any) => {
      console.log('The dialog was closed');
      if (result !== undefined) {
        // console.log(result);
        this.editar(result);
      }
    });
  }
}
