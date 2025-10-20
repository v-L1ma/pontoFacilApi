import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { usuarioLogadoService } from '../../services/usuario-logado/usuario-logado.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  imports: [
    MatIconModule,
    MatButtonModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(
    private usuarioService: usuarioLogadoService,
    private router: Router
  ){}

  logout(){
    this.usuarioService.logout();
    this.router.navigate(['/login']);
  }  
}
