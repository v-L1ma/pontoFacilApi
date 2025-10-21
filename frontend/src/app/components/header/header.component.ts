import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { usuarioLogadoService } from '../../services/usuario-logado/usuario-logado.service';
import { Router } from '@angular/router';
import { MatMenuModule } from "@angular/material/menu";
import { usuario } from '../../types/types';

@Component({
  selector: 'app-header',
  imports: [
    MatIconModule,
    MatButtonModule,
    MatMenuModule
],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit{

  usuarioNome: string  = '';

  constructor(
    private usuarioService: usuarioLogadoService,
    private router: Router
  ){}

  ngOnInit(): void {
    this.usuarioService.retornarUser().subscribe((teste)=>{
      // console.log("TESTANDO", teste)
      this.usuarioNome = teste!.Username;
      // console.log("Usuario nome", this.usuarioNome)
    })
  }

  logout(){
    this.usuarioService.logout();
    this.router.navigate(['/login']);
  }  
}
