import {ChangeDetectionStrategy, Component, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { CardColaboradorComponent } from "../../components/card-colaborador/card-colaborador.component";

@Component({
  selector: 'app-gerenciar-colaboradores',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    CardColaboradorComponent
],
  templateUrl: './gerenciar-colaboradores.component.html',
  styleUrl: './gerenciar-colaboradores.component.scss'
})
export class GerenciarColaboradoresComponent {

}
