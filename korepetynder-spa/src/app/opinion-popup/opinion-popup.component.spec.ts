import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OpinionPopupComponent } from './opinion-popup.component';

describe('OpinionPopupComponent', () => {
  let component: OpinionPopupComponent;
  let fixture: ComponentFixture<OpinionPopupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OpinionPopupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OpinionPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
