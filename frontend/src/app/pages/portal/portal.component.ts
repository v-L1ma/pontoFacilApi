import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { HeaderComponent } from '../../shared/components/header/header.component'; 

@Component({
  selector: 'app-portal',
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './portal.component.html',
  styleUrl: './portal.component.scss'
})
export class PortalComponent {

}
