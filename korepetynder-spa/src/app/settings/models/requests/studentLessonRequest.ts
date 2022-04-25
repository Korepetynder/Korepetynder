export class StudentLessonRequest {
  preferredCostMinimum: number;
  preferredCostMaximum: number;
  frequency: number | null;
  subjectId: number;
  levelsIds: number[];
  languagesIds: number[];

  constructor(minCost: number, maxCost: number, frequency: number | null, subjectId: number,
    levelsIds: number[], languagesIds: number[]) {
    this.preferredCostMinimum = minCost;
    this.preferredCostMaximum = maxCost;
    this.frequency = frequency;
    this.subjectId = subjectId;
    this.levelsIds = levelsIds;
    this.languagesIds = languagesIds;
  }
}
