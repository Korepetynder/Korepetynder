export class TutorLessonRequest {
  cost: number;
  frequency: number | null;
  subjectId: number;
  levelsIds: number[];
  languagesIds: number[];

  constructor(cost: number, frequency: number | null, subjectId: number,
    levelsIds: number[], languagesIds: number[]) {
    this.cost = cost;
    this.frequency = frequency;
    this.subjectId = subjectId;
    this.levelsIds = levelsIds;
    this.languagesIds = languagesIds;
  }
}
