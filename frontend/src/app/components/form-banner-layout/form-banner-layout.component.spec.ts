import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormBannerLayoutComponent } from './form-banner-layout.component';

describe('FormBannerLayoutComponent', () => {
  let component: FormBannerLayoutComponent;
  let fixture: ComponentFixture<FormBannerLayoutComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FormBannerLayoutComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FormBannerLayoutComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
