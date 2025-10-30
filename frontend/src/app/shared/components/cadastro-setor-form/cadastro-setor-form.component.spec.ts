import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroSetorFormComponent } from './cadastro-setor-form.component';

describe('CadastroSetorFormComponent', () => {
  let component: CadastroSetorFormComponent;
  let fixture: ComponentFixture<CadastroSetorFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroSetorFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroSetorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
