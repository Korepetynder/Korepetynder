import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SettingsInitComponent } from './settings-init.component';

describe('SettingsInitComponent', () => {
  let component: SettingsInitComponent;
  let fixture: ComponentFixture<SettingsInitComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SettingsInitComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SettingsInitComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
