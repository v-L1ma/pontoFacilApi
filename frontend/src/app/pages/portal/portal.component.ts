import { Component } from '@angular/core';
import { RouterOutlet } from "@angular/router";
import { SideBarComponent } from '../../shared/components/side-bar/side-bar.component';

@Component({
  selector: 'app-portal',
  imports: [RouterOutlet, SideBarComponent],
  templateUrl: './portal.component.html',
  styleUrl: './portal.component.scss'
})
export class PortalComponent {

}
