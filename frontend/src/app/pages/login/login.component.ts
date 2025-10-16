import {ChangeDetectionStrategy, Component, signal} from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { MatFormFieldControl } from '@angular/material/form-field';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-login',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    RouterLink
],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  mostrarSenha = signal(true);
  esconder(){
    this.mostrarSenha.set(!this.mostrarSenha());
  }
}
