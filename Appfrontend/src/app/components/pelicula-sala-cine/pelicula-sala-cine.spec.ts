import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeliculaSalaCine } from './pelicula-sala-cine';

describe('PeliculaSalaCine', () => {
  let component: PeliculaSalaCine;
  let fixture: ComponentFixture<PeliculaSalaCine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PeliculaSalaCine]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PeliculaSalaCine);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
