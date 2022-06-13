import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FavoriteTutorCardComponent } from './favorite-tutor-card.component';

describe('FavoriteTutorCardComponent', () => {
  let component: FavoriteTutorCardComponent;
  let fixture: ComponentFixture<FavoriteTutorCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FavoriteTutorCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FavoriteTutorCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
