import { Component, Input, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-settings-tutor',
  templateUrl: './settings-tutor.component.html',
  styleUrls: ['./settings-tutor.component.scss']
})
export class SettingsTutorComponent implements OnInit {
  @Input() isEdit: boolean = false;

  isTutor: boolean = true;
  profileForm = this.fb.group({
    isTutor: [true],
    lessons: this.fb.array([])
  });

  constructor(public router: Router, private fb: FormBuilder) { }

  get lessons() {
    return this.profileForm.get('lessons') as FormArray;
  }

  get lessonsControls() {
    return this.lessons.controls as FormGroup[];
  }

  addLesson(): void {
    this.lessons.push(this.fb.group({
      course: ['', [Validators.required]],
      level: ['', [Validators.required]],
      cost: ['', [Validators.required]],
      hoursWeekly: [''],
    }));
  }

  removeLesson(id: number): void {
    this.lessons.removeAt(id - 1);
  }

  ngOnInit() {
    this.profileForm.valueChanges.subscribe(val => {
      if (val.isTutor === true)
        this.isTutor = true;
      else
        this.isTutor = false;
    });
  }
}
