import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudSalas } from './salas';

describe('CrudSalas', () => {
  let component: CrudSalas;
  let fixture: ComponentFixture<CrudSalas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrudSalas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrudSalas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
