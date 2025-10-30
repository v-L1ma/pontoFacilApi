import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GerenciarCargosComponent } from './gerenciar-cargos.component';

describe('GerenciarCargosComponent', () => {
  let component: GerenciarCargosComponent;
  let fixture: ComponentFixture<GerenciarCargosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GerenciarCargosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GerenciarCargosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
