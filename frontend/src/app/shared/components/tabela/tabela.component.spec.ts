import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TabelaProdutos } from './tabela.component';

describe('TabelaProdutos', () => {
  let component: TabelaProdutos;
  let fixture: ComponentFixture<TabelaProdutos>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TabelaProdutos]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TabelaProdutos);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
