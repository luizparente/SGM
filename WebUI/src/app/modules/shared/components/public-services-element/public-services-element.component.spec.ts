import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicServicesElementComponent } from './public-services-element.component';

describe('PublicServicesElementComponent', () => {
  let component: PublicServicesElementComponent;
  let fixture: ComponentFixture<PublicServicesElementComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicServicesElementComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicServicesElementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
