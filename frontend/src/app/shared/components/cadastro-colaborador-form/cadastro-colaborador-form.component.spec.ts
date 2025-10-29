import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroColaboradorFormComponent } from './cadastro-colaborador-form.component';

describe('CadastroColaboradorFormComponent', () => {
  let component: CadastroColaboradorFormComponent;
  let fixture: ComponentFixture<CadastroColaboradorFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroColaboradorFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroColaboradorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
