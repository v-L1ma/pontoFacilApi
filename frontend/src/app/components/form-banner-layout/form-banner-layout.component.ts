import { Component, input } from '@angular/core';
import {MatButtonModule} from '@angular/material/button';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { MatFormFieldControl } from '@angular/material/form-field';
import { RouterLink } from "@angular/router";
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NgStyle } from "@angular/common";

@Component({
  selector: 'app-form-banner-layout',
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    NgStyle
],
  templateUrl: './form-banner-layout.component.html',
  styleUrl: './form-banner-layout.component.scss'
})
export class FormBannerLayoutComponent {
  bannerPosition = input.required<'right' | 'left'>()
}
