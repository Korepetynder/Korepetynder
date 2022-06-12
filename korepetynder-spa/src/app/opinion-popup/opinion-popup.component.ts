import { Component, Inject } from '@angular/core';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';

export interface DialogData {
  user_id: string;
  score: number;
  opinion: string;
}

@Component({
  selector: 'app-opinion-popup',
  templateUrl: './opinion-popup.component.html',
  styleUrls: ['./opinion-popup.component.scss']
})
export class OpinionPopupComponent {
  user_id = "ktoś";
  opinion = "opinia ucznia xvcccccccccvcxvcv1.";
  score = 1.3;

  constructor(
    public dialogRef: MatDialogRef<OpinionPopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData,
  ) {}

  closeOnClick(): void {
    this.dialogRef.close();
  }

}
