import { Component,  signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { usuarioLogadoService } from '../../services/usuario-logado/usuario-logado.service';
import { Router } from '@angular/router';
import { MatMenuModule } from "@angular/material/menu";
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-side-bar',
  imports: [
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    NgClass
  ],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss',
})
export class SideBarComponent {

  usuarioNome: string  = '';
  isMenuOpen = signal<boolean>(false);

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

  navegar(destino:string){
    this.router.navigate([`/portal/${destino}`]);
    this.abrirMenu()
  }

  abrirMenu(){
    this.isMenuOpen.set(!this.isMenuOpen());
  }
}
