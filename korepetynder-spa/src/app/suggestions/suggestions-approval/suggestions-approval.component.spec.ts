import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SuggestionsApprovalComponent } from './suggestions-approval.component';

describe('SuggestionsApprovalComponent', () => {
  let component: SuggestionsApprovalComponent;
  let fixture: ComponentFixture<SuggestionsApprovalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SuggestionsApprovalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SuggestionsApprovalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
