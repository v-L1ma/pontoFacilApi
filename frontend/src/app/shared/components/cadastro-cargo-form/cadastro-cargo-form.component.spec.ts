import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CadastroCargoFormComponent } from './cadastro-cargo-form.component';

describe('CadastroCargoFormComponent', () => {
  let component: CadastroCargoFormComponent;
  let fixture: ComponentFixture<CadastroCargoFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CadastroCargoFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CadastroCargoFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
