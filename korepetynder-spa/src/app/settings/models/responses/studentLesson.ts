import { Language } from "./language";
import { Level } from "./level";
import { Subject } from "./subject";

export interface StudentLesson {
  id: number;
  preferredCostMinimum: number;
  preferredCostMaximum: number;
  frequency: number | null;
  subject: Subject;
  levels: Level[];
  languages: Language[];
}
