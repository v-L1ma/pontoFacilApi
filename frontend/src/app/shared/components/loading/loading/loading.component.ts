import { Component, input } from '@angular/core';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { NgClass } from "@angular/common";

@Component({
  selector: 'app-loading',
  imports: [
    MatProgressSpinnerModule,
    NgClass
],
  templateUrl: './loading.component.html',
  styleUrl: './loading.component.scss'
})
export class LoadingComponent {
    mensagem = input<string>();
    color = input.required<'branco'|'verde'>();
}
