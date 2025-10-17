import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GerenciarColaboradoresComponent } from './gerenciar-colaboradores.component';

describe('GerenciarColaboradoresComponent', () => {
  let component: GerenciarColaboradoresComponent;
  let fixture: ComponentFixture<GerenciarColaboradoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GerenciarColaboradoresComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GerenciarColaboradoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
