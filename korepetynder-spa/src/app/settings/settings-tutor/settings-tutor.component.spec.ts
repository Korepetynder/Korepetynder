import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingsTutorComponent } from './settings-tutor.component';

describe('SettingsTutorComponent', () => {
  let component: SettingsTutorComponent;
  let fixture: ComponentFixture<SettingsTutorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingsTutorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingsTutorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
