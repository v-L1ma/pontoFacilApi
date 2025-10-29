import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GerenciarSetoresComponent } from './gerenciar-setores.component';

describe('GerenciarSetoresComponent', () => {
  let component: GerenciarSetoresComponent;
  let fixture: ComponentFixture<GerenciarSetoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GerenciarSetoresComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(GerenciarSetoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
