import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-public-services-element',
  templateUrl: './public-services-element.component.html',
  styleUrls: ['./public-services-element.component.css']
})
export class PublicServicesElementComponent implements OnInit {
  @Input()
  public header?: string;
  @Input()
  public body?: string;
  @Input()
  public isHighlighted: boolean = false;
  @Input()
  public route: string = "#!";

  constructor() { }

  ngOnInit(): void {
  }

}
