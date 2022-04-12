import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-event-element',
  templateUrl: './event-element.component.html',
  styleUrls: ['./event-element.component.css']
})
export class EventElementComponent implements OnInit {
  @Input()
  public width?: Number;
  @Input()
  public height?: Number;

  constructor() { }

  ngOnInit(): void {
  }

}
