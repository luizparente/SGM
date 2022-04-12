import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-news-element',
  templateUrl: './news-element.component.html',
  styleUrls: ['./news-element.component.css']
})
export class NewsElementComponent implements OnInit {
  @Input()
  public width?: Number;
  @Input()
  public height?: Number;

  constructor() { }

  ngOnInit(): void { }
}
