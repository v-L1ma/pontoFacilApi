import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerficadorForcaSenhaComponent } from './verficador-forca-senha.component';

describe('VerficadorForcaSenhaComponent', () => {
  let component: VerficadorForcaSenhaComponent;
  let fixture: ComponentFixture<VerficadorForcaSenhaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [VerficadorForcaSenhaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(VerficadorForcaSenhaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
