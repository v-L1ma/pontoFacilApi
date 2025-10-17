import { Component, input, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatAnchor } from "@angular/material/button";
import {MatMenuModule} from '@angular/material/menu';
import {MatButtonModule} from '@angular/material/button';

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
  colaborador = input.required<IColaborador>()

  ngOnInit(): void {
  }

  pegarIniciais():string{
    let iniciais: string= '' 

    iniciais+= this. colaborador().nome.charAt(0);

    if(this.colaborador().nome.split(' ').length>=1){
      iniciais+=this.colaborador().nome.split(' ')[1].charAt(0)
    }     

    return iniciais;
  }
}
