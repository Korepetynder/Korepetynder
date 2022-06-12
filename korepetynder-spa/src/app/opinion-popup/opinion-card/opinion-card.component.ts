import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-opinion-card',
  templateUrl: './opinion-card.component.html',
  styleUrls: ['./opinion-card.component.scss']
})
export class OpinionCardComponent implements OnInit {
  user_id = "ktoś";
  opinion = "opinia ucznia xvcccccccccvcxvcv1.";
  score = 1.3;

  constructor() { }

  ngOnInit(): void {
  }

}
